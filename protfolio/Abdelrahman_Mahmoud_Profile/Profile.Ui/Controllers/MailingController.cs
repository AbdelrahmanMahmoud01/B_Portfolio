using Profile.Ui.Services;
using Profile.Ui.ViewModel;
using Profile.Ui.Controllers;

namespace SendEmailsWithDotNet5.Controllers
{
    public class MailingController : Controller
    {
        private readonly IMailingService _mailingService;

        public MailingController(IMailingService mailingService)
        {
            _mailingService = mailingService;
        }

        [HttpGet]
        public ActionResult SendMail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendMail(MailRequestDto dto)
        {
            await _mailingService.SendEmailAsync("abdu_boro@yahoo.com",dto.Subject,
                dto.Body, dto.Attachments);
            return RedirectToAction(nameof(Index),nameof(HomeController));
        }

        [HttpPost("welcome")]
        public async Task<IActionResult> SendWelcomeEmail([FromBody] WelcomeRequestDto dto)
        {
            var filePath = $"{Directory.GetCurrentDirectory()}\\Templates\\EmailTemplate.html";
            var str = new StreamReader(filePath);

            var mailText = str.ReadToEnd();
            str.Close();

            mailText = mailText.Replace("[username]", dto.UserName).Replace("[email]", dto.Email);

            await _mailingService.SendEmailAsync(dto.Email, "Welcome to our channel", mailText);
            return Ok();
        }
    }
}
