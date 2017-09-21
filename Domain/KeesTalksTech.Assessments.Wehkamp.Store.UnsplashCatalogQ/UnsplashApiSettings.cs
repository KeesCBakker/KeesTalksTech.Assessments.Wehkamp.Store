using System.ComponentModel.DataAnnotations;

namespace KeesTalksTech.Assessments.Wehkamp.Store.UnsplashCatalog
{
    public class UnsplashApiSettings
    {
        [Required]
        public string ApplicationId { get; set; }

        [Required]
        public string ApplicationSecret { get; set; }
    }
}
