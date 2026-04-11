using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static SBC_2D.Shared.Enums;

namespace SBC_2D.Infrastructures.Bsk
{
    public class BskArrayBuilder
    {
        private readonly XDocument _xdoc;
        public string Path { get; private set; }
        public int LayoutX { get; private set; } //基板X方向的所有點數
        public int LayoutY { get; private set; } //基板Y方向的所有點數
        public int FailCount { get; private set; }
        public int PassCount { get; private set; }
        public int TotalCount { get; private set; }
        public string[,] Codes { get; private set; }
        public int[] FrontSkips { get; set; }
        public int[] BackSkips { get; set; }

        public BskArrayBuilder(string xmlPath)
        {
            Path = xmlPath;
            FrontSkips = Array.Empty<int>();
            BackSkips = Array.Empty<int>();
            _xdoc = XDocument.Load(Path);
        }

        public bool Phrase()
        {
            return ParseLayoutDimensions(_xdoc) && ParseBinCodes(_xdoc);
        }

        public string[,] RotateLeftRight(string[,] codes)
        {
            int rows = codes.GetLength(0);
            int cols = codes.GetLength(1);

            string[,] rotatedCodes = new string[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    rotatedCodes[i, j] = codes[i, cols - j - 1];
                }
            }
            return rotatedCodes;
        }


        private bool ParseLayoutDimensions(XDocument xdoc)
        {
            try
            {
                XElement xeLayouts = xdoc.Root
                    .Elements()
                    .FirstOrDefault(e => e.Name.LocalName == "Layouts")
                    ?? throw new Exception("找不到 Layouts 節點");

                XElement xeLayout = xeLayouts
                    .Descendants()
                    .FirstOrDefault(e =>
                        e.Name.LocalName == "Layout" &&
                        e.Attribute("LayoutId")?.Value != "Strip")
                    ?? throw new Exception("找不到 Layout 節點");

                XElement xeDimension = xeLayout
                    .Elements()
                    .FirstOrDefault(e => e.Name.LocalName == "Dimension")
                    ?? throw new Exception("Layout 缺少 Dimension");

                LayoutX = int.TryParse(xeDimension.Attribute("X")?.Value, out var x) ? x : 0;
                LayoutY = int.TryParse(xeDimension.Attribute("Y")?.Value, out var y) ? y : 0;

                if (LayoutX <= 0 || LayoutY <= 0)
                    throw new Exception($"Layout 尺寸錯誤: X={LayoutX}, Y={LayoutY}");

                TotalCount = LayoutX * LayoutY;
            }
            catch (Exception ex)
            {
                return false;
            }
            Codes = new string[LayoutY, LayoutX];
            return true;
        }

        private bool ParseBinCodes(XDocument xdoc)
        {
            bool isParseOk = true;
            try
            {
                XElement xeSubstrateMaps = xdoc.Root
                    .Elements()
                    .FirstOrDefault(e => e.Name.LocalName == "SubstrateMaps")
                    ?? throw new Exception("缺少 SubstrateMaps 節點");

                var binCodesList = xeSubstrateMaps
                    .Descendants()
                    .Where(e => e.Name.LocalName == "BinCode")
                    .ToList();

                if (binCodesList.Count < LayoutY)
                    throw new Exception($"BinCode 列數不足，預期 {LayoutY}，實際 {binCodesList.Count}");

                int codeLength = 4;
                for (int row = 0; row < LayoutY; row++)
                {
                    string binCodes = binCodesList[row].Value;
                    for (int column = 0; column < LayoutX; column++)
                    {
                        int start = column * codeLength;
                        if (start >= binCodes.Length) break;

                        string binCode = binCodes.Substring(start, Math.Min(codeLength, binCodes.Length - start));
                        Codes[row, column] = binCode;
                    }
                }
            }
            catch (Exception ex)
            {
                isParseOk = false;
            }
            return isParseOk;
        }

        public int[] TakeSkips(string[,] Codes, int[,] indexes)
        {
            List<int> skips = new List<int>();

            int LayoutX = Codes.GetLength(1);
            int LayoutY = Codes.GetLength(0);

            for (int row = 0; row < LayoutY; row++)
            {
                for (int column = 0; column < LayoutX; column++)
                {
                    if (Codes[row, column] == "___0" || Codes[row, column] == "____")
                        skips.Add(indexes[row, column]);
                }
            }
            skips.Sort();
            return skips.ToArray();
        }


        public int[,] CreateLayoutIndex(int startNumber, int x, int y, ArraySortType sortMode)
        {
            int[,] indexes = new int[y, x];
            int value = startNumber;
            switch (sortMode)
            {
                // ----------------
                // 橫向優先
                // ----------------
                case ArraySortType.upperLeft_H:     // 左上 → 右下
                for (int r = 0; r < y; r++)
                    for (int c = 0; c < x; c++)
                        indexes[r, c] = value++;
                break;

                case ArraySortType.upperRight_H:    // 右上 → 左下
                for (int r = 0; r < y; r++)
                    for (int c = 0; c < x; c++)
                        indexes[r, x - 1 - c] = value++;
                break;

                case ArraySortType.lowerRight_H:    // 右下 → 左上
                for (int r = 0; r < y; r++)
                    for (int c = 0; c < x; c++)
                        indexes[y - 1 - r, x - 1 - c] = value++;
                break;

                case ArraySortType.lowerLeft_H:     // 左下 → 右上
                for (int r = 0; r < y; r++)
                    for (int c = 0; c < x; c++)
                        indexes[y - 1 - r, c] = value++;
                break;

                // ----------------
                // 直向優先
                // ----------------
                case ArraySortType.upperLeft_V:     // 左上 → 右下 (直向)
                for (int c = 0; c < x; c++)
                    for (int r = 0; r < y; r++)
                        indexes[r, c] = value++;
                break;

                case ArraySortType.upperRight_V:    // 右上 → 左下 (直向)
                for (int c = 0; c < x; c++)
                    for (int r = 0; r < y; r++)
                        indexes[r, x - 1 - c] = value++;
                break;

                case ArraySortType.lowerRight_V:    // 右下 → 左上 (直向)
                for (int c = 0; c < x; c++)
                    for (int r = 0; r < y; r++)
                        indexes[y - 1 - r, x - 1 - c] = value++;
                break;

                case ArraySortType.lowerLeft_V:     // 左下 → 右上 (直向)
                for (int c = 0; c < x; c++)
                    for (int r = 0; r < y; r++)
                        indexes[y - 1 - r, c] = value++;
                break;
            }
            return indexes;
        }

