namespace Profile.Ui.Controllers;
public class ProjectsController : Controller
{
    private readonly IMediator<Projects> _mediator;

    public ProjectsController(IMediator<Projects> mediator)
    {
        _mediator = mediator;
    }
    private async Task<Projects> GetProjectAsync(int id) =>
        await _mediator.UnitOfWork.Entity.GetByIdAsync(id);

    // GET: ProjectController
    public async Task<ActionResult> Index() =>
        View(await _mediator.UnitOfWork.Entity.GetAllAsync());

    // GET: ProjectController/Details/5
    public async Task<ActionResult> Details(int id) =>
        View(await GetProjectAsync(id));


    // GET: ProjectController/Create
    public ActionResult Create() => View();

    // POST: ProjectController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateProjectViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var projects = _mediator.Mapper.Map<Projects>(model);

        if (model.UploadedPhoto is not null)
            projects.ProjectPhotoUrl = 
                UploadedFileProcess.UploadFile(_mediator.Environment, model.UploadedPhoto);

        await _mediator.UnitOfWork.Entity.CreateAsync(projects);
        await _mediator.UnitOfWork.SaveAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: ProjectController/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
        var project = await GetProjectAsync(id);

        var model = _mediator.Mapper.Map<UpdateProjectViewModel>(project);

        return View(model);
    }

    // POST: ProjectController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(UpdateProjectViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var project = _mediator.Mapper.Map<Projects>(model);

        if (model.UploadedPhoto is not null)
        {
            if (model.ProjectPhotoUrl is not null)
                UploadedFileProcess.DeleteOldFile(_mediator.Environment, model.ProjectPhotoUrl);

            project.ProjectPhotoUrl = UploadedFileProcess.UploadFile(_mediator.Environment, model.UploadedPhoto);
        }

        _mediator.UnitOfWork.Entity.UpdateAsync(project);
        await _mediator.UnitOfWork.SaveAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: ProjectController/Delete/5
    public async Task<ActionResult> Delete(int id) =>
        View(await GetProjectAsync(id));

    // POST: ProjectController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ConfrimDelete(int id)
    {
        var project = await GetProjectAsync(id);

        if (UploadedFileProcess.IsPhotoExsist(project.ProjectPhotoUrl))
            UploadedFileProcess.DeleteOldFile(_mediator.Environment, project.ProjectPhotoUrl!);

        _mediator.UnitOfWork.Entity.DeleteAsync(project);
        await _mediator.UnitOfWork.SaveAsync();

        return RedirectToAction(nameof(Index));
    }
}
