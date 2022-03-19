using Profile.Ui.ViewModel.OwnerAbout;

namespace Profile.Ui.Controllers;
public class AboutController : Controller
{
    private readonly IMediator<OwnerAbout> _mediator;

    public AboutController(IMediator<OwnerAbout> mediator)
    {
        _mediator = mediator;
    }
    // GET: AboutController
    public async Task<ActionResult> Index() =>
        View(await _mediator.UnitOfWork.Entity.GetAllAsync());

    // GET: AboutController/Details/5
    public ActionResult Details(int id) =>
        View(GetOwnerAboutAsync(id));

    private Task<OwnerAbout> GetOwnerAboutAsync(int id) =>
        _mediator.UnitOfWork.Entity.GetByIdAsync(id);


    // GET: AboutController/Create
    public ActionResult Create() => View();

    // POST: AboutController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateAboutViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var about = _mediator.Mapper.Map<OwnerAbout>(model);

        if (model.UploadedFile is not null)
            about.CvUrl = UploadedFileProcess.UploadFile(_mediator.Environment, model.UploadedFile);

        await _mediator.UnitOfWork.Entity.CreateAsync(about);
        await _mediator.UnitOfWork.SaveAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: AboutController/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
        var ownerAbout = await GetOwnerAboutAsync(id);

        var about = _mediator.Mapper.Map<UpdateAboutViewModel>(ownerAbout);

        return View(about);
    }
    // POST: AboutController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(UpdateAboutViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var owner = _mediator.Mapper.Map<OwnerAbout>(model);

        if (model.UploadedFile is not null)
        {
            if (model.CvUrl is not null)
                UploadedFileProcess.DeleteOldFile(_mediator.Environment, model.CvUrl);

            owner.CvUrl = UploadedFileProcess.UploadFile(_mediator.Environment, model.UploadedFile);
        }

        _mediator.UnitOfWork.Entity.UpdateAsync(owner);
        await _mediator.UnitOfWork.SaveAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: AboutController/Delete/5
    public async Task<ActionResult> Delete(int id) =>
         View(await GetOwnerAboutAsync(id));

    // POST: AboutController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ConfrimDelete(int id)
    {
        var owner = await GetOwnerAboutAsync(id);

        if (UploadedFileProcess.IsPhotoExsist(owner.CvUrl))
            UploadedFileProcess.DeleteOldFile(_mediator.Environment, owner.CvUrl!);

        _mediator.UnitOfWork.Entity.DeleteAsync(owner);
        await _mediator.UnitOfWork.SaveAsync();

        return RedirectToAction(nameof(Index));

    }
}
