using SBC_2D.Infrastructures.Ini;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Infrastructures.Bsk
{
    public static class RemoteBskHelper
    {
        private static string _dirPath = string.Empty;
        private const string _rbskFileName = "RemoteBoardSkip.ini";
        private const string _rwbFileName = "RWBIF.dll";
        private const string _rbsFileName = "RBSInfo.dat";
        public static string MCServerIp { get; private set; }
        public static string MachineName { get; private set; }
        public static string ErrMsg { get; set; }

        public static void Initialize(string dirPath, string mcServerIp, string machineName)
        {
            string dir = dirPath;
            string rbskPath = Path.Combine(dirPath, _rbskFileName);
            string rwbPath = Path.Combine(dirPath, _rwbFileName);
            string rbsPath = Path.Combine(dirPath, _rbsFileName);
            if (!File.Exists(rbskPath))
            {
                //LOG
                return;
            }
            IniFile.Write("SETUP", "MCServer", mcServerIp, rbskPath);
            IniFile.Write("SETUP", "MachineName", machineName, rbskPath);
            IniFile.Write("SETUP", "RWBIFPath", rwbPath, rbskPath);
            IniFile.Write("SETUP", "TargetFile", rbsPath, rbskPath);
        }

        public static void Update(int count, int[] intArr)
        {
            string rbskPath = Path.Combine(_dirPath, _rbskFileName);
            IniFile.Write("Info", "BoardQty", count.ToString(), rbskPath);
            string bskArr = string.Join(", ", intArr.Select(i => i.ToString()));
            IniFile.Write("Info", "BoardSkip", bskArr, rbskPath);
        }

        public static void Execute()
        {
            //87347074.exe會對日月光的server發送指令，並對RemoteBoardSkip.log寫入兩條紀錄
            //第一條是CommandInfo(執行的指令訊息)，第二條是ReplyInfo(server回應的訊息)
            //每一條都有時間戳記
            string executablePath = _dirPath + @"\87347074.exe";
            var psi = new ProcessStartInfo
            {
                FileName = executablePath,
                UseShellExecute = false,
                WorkingDirectory = _dirPath
            };
            Process.Start(psi);
        }

        public static int ExecuteResult()
        {
            /* 0. 正常完成
             * 1. 等待資料
             * 2. 失敗
             */
            string logPath = _dirPath + @"\RemoteBoardSkip.log"; // Log 檔案位置
            string result = RBS_Return_Code(logPath);

            switch (result)
            {
                case "Waiting Reply":
                case "No Data":
                case "Null Data":
                case "Open Error":
                case "No File Exist":
                    ErrMsg = ("等待 Machine Communication Server 回覆");
                    return 1;

                case "0x00000000":
                    ErrMsg = String.Empty;
                    return 0;

                case "0x64a00006":
                    ErrMsg = ("Machine Communication Server 未偵測到 Remote Board Skip License");
                    return 2;

                case "0x64a00007":
                    ErrMsg = ("NXT 未開啟 Remote Board Skip 功能");
                    return 2;

                default:
                    ErrMsg = ("Machine Communication Server 回覆收訊異常");
                    return 2;
            }
        }
        private static string RBS_Return_Code(string sourceFile)
        {
            //1. 因為exe都是對同一個檔案存取(RemoteBoardSkip.log)，如果沒有就會生成。
            //2. 做法是利用Move來轉成暫存檔，每次都會擷取一條紀錄(取lastLine)，然後另外寫入RBSLog
            //3. 若因server還未回應，ReplyInfo(第二行)還不會寫進RemoteBoardSkip.log，會回傳首先擷取到的CommandInfo。
            //4. 如果server確實回應完成，RemoteBoardSkip.log就會有一行ReplyInfo的紀錄(這次是新檔案，因為前一次呼叫已經File.Move)
            //5. 分兩次執行ExcuteResult，第一次是擷取CommandInfo，第二次是擷取ReplyInfo
            //6. 記得用ProcessStartInfo指定執行路徑。
            try
            {
                string targetDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RBSLog");
                string targetFile = Path.Combine(targetDir, DateTime.Now.ToString("yyyyMMdd") + ".Log");
                string tempFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RemoteBoardSkip_R.log");

                if (!Directory.Exists(targetDir)) { Directory.CreateDirectory(targetDir); }

                if (!File.Exists(sourceFile)) { return "No File Exist"; }

                if (File.Exists(tempFile)) { File.Delete(tempFile); } // 如果 tempFile 存在，刪除它

                File.Move(sourceFile, tempFile); // 複製 sourceFile 到 tempFile

                string lastLine = "";
                string[] lines = File.ReadAllLines(tempFile);
                if (lines.Length > 0) { lastLine = lines[lines.Length - 1].Trim(); } // 取得最後一行，並去除空白

                File.AppendAllLines(targetFile, lines); // 記錄到 Log 檔

                if (string.IsNullOrEmpty(lastLine)) { return "Null Data"; }

                if (!lastLine.Contains("Error:")) { return "Waiting Reply"; }

                int errorIndex = lastLine.IndexOf("Error:") + 7;

                if (lastLine.Length >= errorIndex + 10)
                { return lastLine.Substring(errorIndex, 10); }
                else
                { return "Waiting Reply"; }
            }
            catch (Exception ex)
            {
                return "Open Error: " + ex.Message; // 通常是檔案開啟發生錯誤
            }
        }
    }
}
