using System;
using System.ComponentModel.DataAnnotations;

namespace ToolWell
{
    public class OilWellTool
    {
        /// <summary>
        /// A unique identifier for the asset.
        /// </summary>
        [Key]
        public string AssetId { get; set; }// = Guid.NewGuid().ToString();
        public ToolType? Type { get; set; }
        public double Weight { get; set; }
        public double Length { get; set; }
        public double Diameter { get; set; }
        public DateTime ServiceDateDue { get; set; }
        public string Location { get; set; }
       
    }

    public enum ToolType
    {
        OpenHole,
        CasedHole
    }
}
