

namespace DSLNG.PEAR.Services.Requests.Pillar
{
    public class CreatePillarRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Order { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
    }
}
