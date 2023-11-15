namespace YoutubeVideoDownloader.Utils
{
    public static class StringHelper
    {
        public static string SanitizeFileName(string fileName)
        {
            return string.Join("_", fileName.Split(Path.GetInvalidFileNameChars()));
        }

        public static string GetFilePath(string title)
        {
            return Path.Combine(@"C:\Users", Environment.UserName, "Videos", SanitizeFileName(title) + ".mp4");
        }
    }
}
