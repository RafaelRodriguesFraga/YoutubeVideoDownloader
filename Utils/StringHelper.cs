namespace YoutubeVideoDownloader.Utils
{
    public static class StringHelper
    {
        public static string SanitizeFileName(string fileName)
        {
            return string.Join("_", fileName.Split(Path.GetInvalidFileNameChars()));
        }

        public static string GetFilePath(string directory, string title)
        {
            if (string.IsNullOrEmpty(directory))
            {
                directory = Path.Combine(@"C:\Users", Environment.UserName, "Videos");
            }

            return Path.Combine(directory, SanitizeFileName(title) + ".mp4");
        }

        public static string RemoveWhiteSpaces(string folderName)
        {
            return folderName.Replace(" ", "_");
        }
    }
}
