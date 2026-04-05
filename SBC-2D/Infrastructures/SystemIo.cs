using SBC_2D.Infrastructures.Device;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

public class SystemIo
{
    private readonly Dictionary<int, (IIoDevice Device, int Channel)> _diLookup;
    private readonly Dictionary<int, (IIoDevice Device, int Channel)> _doLookup;
    private readonly Dictionary<string, (int DiStart, int DoStart)> _deviceStartMap;
    private Dictionary<int, bool> _systemDis;
    private Dictionary<int, bool> _systemDos;
    public IReadOnlyDictionary<int, bool> SystemDis => _systemDis;
    public IReadOnlyDictionary<int, bool> SystemDos => _systemDos;
    public event Action<IReadOnlyDictionary<int, bool>> SystemDisUpdated;
    public event Action<IReadOnlyDictionary<int, bool>> SystemDosUpdated;

    public SystemIo(IEnumerable<(IIoDevice Device, int DiStart, int DoStart)> devices)
    {
        _diLookup = new Dictionary<int, (IIoDevice, int)>();
        _doLookup = new Dictionary<int, (IIoDevice, int)>();
        _deviceStartMap = new Dictionary<string, (int, int)>();
        _systemDis = new Dictionary<int, bool>();
        _systemDos = new Dictionary<int, bool>();

        //有重複就讓它建構失敗
        foreach (var (device, diStart, doStart) in devices)
        {
            _deviceStartMap[device.Name] = (diStart, doStart);

            for (int i = 0; i < device.DiCount; i++)
            {
                _systemDis[diStart + i] = false;
                _diLookup[diStart + i] = (device, i);
            }
            for (int i = 0; i < device.DoCount; i++)
            {
                _systemDos[doStart + i] = false;
                _doLookup[doStart + i] = (device, i);
            }
        }
    }

    public void Initialize()
    {
        foreach (var name in _deviceStartMap.Keys)
        {
            var device = _diLookup.Values
                .FirstOrDefault(v => v.Device.Name == name).Device;
            if (device == null) continue;

            device.DisUpdated -= IoDevice_DisUpdated;
            device.DisUpdated += IoDevice_DisUpdated;
            device.DosUpdated -= IoDevice_DosUpdated;
            device.DosUpdated += IoDevice_DosUpdated;
        }
    }

    public bool ControlDo(int systemIndex, bool isOn)
    {
        if (!_doLookup.TryGetValue(systemIndex, out var info))
            return false;
        return info.Device.WriteDo(info.Channel, isOn);
    }

    public bool InverseDo(int systemIndex, out bool isOn)
    {
        isOn = false;
        if (!_doLookup.TryGetValue(systemIndex, out var info))
            return false;
        return info.Device.InverseDo(info.Channel, out isOn);
    }

    public bool TryToDeviceDi(int systemIndex, out int channel, out IIoDevice device)
    {
        if (_diLookup.TryGetValue(systemIndex, out var parms))
        {
            channel = parms.Channel;
            device = parms.Device;
            return true;
        }
        channel = -1;
        device = null;
        return false;
    }

    public bool TryToDeviceDo(int systemIndex, out int channel, out IIoDevice device)
    {
        if (_doLookup.TryGetValue(systemIndex, out var info))
        {
            channel = info.Channel;
            device = info.Device;
            return true;
        }
        channel = -1;
        device = null;
        return false;
    }

    public bool OwnsDi(int systemIndex) => _diLookup.ContainsKey(systemIndex);
    public bool OwnsDo(int systemIndex) => _doLookup.ContainsKey(systemIndex);

    private void IoDevice_DisUpdated(string deviceName, IReadOnlyCollection<bool> data)
        => HandleIoUpdated(deviceName, data, isDi: true);

    private void IoDevice_DosUpdated(string deviceName, IReadOnlyCollection<bool> data)
        => HandleIoUpdated(deviceName, data, isDi: false);

    private void HandleIoUpdated(string deviceName, IReadOnlyCollection<bool> data, bool isDi)
    {
        if (!_deviceStartMap.TryGetValue(deviceName, out var starts)) return;

        int start = isDi ? starts.DiStart : starts.DoStart;
        var systemMap = isDi ? _systemDis : _systemDos;
        var changedMap = new Dictionary<int, bool>();

        int i = 0;
        foreach (bool value in data)
        {
            int systemIndex = start + i++;
            if (systemMap[systemIndex] == value)
                continue;
            systemMap[systemIndex] = value;
            changedMap[systemIndex] = value;
        }

        if (changedMap.Count == 0)
            return;

        if (isDi)
            _systemDis = systemMap;
        else
            _systemDos = systemMap;

        if (isDi)
            SystemDisUpdated?.Invoke(changedMap);
        else
            SystemDosUpdated?.Invoke(changedMap);
    }
}