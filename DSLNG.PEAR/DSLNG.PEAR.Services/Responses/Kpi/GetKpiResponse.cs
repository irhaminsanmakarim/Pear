using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.Kpi
{
    public class GetKpiResponse
    {
        public class Kpi
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public int? PilarId { get; set; } //to make this nullable we need to include it
            public Pilar Pilar { get; set; }
            public Level Level { get; set; }
            public RoleGroup RoleGroup { get; set; }
            public Type Type { get; set; }
            public Group Group { get; set; }
            public string IsEconomic { get; set; }
            public int Order { get; set; }
            public YtdFormula YtdFormula { get; set; }
            public Measurement Measurement { get; set; }
            public Method Method { get; set; }
            public int? ConversionId { get; set; }
            public Conversion Conversion { get; set; }
            public FormatInput FormatInput { get; set; }
            public Periode Periode { get; set; }
            public string Remark { get; set; }

            public bool IsActive { get; set; }
            public User.UserResponse CreatedBy { get; set; }
            public User.UserResponse UpdatedBy { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime UpdatedDate { get; set; }
        }

        public class Pilar
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public int Order { get; set; }
            public string Color { get; set; }
            public string Icon { get; set; }
            public string Remark { get; set; }
            public bool IsActive { get; set; }
        }

        public class RoleGroup
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Level Level { get; set; }
            public string Icon { get; set; }
            public string Remark { get; set; }
            public bool IsActive { get; set; }
        }

        public class Level
        {
            public int Id { get; set; }

            public string Code { get; set; }
            public string Name { get; set; }
            public int Number { get; set; }
            public string Remark { get; set; }

            public bool IsActive { get; set; }
        }

        public class Group
        {
            public int Id { get; set; }

            public string Name { get; set; }
            public int Order { get; set; }
            public string Remark { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
