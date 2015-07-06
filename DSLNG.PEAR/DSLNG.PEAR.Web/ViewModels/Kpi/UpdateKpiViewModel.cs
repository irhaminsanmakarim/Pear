using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.ViewModels.Kpi
{
    public class UpdateKpiViewModel
    {
        public UpdateKpiViewModel()
        {
            RelationModels = new List<KpiRelationModel>()
                {
                    new KpiRelationModel()
                };
        }
        public string CodeFromLevel { get; set; }
        public string CodeFromPillar { get; set; }

        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "KPI Code")]
        public string Code { get; set; }

        [Required]
        [Display(Name="KPI Name")]
        public string Name { get; set; }

        [Display(Name="Pillar")]
        public int? PillarId { get; set; } //to make this nullable we need to include it
        public IEnumerable<SelectListItem> PillarList { get; set; }

        [Required]
        [Display(Name = "Level")]
        public int LevelId { get; set; }
        public IEnumerable<SelectListItem> LevelList { get; set; }

        [Display(Name = "Accountability")]
        public int? RoleGroupId { get; set; }
        public IEnumerable<SelectListItem> RoleGroupList { get; set; }

        [Required]
        [Display(Name = "Type")]
        public int TypeId { get; set; }
        public IEnumerable<SelectListItem> TypeList { get; set; }

        [Display(Name = "Group")]
        public int? GroupId { get; set; }
        public IEnumerable<SelectListItem> GroupList { get; set; }

        [Display(Name = "Is Economic Model")]
        public bool IsEconomic { get; set; }

        [Required]
        [Display(Name = "Ordering")]
        public int Order { get; set; }

        [Display(Name = "PMS Formula Ytd")]
        public YtdFormula YtdFormula { get; set; }
        public string YtdFormulaValue { get; set; }
        public IEnumerable<SelectListItem> YtdFormulaList { get; set; }

        [Display(Name="Measurement")]
        public int? MeasurementId { get; set; }
        public IEnumerable<SelectListItem> MeasurementList { get; set; }

        [Required]
        [Display(Name = "Method Value")]
        public int MethodId { get; set; }
        public IEnumerable<SelectListItem> MethodList { get; set; }

        public int? ConversionId { get; set; }
        public Conversion Conversion { get; set; }
        public FormatInput FormatInput { get; set; }

        //public Periode Periode { get; set; }

        [Display(Name = "Period Input")]
        public string PeriodeValue { get; set; }
        public PeriodeType Periode { get; set; }
        public IEnumerable<SelectListItem> PeriodeList { get; set; }

        public string Remark { get; set; }

        public ICollection<KpiRelationModel> RelationModels { get; set; }
        public IEnumerable<SelectListItem> KpiList { get; set; }
        public DateTime? Value { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}