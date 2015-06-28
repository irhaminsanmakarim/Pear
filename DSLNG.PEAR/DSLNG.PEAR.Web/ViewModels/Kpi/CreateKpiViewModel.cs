using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.ViewModels.Kpi
{
    public class CreateKpiViewModel
    {
        public CreateKpiViewModel()
        {
            LevelList = new List<SelectListItem>();
            TypeList = new List<SelectListItem>();
            GroupList = new List<SelectListItem>();
            RoleGroupList = new List<SelectListItem>();
            MethodList = new List<SelectListItem>();
            MeasurementList = new List<SelectListItem>();
            YtdFormulaList = new List<SelectListItem>();
            KpiList = new List<SelectListItem>();
            PillarList = new List<SelectListItem>();
        }
        public string CodeFromLevel { get; set; }
        public string CodeFromPillar { get; set; }

        [Required]
        [Display(Name = "KPI Code")]
        public string Code { get; set; }

        [Required]
        [Display(Name="KPI Name")]
        public string Name { get; set; }

        [Display(Name="Pillar")]
        public int? PillarId { get; set; } //to make this nullable we need to include it
        public Pillar Pillar { get; set; }
        public List<SelectListItem> PillarList { get; set; }

        [Required]
        [Display(Name = "Level")]
        public int LevelId { get; set; }
        public List<SelectListItem> LevelList { get; set; }
        public Level Level { get; set; }

        [Display(Name = "Accountability")]
        public int? RoleGroupId { get; set; }
        public List<SelectListItem> RoleGroupList { get; set; }
        public RoleGroup RoleGroup { get; set; } 

        [Required]
        [Display(Name = "Type")]
        public int TypeId { get; set; }
        public List<SelectListItem> TypeList { get; set; }
        public Type Type { get; set; }

        [Display(Name = "Group")]
        public int? GroupId { get; set; }
        public List<SelectListItem> GroupList { get; set; }
        public Group Group { get; set; }

        [Display(Name = "Is Economic Model")]
        public bool IsEconomic { get; set; }

        [Required]
        [Display(Name = "Ordering")]
        public int Order { get; set; }

        [Display(Name = "PMS Formula Ytd")]
        public YtdFormula YtdFormula { get; set; }
        public string YtdFormulaValue { get; set; }
        public List<SelectListItem> YtdFormulaList { get; set; }

        [Display(Name="Measurement")]
        public int? MeasurementId { get; set; }
        public List<SelectListItem> MeasurementList { get; set; }
        public Measurement Measurement { get; set; }

        [Required]
        [Display(Name = "Method Value")]
        public int MethodId { get; set; }
        public List<SelectListItem> MethodList { get; set; }
        public Method Method { get; set; }

        public int? ConversionId { get; set; }
        public Conversion Conversion { get; set; }
        public FormatInput FormatInput { get; set; }

        //public Periode Periode { get; set; }

        [Display(Name = "Period Input")]
        public string PeriodeValue { get; set; }
        public List<SelectListItem> PeriodeList { get; set; }
        public PeriodeType PeriodeType { get; set; }

        public string Remark { get; set; }

        public ICollection<KpiRelationModel> RelationModels { get; set; }
        public List<SelectListItem> KpiList { get; set; }
        public DateTime? Value { get; set; }
        //public ICollection<KpiTarget> KpiTargets { get; set; }
        //public ICollection<KpiAchievement> KpiAchievements { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}