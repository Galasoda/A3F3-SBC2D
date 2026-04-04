using SBC_2D.Infrastructures.Ini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SBC_2D.Infrastructures.Device
{
    public class Dlen1 : IConnectableDevice, IDisposable
    {
        private Socket _socketClient;
        private SemaphoreSlim _socketClientLock;
        public string Name { get; private set; }
        public bool IsConnected { get; private set; }
        public event Action<string, bool> ConnectionChanged;

        public Dlen1(string name)
        {
            _socketClientLock = new SemaphoreSlim(1, 1);
            Name = name;
        }
        public void Dispose()
        {
            Disconnect();
        }

        public bool Connect(IConnectionConfig config)
        {
            _socketClientLock.Wait();
            try
            {
                SocketConfig cfg = config as SocketConfig;
                if (cfg == null)
                    throw new ArgumentException($"{nameof(config)} is not {nameof(SocketConfig)}");
                if (_socketClient != null)
                    ShutdownClose();
                _socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IAsyncResult result = _socketClient.BeginConnect(IPAddress.Parse(cfg.Address.Trim()), cfg.Port, null, null);
                IsConnected = result.AsyncWaitHandle.WaitOne(2000, true);
                if (!IsConnected)
                    throw new Exception($"{nameof(KeyenceBarcodeReader)} Connect timeout.");
                _socketClient.EndConnect(result);
                _socketClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, new LingerOption(true, 0));
                _socketClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, 1);
                byte[] keepAlive = new byte[12];
                BitConverter.GetBytes((uint)1).CopyTo(keepAlive, 0);
                BitConverter.GetBytes((uint)3000).CopyTo(keepAlive, 4);
                BitConverter.GetBytes((uint)500).CopyTo(keepAlive, 8);
                _socketClient.IOControl(IOControlCode.KeepAliveValues, keepAlive, null);
                IsConnected = _socketClient.Connected;
                return true;
            }
            catch (Exception ex)
            {
                IsConnected = false;
                return false;
            }
            finally
            {
                ConnectionChanged?.Invoke(Name, IsConnected);
                _socketClientLock.Release();
            }
        }

        public void Disconnect()
        {
            _socketClientLock.Wait();
            try
            {
                ShutdownClose();
            }
            finally
            {
                _socketClientLock.Release();
            }
        }

        private void ShutdownClose()
        {
            if (_socketClient == null)
                return;
            try
            {
                _socketClient.Shutdown(SocketShutdown.Both);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                _socketClient.Close();
                ConnectionChanged?.Invoke(Name, _socketClient.Connected);
                _socketClient.Dispose();
                _socketClient = null;
            }
        }

        public bool CheckConnection()
        {
            _socketClientLock.Wait();
            try
            {
                bool isNowConnected = _socketClient != null && _socketClient.Connected;

                if (isNowConnected)
                {
                    try
                    {
                        int length = _socketClient.Send(Encoding.UTF8.GetBytes("KEYENCE\r"));
                        if (length <= 0)
                            isNowConnected = false;
                    }
                    catch
                    {
                        isNowConnected = false;
                    }
                }

                if (IsConnected != isNowConnected)
                {
                    IsConnected = isNowConnected;
                    ConnectionChanged?.Invoke(Name, isNowConnected);
                }

                return IsConnected;
            }
            finally
            {
                _socketClientLock.Release();
            }
        }



        //command = M0\r\n(全部感測器的數據)
        //command = MS\r\n(全部感測器的數據及輸出狀態)
        //command = SR\r\n(指定感測器的數據)
        //error = "ER" "command" ... 
        //OK = "command" ...
        //传感器为错误状态时输出“+100000000”。
        //传感器为超范围时（传感器放大器显示FFFF）输出“+099999999”。
        //传感器为欠范围时（传感器放大器显示-FFFF）输出“-099999999”。
        //传感器为无效状态时（传感器放大器显示----）输出“-099999998”。
        //string response = "M0, +000012345, +000056789\r\n";
        //string response = "MS, 01, +000012345, 02, +000056789\r\n";


        public (Dictionary<int, int?> values, string error) M0(int timeout)
        {
            string response = GetData("M0\r\n", timeout);
            if (ParseError(response, out string errorMsg))
                return (new Dictionary<int, int?>(), errorMsg);
            else
                return (ParseM0(response), errorMsg);
        }

        public (List<(int? value, int status)> values, string error) MS(int timeout)
        {
            string response = GetData("MS\r\n", timeout);
            if (ParseError(response, out string reason))
                return (new List<(int? value, int status)>(), reason);
            else
                return (ParseMS(response), reason);
        }

        public bool SW(int dvcId, int dataId, string data)
        {
            string command = $"SW,{dvcId.ToString("00")},{dataId.ToString("000")},{data}\r\n";
            string response = GetData(command, 1000);
            if (ParseError(response, out string reason))
                return false;
            else
                return ParseSW(response, dvcId, dataId);
        }

        private Dictionary<int, int?> ParseM0(string response)
        {
            var result = new Dictionary<int, int?>();
            if (!response.StartsWith("M0"))
                return result;
            string data = response.Substring(2)
                .Trim(',', '\r', '\n')
                .Replace("\r\n", "");
            string[] parts = data.Split(',');
            for (int i = 0; i < parts.Length; i++)
            {
                if (int.TryParse(parts[i], out int v))
                    result[i + 1] = v;
                else
                    result[i + 1] = null;
            }
            return result;
        }

        private List<(int? value, int status)> ParseMS(string response)
        {
            var result = new List<(int?, int)>();
            if (!response.StartsWith("MS"))
                return result;
            string data = response.Substring(2)
                .Trim(',', '\r', '\n')
                .Replace("\r\n", "");
            string[] parts = data.Split(',');
            for (int i = 0; i < parts.Length - 1; i += 2)
            {
                int status = int.TryParse(parts[i], out int s) ? s : -1;
                int? value = int.TryParse(parts[i + 1], out int v) ? (int?)v : null;
                result.Add((value, status));
            }
            return result;
        }

        private bool ParseSW(string response, int id, int dataId)
        {
            if (!response.StartsWith("SW"))
                return false;
            string data = response.Substring(2)
                .Trim(',', '\r', '\n')
                .Replace("\r\n", "");
            string[] parts = data.Split(',');
            if (parts.Length != 2)
                return false;
            if (!parts[0].Equals(id.ToString("00")))
                return false;
            if (!parts[1].Equals(dataId.ToString("000")))
                return false;
            return true;
        }

        private bool ParseError(string response, out string reason)
        {
            if (response.StartsWith("ER"))
            {
                reason = "error"; //看手冊 回傳錯誤資訊
                return true;
            }
            else
            {
                reason = "";
                return false;
            }
        }

        public string GetData(string command, int timeout)
        {
            _socketClientLock.Wait();
            try
            {
                if (_socketClient == null)
                    return "";
                _socketClient.ReceiveTimeout = timeout;
                byte[] sendBytes = Encoding.ASCII.GetBytes(command);
                _socketClient.Send(sendBytes);
                StringBuilder sb = new StringBuilder();
                byte[] recv = new byte[1024];
                int len;
                while ((len = _socketClient.Receive(recv)) > 0)
                {
                    sb.Append(Encoding.ASCII.GetString(recv, 0, len));
                    if (sb.ToString().EndsWith("\r\n")) break;
                }
                return sb.ToString().Trim();
            }
            catch (Exception ex)
            {
                return "";
            }
            finally
            {
                _socketClientLock.Release();
            }
        }
    }
}