        public int ConvertIndex(
            int oldNumber,          // 舊編號（右下角為 1）
            int totalCols,          // 矩陣寬度
            int totalRows,          // 矩陣高度
            int blockCols,          // Block 寬
            int blockRows,          // Block 高
            bool xMain = true,      // X 主導排列（true）或 Y 主導排列（false）這邊應該只要用到X
            bool rowMajor = true    // 右下角為起點1
)
        {
            int index0 = oldNumber - 1;

            int rowFromBottom = index0 / totalCols;
            int colFromRight = index0 % totalCols;

            int row = totalRows - 1 - rowFromBottom;
            int col = totalCols - 1 - colFromRight;

            int RCX, RCY;

            if (rowMajor)
            {
                RCX = col + 1;
                RCY = row + 1;
            }
            else
            {
                RCX = row + 1;
                RCY = col + 1;
            }

            int BX = totalCols / blockCols;
            int BY = totalRows / blockRows;

            int blockNo;

            if (xMain)
                blockNo = ((RCX - 1) / blockCols) + ((RCY - 1) / blockRows) * BX;
            else
                blockNo = ((RCX - 1) / blockCols) * BY + ((RCY - 1) / blockRows);

            int innerIndex;
            if (rowMajor)
                innerIndex = ((RCX - 1) % blockCols) + ((RCY - 1) % blockRows) * blockCols;
            else
                innerIndex = ((RCY - 1) % blockRows) + ((RCX - 1) % blockCols) * blockRows;

            return blockNo * (blockCols * blockRows) + innerIndex + 1;
        }




        public int ConvertIndex(
            int oldNumber,          // 舊編號
            int totalCols,          // 矩陣寬度
            int totalRows,          // 矩陣高度
            int blockCols,          // Block 寬
            int blockRows,          // Block 高
            ArraySortType sortType, // 編號排法
            bool xMain = true,      // X 主導排列（true）或 Y 主導排列（false）這邊應該只要用到X
            bool rowMajor = true    // 右下角為起點1
        )
        {
            //前半段要將矩陣縮簡為一維陣列想像，因為區塊在每列每行的占用數量是固定的
            int index0 = oldNumber - 1;

            //應該使用預設的左上到右下
            int oldRow = index0 / totalCols;
            int oldCol = index0 % totalCols;
            //這裡將整個矩陣鏡像成右下到左上
            //起始位置有變更需求就改這裡
            int row = 0;
            int col = 0;
            switch (sortType)
            {
                case ArraySortType.upperLeft_H:     // 左上 → 右下
                row = oldRow;
                col = oldCol;
                break;

                case ArraySortType.upperRight_H:    // 右上 → 左下
                row = oldRow;
                col = totalCols - 1 - oldCol;
                break;

                case ArraySortType.lowerRight_H:    // 右下 → 左上
                row = totalRows - 1 - oldRow;
                col = totalCols - 1 - oldCol;
                break;

                case ArraySortType.lowerLeft_H:     // 左下 → 右上
                row = totalRows - 1 - oldRow;
                col = oldCol;
                break;
            }

            int RCX, RCY;

            if (rowMajor)
            {
                RCX = col + 1;
                RCY = row + 1;
            }
            else
            {
                RCX = row + 1;
                RCY = col + 1;
            }

            //求出有幾個BLOCK
            int BX = totalCols / blockCols;
            int BY = totalRows / blockRows;

            int blockNo;

            if (xMain)
                //((RCX - 1) / blockCols)以及((RCY - 1) / blockRows)都是計算其在該軸上區塊的位置
                //所以要計算整個矩陣的區塊位置
                //因為是X順序優先，所以要算每一列(Y)占用到幾個X方向的區塊
                //白話: X軸區塊位置(補) + 計算Y軸區塊位置 * BX(算面積)
                blockNo = ((RCX - 1) / blockCols) + ((RCY - 1) / blockRows) * BX;
            else
                blockNo = ((RCX - 1) / blockCols) * BY + ((RCY - 1) / blockRows);

            int innerIndex;
            if (rowMajor)
                //跟上方用意一樣，只是上面是找BLOCK位置，而這裡是取餘數計算得出BLOCK裡面的位置
                innerIndex = ((RCX - 1) % blockCols) + ((RCY - 1) % blockRows) * blockCols;
            else
                innerIndex = ((RCY - 1) % blockRows) + ((RCX - 1) % blockCols) * blockRows;
            //blockNo * (blockCols * blockRows)算面積 + (最後一塊的位置 + 1) 
            //(+1是將程式的矩陣座標轉為編號)
            //如果起始編號不是1也可改
            return blockNo * (blockCols * blockRows) + innerIndex + 1;
        }


        public void PrintArray(Array codes)
        {
            int rows = codes.GetLength(0);
            int cols = codes.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(codes.GetValue(i, j) + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}