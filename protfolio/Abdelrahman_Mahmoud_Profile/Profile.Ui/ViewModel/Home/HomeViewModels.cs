namespace Profile.Ui.ViewModel.Home;

public class HomeViewModels
{
    public Core.Entites.Owner Owner { get; set; } = new();

    public List<Core.Entites.Projects> Projects { get; set; } = new();
    public Core.Entites.OwnerAbout About { get; set; } = new();
    public MailRequestDto Mail { get; set; }
}
