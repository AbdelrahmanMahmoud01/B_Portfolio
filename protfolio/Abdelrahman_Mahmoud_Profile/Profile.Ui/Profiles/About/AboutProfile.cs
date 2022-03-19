namespace Profile.Ui.Profiles.About;

public class AboutProfile : AutoMapper.Profile
{
    public AboutProfile()
    {

        CreateMap<Core.Entites.OwnerAbout, CreateAboutViewModel>().ReverseMap();
        CreateMap<Core.Entites.OwnerAbout, UpdateAboutViewModel>().ReverseMap();
    }
}
