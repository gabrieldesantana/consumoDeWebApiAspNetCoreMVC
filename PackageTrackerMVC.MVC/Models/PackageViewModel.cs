using System.ComponentModel.DataAnnotations;

namespace PackageTrackerMVC.MVC.Models
{
    public class PackageViewModel
    {
        public int Id { get;  set; }
        public string Code { get;  set; }
        public string Title { get;  set; }
        public decimal Weight { get;  set; }
        public bool Delivered { get;  set; }
        public DateTime PostedAt { get;  set; }
        public List<PackageUpdateViewModel> Updates { get; set;}
        public bool Actived { get;  set; }
    }
}