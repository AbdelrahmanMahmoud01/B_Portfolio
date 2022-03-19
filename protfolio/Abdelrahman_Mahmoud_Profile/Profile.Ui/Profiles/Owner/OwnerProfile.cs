namespace Profile.Ui.Profiles.Owner;

public class OwnerProfile : AutoMapper.Profile
{
    public OwnerProfile()
    {
        CreateMap<Core.Entites.Owner , CreateOwnerViewModel >().ReverseMap();
        CreateMap<Core.Entites.Owner, UpdateOwnerViewModel>().ReverseMap();
    }
}
