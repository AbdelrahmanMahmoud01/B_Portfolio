namespace Profile.Ui.Helpers;

public static class UploadedFileProcess
{
    public static bool IsPhotoExsist(string photo)
    {
        return photo != string.Empty && photo != null;
    }

    public static string UploadFile(IWebHostEnvironment environment , IFormFile file)
    {
        string uniqueFileName = null;

        if (file != null)
        {
            string uploadsFolder = Path.Combine(environment.WebRootPath, "img");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
        }

        return uniqueFileName;
    }

    public static void DeleteOldFile(IWebHostEnvironment environment, string file)
    {
        string filePath = Path.Combine(environment.WebRootPath, "img", file);
        File.Delete(filePath);
    }
}
