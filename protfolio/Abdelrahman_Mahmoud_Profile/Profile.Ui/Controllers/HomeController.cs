using NToastNotify;
using Profile.Ui.Services;

namespace Profile.Ui.Controllers;
public class HomeController : Controller
{
    private readonly DataContext context;
    private readonly IWebHostEnvironment env;
    private readonly IMailingService _mailingService;
    private readonly IToastNotification _toastNotification;

    public HomeController(DataContext context, IWebHostEnvironment env ,
        IMailingService mailingService , IToastNotification toastNotification)
    {
        this.context = context;
        this.env = env;
        _mailingService = mailingService;
        _toastNotification = toastNotification;
    }

    public ActionResult Index()
    {
        var homeViewModel = new HomeViewModels
        {
            Owner = context.OwnerInfo.ToList().First(),
            Projects = context.Projects.ToList(),
            About = context.OwnerAbout.ToList().First()
        };
        return View(homeViewModel);
    }

    public async Task<IActionResult> DownloadCV(UpdateAboutViewModel model)
    {
        var cv = await context.OwnerAbout.FirstOrDefaultAsync();
        var path = Path.Combine(env.WebRootPath, $@"img\{cv.CvUrl}");

        var memory = new MemoryStream();

        using var stream = new FileStream(path, FileMode.Open);
        await stream.CopyToAsync(memory);
        memory.Position = 0;

        return File(memory,"application/pdf",Path.GetFileName(path));
    }

    [HttpPost]
    public async Task<ActionResult> Index(HomeViewModels dto)
    {
        //if (!ModelState.IsValid)
        //{
        //    dto.Owner = context.OwnerInfo.ToList().First();
        //    dto.Projects = context.Projects.ToList();
        //    dto.About = context.OwnerAbout.ToList().First();
        //    return View(dto);
        //}

        await _mailingService.SendEmailAsync("abdu_boro@yahoo.com",
        dto.Mail.Subject, $"From : {dto.Mail.ToEmail}\n" +
        $"Phone : {dto.Mail.PhoneNumber}\n" +
        $"Message : {dto.Mail.Body}", dto.Mail.Attachments);
        //_toastNotification.AddSuccessToastMessage("Thanks for contact me ");
        return RedirectToAction(nameof(Index));
    }
}
