using SBC_2D.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBC_2D.Infrastructures.Recipe
{
    public class Recipe
    {
        public string Name { get; set; } = string.Empty;
        [CompareIgnore]
        public DateTime CreatedAt { get; set; }
        [CompareIgnore]
        public DateTime UpdatedAt { get; set; }
        public bool IsMapModeBypass { get; set; }
        public bool IsUpperBrBypass { get; set; }
        public bool IsLowerBrBypass { get; set; }
        public bool IsLdsBypass { get; set; }
        public bool IsPcbRotate { get; set; }
        public int ThicknessZeroBias { get; set; }
        public int Thickness { get; set; }
        public int ThicknessPosTolerance { get; set; }
        public int PcbCount { get; set; }
        public int PcbBlockX { get; set; }
        public int PcbBlockY { get; set; }
        public int PcbBlocksX { get; set; }
        public int PcbBlocksY { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Recipe recipe &&
                   Name == recipe.Name &&
                   IsMapModeBypass == recipe.IsMapModeBypass &&
                   IsUpperBrBypass == recipe.IsUpperBrBypass &&
                   IsLowerBrBypass == recipe.IsLowerBrBypass &&
                   IsLdsBypass == recipe.IsLdsBypass &&
                   IsPcbRotate == recipe.IsPcbRotate &&
                   ThicknessZeroBias == recipe.ThicknessZeroBias &&
                   Thickness == recipe.Thickness &&
                   ThicknessPosTolerance == recipe.ThicknessPosTolerance &&
                   PcbCount == recipe.PcbCount &&
                   PcbBlockX == recipe.PcbBlockX &&
                   PcbBlockY == recipe.PcbBlockY &&
                   PcbBlocksX == recipe.PcbBlocksX &&
                   PcbBlocksY == recipe.PcbBlocksY;
        }
    }
}
