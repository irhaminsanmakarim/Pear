using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.Kpi
{
    public class GetKpiResponse : BaseResponse
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int? PillarId { get; set; } //to make this nullable we need to include it

        public int LevelId { get; set; }

        public int? RoleGroupId { get; set; }

        public int TypeId { get; set; }

        public int? GroupId { get; set; }

        public bool IsEconomic { get; set; }

        public int Order { get; set; }

        public YtdFormula YtdFormula { get; set; }
        public string YtdFormulaValue { get; set; }

        public int? MeasurementId { get; set; }

        public int MethodId { get; set; }

        public int? ConversionId { get; set; }
        public Conversion Conversion { get; set; }
        public FormatInput FormatInput { get; set; }

        public string PeriodeValue { get; set; }

        public string Remark { get; set; }

        public ICollection<KpiRelationModel> RelationModels { get; set; }
        public DateTime? Value { get; set; }

        public bool IsActive { get; set; }
    }
}
