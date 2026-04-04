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

        public enum AutoRunStep
        {
            開始自動,
            判斷模式,
            進板前準備,
            等待進板,
            讀取條碼,
            XML_MAP處理,
            下降止檔,
            上升止檔,
            錯誤流程,
            停止,
        }

        public enum ErrorCode : int
        {
            NoError,
            ES1,
            ES2,
            EL1,
            EL2,
            E1,
            E2, 
            E3, 
            E4, 
            E5, 
            E6,
            E7, 
            E8, 
            E9, 
            E10, 
            E11, 
            E12, 
            E13,
            E14,
            E15, 
            E16, 
            E17,
            E18,
            E19,
            E20,
            E21,
            E22, 
            E23, 
            E24, 
            E25, 
            E26, 
            E27, 
            E28, 
            E29, 
            E30, 
            E31, 
            E32, 
            E33, 
            E34 
        }

    }
}
