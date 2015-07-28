
using System.ComponentModel.DataAnnotations;
namespace DSLNG.PEAR.Web.ViewModels.Artifact
{
    public class TankViewModel
    {
        [Display(Name="Volume Inventory")]
        public int  Id { get; set; }
        public int VolumeInventoryId { get; set; }
        public string VolumeInventory { get; set; }
        public int DaysToTankTopId { get; set; }
        public string DaysToTankTop { get; set; }
        public string DaysToTankTopTitle { get; set; }
        public double MinCapacity { get; set; }
        public double MaxCapacity { get; set; }
    }
}