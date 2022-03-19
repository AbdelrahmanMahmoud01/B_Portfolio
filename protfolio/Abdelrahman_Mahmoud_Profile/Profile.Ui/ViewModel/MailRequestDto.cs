namespace Profile.Ui.ViewModel
{
    public class MailRequestDto
    {
        [Required(ErrorMessage ="Please type your email")]
        [EmailAddress]
        public string? ToEmail { get; set; }


        [Required(ErrorMessage = "Please type the subject")]
        public string? Subject { get; set; }


        [Required(ErrorMessage = "Please type the message")]
        public string? Body { get; set; }


        public IList<IFormFile> Attachments { get; set; }



        [Required(ErrorMessage = "Please type your phone number")]
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}