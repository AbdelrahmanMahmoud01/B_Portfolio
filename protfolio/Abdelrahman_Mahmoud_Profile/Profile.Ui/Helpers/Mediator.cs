namespace Profile.Ui.Helpers;

public class Mediator<T> : IMediator<T> where T : class
{

    private readonly IMapper _mapper;
    private readonly IUnitOfWork<T> _unitOfWork;
    private readonly IWebHostEnvironment _environment;

    public Mediator(IMapper mapper , IUnitOfWork<T> unitOfWork , IWebHostEnvironment environment)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _environment = environment;
    }

    public IMapper Mapper
    {
        get { return _mapper; }
    } 

    public IUnitOfWork<T> UnitOfWork
    {
        get { return _unitOfWork; }
    }

    public IWebHostEnvironment Environment
    {
        get { return _environment; }
    }

}
