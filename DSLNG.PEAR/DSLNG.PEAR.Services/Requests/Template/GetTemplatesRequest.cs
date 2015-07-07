

namespace DSLNG.PEAR.Services.Requests.Template
{
    public class GetTemplatesRequest
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool OnlyCount { get; set; }
    }
}
