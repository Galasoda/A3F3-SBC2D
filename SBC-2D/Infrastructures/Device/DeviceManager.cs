using SBC_2D.Domain.Servicies;
using SBC_2D.Events;
using SBC_2D.Infrastructures.Ini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SBC_2D.Infrastructures.Device
{
    public class DeviceManager
    {
        private List<IDevice> _devices;
        private List<IConnectableDevice> _connectableDevices;
        private List<IoDeviceContext> _ioDeviceContexts;
        private DeviceConfig _deviceConfig;
        private SemaphoreSlim _connectLimit;
        private Task _pollingDevicesTask;
        private CancellationTokenSource _ctsPollingDevicesConnection;
        public IReadOnlyList<IDevice> Devices => _devices;
        public IReadOnlyList<IConnectableDevice> ConnectableDevices => _connectableDevices;
        public IReadOnlyList<IoDeviceContext> IoDeviceContexts => _ioDeviceContexts;

        public bool IsStartedPollingDevicesConnection
        {
            get => _ctsPollingDevicesConnection != null && !_ctsPollingDevicesConnection.IsCancellationRequested;
        }

        public DeviceManager()
        {
            _connectLimit = new SemaphoreSlim(3);
            _deviceConfig = new DeviceConfig();
            _devices = new List<IDevice>();
            _ioDeviceContexts = new List<IoDeviceContext>();
        }

        public void Initialize(DeviceConfig deviceConfig)
        {
            List<IDevice> devices = DeviceFactory.CreateDevices(deviceConfig);
            _devices.Clear();
            _devices.AddRange(devices);
            _connectableDevices = _devices.OfType<IConnectableDevice>().ToList();
            List<IoDeviceContext> iodcs = DeviceFactory.CreateIoDeviceContexts(devices.OfType<IIoDevice>());
            _ioDeviceContexts.Clear();
            _ioDeviceContexts.AddRange(iodcs);
            _deviceConfig = deviceConfig;
            //foreach (var device in _connectableDevices)
            //{
            //    if (device is IIoDevice ioDevice)
            //    {
            //        device.ConnectionChanged -= IoDevice_ConnectionChanged;
            //        device.ConnectionChanged += IoDevice_ConnectionChanged;
            //    }
            //}
        }

        //private async void IoDevice_ConnectionChanged(string name, bool status)
        //{
        //    var iodc = _ioDeviceContexts.FirstOrDefault(c => c.Device.Name == name);
        //    if (iodc == null)
        //        return;
        //    if (status)
        //    {
        //        if (!iodc.IsStartedUpdatingDios)
        //            _ = iodc.StartUpdatingDios();
        //    }
        //    else
        //    {
        //        if (iodc.IsStartedUpdatingDios)
        //            await iodc.StopUpdatingDios();
        //    }
        //}

        /* Connection */
        public async Task<Dictionary<string, bool>> ConnectAllAsync()
        {
            var tasks = _devices
                .OfType<IConnectableDevice>()
                .Where(d => _deviceConfig.SocketConfigs.ContainsKey(d.Name))
                .Select(d => ConnectAsync(d, _deviceConfig.SocketConfigs[d.Name]));

            (string Name, bool Value)[] results = await Task.WhenAll(tasks);

            return results.ToDictionary(r => r.Name, r => r.Value);
        }

        public async Task<(string Name, bool Value)> ConnectAsync(
            IConnectableDevice device,
            IConnectionConfig config)
        {
            await _connectLimit.WaitAsync();
            try
            {
                bool isConnected = await Task.Run(() => device.Connect(config));
                return (device.Name, isConnected);
            }
            catch (Exception ex)
            {
                return (device.Name, false);
            }
            finally
            {
                _connectLimit.Release();
            }
        }

        /* Polling */
        public Task StartPollingAllDeviceConnection()
        {
            _ctsPollingDevicesConnection = new CancellationTokenSource();
            _pollingDevicesTask = Task.Run(async () =>
            {
                try
                {
                    while (!_ctsPollingDevicesConnection.Token.IsCancellationRequested)
                    {
                        var tasks = _devices.OfType<IConnectableDevice>().Select(device => Task.Run(() =>
                            {
                                try
                                {
                                    device.CheckConnection();
                                }
                                catch (Exception ex)
                                {
                                }
                            }));

                        await Task.WhenAll(tasks);
                        await Task.Delay(1000, _ctsPollingDevicesConnection.Token);
                    }
                }
                catch (OperationCanceledException)
                {
                    //出問題要waring
                }
            });
            return _pollingDevicesTask;
        }

        public async Task StopPollingAllDevicesConnection()
        {
            if (_ctsPollingDevicesConnection == null)
            {
                return;
            }

            _ctsPollingDevicesConnection.Cancel();

            try
            {
                if (_pollingDevicesTask != null)
                {
                    await _pollingDevicesTask;
                }

            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
                //出問題要waring
            }
            finally
            {
                _ctsPollingDevicesConnection.Dispose();
                _ctsPollingDevicesConnection = null;
                _pollingDevicesTask = null;
            }
        }

        public async Task StartUpdatingAllDios()
        {
            var tasks = new List<Task>();
            foreach (var iodc in _ioDeviceContexts)
                tasks.Add(iodc.StartUpdatingDios());
            await Task.WhenAll(tasks);
        }

        public async Task StopUpdatingAllDios()
        {
            var tasks = new List<Task>();
            foreach (var iodc in _ioDeviceContexts)
                tasks.Add(iodc.StopUpdatingDios());
            await Task.WhenAll(tasks);
        }

        public bool ControlDo(int systemIndex, bool isOn)
        {
            int index = -1;
            foreach (var iodc in _ioDeviceContexts)
            {
                if (iodc.TryToDeviceDo(systemIndex, out index))
                {
                    iodc.Device.WriteDo(index, isOn);
                    return true;
                }
            }
            return false;
        }

        public bool InverseDo(int systemIndex, out bool isOn)
        {
            isOn = false;
            bool isInversed = false;
            foreach (var iodc in _ioDeviceContexts)
            {
                if (iodc.TryToDeviceDo(systemIndex, out int index))
                {
                    isInversed = iodc.Device.InverseDo(index, out bool result);
                    isOn = result;
                    break;
                }
            }
            return isInversed;
        }


        /* Helper */
        public bool TryGetConnectableDevice(string name, out IConnectableDevice device)
        {
            device = _devices.OfType<IConnectableDevice>().FirstOrDefault(d => d.Name.Equals(name));
            return device != null;
        }
    }
}