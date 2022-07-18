using System.ComponentModel.DataAnnotations;

namespace PackageTrackerMVC.MVC.Models
{
    public class PackageUpdateViewModel
    {
        public int Id { get; private set; }
        public int PackageId { get; private set; }

        [Required]
        public string Status { get; private set; }
        public DateTime UpdateDate { get; private set; }
    }
}