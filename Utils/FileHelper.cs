namespace YoutubeVideoDownloader.Utils
{
    public static class FileHelper
    {
        public static string CreateDirectory(string folderName)
        {
            var path = Path.Combine(@"C:\Users", Environment.UserName, "Videos", folderName);
            var directoryDoesNotExist = !Directory.Exists(path);
            if (directoryDoesNotExist)
            {
                Directory.CreateDirectory(path);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Folder created!");
                Console.ResetColor();

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Folder already exists!");
                Console.ResetColor();
            }
            return path;
        }

    }
}
