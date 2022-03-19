using Profile.Ui.Helpers;

namespace Profile.Ui.ViewModel.Owner;

public abstract class OwnerViewModel
{
    [Required()]
    public string? FullName { get; set; }
    [Required()]
    public string? JobDescription { get; set; }


    //[Required()]
    [DisplayName("Photo")]
    [AllowedExtensions(new string[] { ".jpg", ".png" })]
    [MaxFileSize(5 * 1024 * 1024)]
    public IFormFile? UploadedPhoto { get; set; }
}
