namespace Profile.Ui.Profiles.Projects;

public class ProjectsProfile : AutoMapper.Profile
{
    public ProjectsProfile()
    {
        CreateMap<Core.Entites.Projects, CreateProjectViewModel>().ReverseMap();
        CreateMap<Core.Entites.Projects, UpdateProjectViewModel>().ReverseMap();
    }
}
