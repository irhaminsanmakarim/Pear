

using System.Collections.Generic;
namespace DSLNG.PEAR.Web.ViewModels.Template
{
    public class TemplateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RefershTime { get; set; } //in minutes
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public IList<RowViewModel> LayoutRows { get; set; }

        public class RowViewModel {
            public int Index { get; set; }
            public IList<ColumnViewModel> LayoutColumns {get;set;}
        }

        public class ColumnViewModel {
            public int Index { get; set; }
            public float Width { get; set; }
            public float Height { get; set; }
            public int ArtifactId { get; set; }
        }
    }
}