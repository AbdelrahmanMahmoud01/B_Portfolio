namespace Profile.Ui.Helpers;

public interface IMediator<T> where T : class
{
    IMapper Mapper { get; }
    IUnitOfWork<T> UnitOfWork { get; }
    IWebHostEnvironment Environment { get; }
}
