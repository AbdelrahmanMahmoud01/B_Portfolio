namespace Profile.Ui.Controllers;
public class OwnerController : Controller
{

    private readonly IMediator<Owner> _mediator;

    public OwnerController(IMediator<Owner> mediator)
    {
        _mediator = mediator;
    }

    // GET: OwnerController
    public async Task<ActionResult> Index() =>
        View(await _mediator.UnitOfWork.Entity.GetAllAsync());


    // GET: OwnerController/Details/5
    public async Task<ActionResult> Details(int id) =>
        View(await GetOwnerAsync(id));


    // GET: OwnerController/Create
    public ActionResult Create() => View();

    // POST: OwnerController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateOwnerViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        Owner owner = _mediator.Mapper.Map<Owner>(model);

        if (model.UploadedPhoto is not null)
            owner.Avatar = UploadedFileProcess.UploadFile(_mediator.Environment, model.UploadedPhoto);

        await _mediator.UnitOfWork.Entity.CreateAsync(owner);
        await _mediator.UnitOfWork.SaveAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: OwnerController/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
        var ownerInfo = await GetOwnerAsync(id);

        var owner = _mediator.Mapper.Map<UpdateOwnerViewModel>(ownerInfo);

        return View(owner);
    }

    // POST: OwnerController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(UpdateOwnerViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var owner = _mediator.Mapper.Map<Owner>(model);

        if (model.UploadedPhoto is not null)
        {
            if (model.Avatar is not null)
                UploadedFileProcess.DeleteOldFile(_mediator.Environment, model.Avatar);

            owner.Avatar = UploadedFileProcess.UploadFile(_mediator.Environment, model.UploadedPhoto);
        }

        _mediator.UnitOfWork.Entity.UpdateAsync(owner);
        await _mediator.UnitOfWork.SaveAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: OwnerController/Delete/5
    public async Task<ActionResult> Delete(int id) =>
        View(await GetOwnerAsync(id));

    
    // POST: OwnerController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ConfrimDelete(int id)
    {
        var owner = await GetOwnerAsync(id);

        if (UploadedFileProcess.IsPhotoExsist(owner.Avatar))
            UploadedFileProcess.DeleteOldFile(_mediator.Environment, owner.Avatar!);

        _mediator.UnitOfWork.Entity.DeleteAsync(owner);
        await _mediator.UnitOfWork.SaveAsync();

        return RedirectToAction(nameof(Index));

    }

    private async Task<Owner> GetOwnerAsync(int id) =>
        await _mediator.UnitOfWork.Entity.GetByIdAsync(id);

}
