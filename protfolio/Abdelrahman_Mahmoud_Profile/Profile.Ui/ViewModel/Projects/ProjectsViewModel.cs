namespace Profile.Ui.ViewModel.Projects;

public abstract class ProjectsViewModel
{
    [Required]
    public string? ProjectTitle { get; set; }

    [Required]
    public string? ProjectDescription { get; set; }

    [Required]
    public string? SiteUrl { get; set; }

    [Required]
    public string? ProjectGitHubLink { get; set; }

    [DisplayName("Photo")]
    [AllowedExtensions(new string[] { ".jpg", ".png" })]
    [MaxFileSize(5 * 1024 * 1024)]
    public IFormFile? UploadedPhoto { get; set; }
}
