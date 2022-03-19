namespace Profile.Core.Entites;

public class Projects : EntityBase
{
    public string? ProjectTitle { get; set; }
    public string? ProjectPhotoUrl { get; set; }
    public string? ProjectDescription { get; set; }
    public string? SiteUrl { get; set; }
    public string? ProjectGitHubLink { get; set; }
}
