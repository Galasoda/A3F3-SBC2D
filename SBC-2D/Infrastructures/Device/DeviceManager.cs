using SBC_2D.Domain.Servicies;
using SBC_2D.Infrastructures.Ini;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SBC_2D.Infrastructures.Device
{
    public class DeviceManager : IDisposable
    {
        private List<IDevice> _devices;
        private List<IConnectableDevice> _connectableDevices;
        private DeviceConfig _deviceConfig;
        private SemaphoreSlim _connectLimit;
        private Task _pollingDevicesTask;
        private CancellationTokenSource _ctsPollingDevicesConnection;
        private Task _updateDiosTask;
        private CancellationTokenSource _ctsUpdatingDios;
        public IReadOnlyList<IDevice> Devices => _devices;
        public IReadOnlyList<IConnectableDevice> ConnectableDevices => _connectableDevices;
        public SystemIo SystemIo { get; private set; }
        public bool IsStartedPollingDevicesConnection
        {
            get => _ctsPollingDevicesConnection != null && !_ctsPollingDevicesConnection.IsCancellationRequested;
        }
        public bool IsStartedUpdatingDios
        {
            get => _ctsUpdatingDios != null && !_ctsUpdatingDios.IsCancellationRequested;
        }

        public DeviceManager()
        {
            _connectLimit = new SemaphoreSlim(3);
            _deviceConfig = new DeviceConfig();
            _devices = new List<IDevice>();
            _connectableDevices = new List<IConnectableDevice>();
            SystemIo = new SystemIo(new List<(IIoDevice, int DiStart, int DoStart)>());
        }

        public void Dispose()
        {
            _connectLimit?.Dispose();
            _ctsPollingDevicesConnection?.Dispose();
        }

        public void Initialize(DeviceConfig deviceConfig)
        {
            List<IDevice> devices = DeviceFactory.CreateDevices(deviceConfig);
            _devices.Clear();
            _devices.AddRange(devices);
            _connectableDevices = _devices.OfType<IConnectableDevice>().ToList();
            _deviceConfig = deviceConfig;
        }

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
            _ctsUpdatingDios = new CancellationTokenSource();
            _updateDiosTask = Task.Run(async () =>
            {
                try
                {
                    var ioDevices = _devices.OfType<IIoDevice>().ToList();
                    while (!_ctsUpdatingDios.Token.IsCancellationRequested)
                    {
                        var tasks = new List<Task>();
                        foreach (var ioDevice in ioDevices)
                        {
                            tasks.Add(Task.Run(() =>
                            {
                                ioDevice.ReadAllDi(out bool[] dis);
                                ioDevice.ReadAllDo(out bool[] dos);
                            }));
                        }
                        await Task.WhenAll(tasks);
                        await Task.Delay(100, _ctsUpdatingDios.Token);
                    }
                }
                catch (OperationCanceledException) { }
            });
        }

        public async Task StopUpdatingAllDios()
        {
            if (_ctsUpdatingDios == null)
                return;

            //請求停止而已
            _ctsUpdatingDios.Cancel();

            //還是要等待task完成最後一次
            try
            {
                if (_ctsUpdatingDios != null)
                    await _updateDiosTask;
            }
            catch (TaskCanceledException)
            {
                // 忽略，代表正常停止
            }
            finally
            {
                _ctsUpdatingDios.Dispose();
                _ctsUpdatingDios = null;
                _updateDiosTask = null;
            }
        }

    }
}