using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC_2D.Shared
{
    public static class Enums
    {
        public enum MachineStatus
        {
            Idle,
            Running,
            Alarm,
            Lock
        }

        public enum Role
        {
            Operater = 0,
            Engineer = 1,
            Vendor = 2
        }


        [Flags]
        public enum LightStatus
        {
            None = 0,
            Green = 1 << 0,
            Yellow = 1 << 1,
            Red = 1 << 2
        }

        public enum RecipeManageViewMode
        {
            Nothing,
            Open,
            Save,
            SaveAs,
            Delete
        }

        public enum Langs
        {
            Both,
            Zh,
            En
        }

        public enum ArraySortType
        {
            upperLeft_H,   // 左上 → 右下 (橫向)
            upperRight_H,  // 右上 → 左下 (橫向)
            lowerRight_H,  // 右下 → 左上 (橫向)
            lowerLeft_H,   // 左下 → 右上 (橫向)

            upperLeft_V,   // 左上 → 右下 (直向)
            upperRight_V,  // 右上 → 左下 (直向)
            lowerRight_V,  // 右下 → 左上 (直向)
            lowerLeft_V    // 左下 → 右上 (直向)
        }

        public enum AutoRunStep
        {
            檢查輸送帶,
            進板前準備,
            等待進板,
            等待流板到位,
            流板到位確認,
            測量板子厚度,
            讀取條碼,
            MakeA3XML,
            讀取XML及擷取BSK資訊,
            發送Bsk資訊,
            等待止檔氣壓缸下降,
            等待流板抵達出口,
            等待下游要板訊號,
            將板子移至下游,
            錯誤流程,
            停止自動
        }

        public enum ErrorType
        {
            Code,
            Process,
            Timeout,
            Comparison,
        }

        public enum ErrorCode : int
        {
            NoError,
            程式錯誤_捕捉到執行例外,
            ES1,
            ES2,
            EL1,
            EL2,
            啟動失敗_輸送帶未清空,
            E2, 
            等待流板到位已超時, 
            等待板子抵達出口已超時, 
            等待板子移出已超時, 
            E6,
            板子長度NG, 
            止擋氣壓缸上升未到位, 
            止擋氣壓缸未降落, 
            止擋氣壓缸異常, 
            程式錯誤_條碼機讀取異常,
            條碼機讀取超時, 
            條碼機回傳非數值,
            讀取到的條碼數量與設定條數不符,
            條碼機連線已中斷,
            讀取BSK資訊失敗,
            發送BSK資訊失敗, 
            E16, 
            E17,
            E18,
            E19,
            E20,
            E21,
            E22, 
            E23, 
            板厚量測異常_回傳數值無法解析,
            板厚量測異常_回傳的讀頭數量不符,
            板厚量測異常_收到錯誤代碼ER, 
            板厚量測異常_讀頭狀態為Error, 
            E26, 
            板厚數值超過設定上限, 
            E28, 
            E30, 
            E31, 
            E32, 
            E33, 
            Barcode格式錯誤,
            程式錯誤_型態不相符,
        }

    }
}
