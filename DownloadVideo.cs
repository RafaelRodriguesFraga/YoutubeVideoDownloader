using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Exceptions;
using YoutubeExplode.Videos.Streams;
using YoutubeVideoDownloader.Utils;

namespace YoutubeVideoDownloader
{
    public static class DownloadVideo
    {
        static string filePath = string.Empty;
        static string folderName = string.Empty;
        static string directory = string.Empty;
        private const char YesOption = 'y';
        private const char NoOption = 'n';

        public static async Task DownloadOneAsync(string videoUrl)
        {
            try
            {
                var client = new YoutubeClient();
                var video = await client.Videos.GetAsync(videoUrl);

                var streamInfoSet = await client.Videos.Streams.GetManifestAsync(video.Id);
                var streamInfo = streamInfoSet.GetMuxedStreams().GetWithHighestVideoQuality();
                if (streamInfo is not null)
                {
                    var stream = await client.Videos.Streams.GetAsync(streamInfo);
                    filePath = StringHelper.GetFilePath("", video.Title);

                    Console.WriteLine();

                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Video Details");
                    Console.ResetColor();

                    Console.WriteLine($"Title: {video.Title} ");
                    Console.WriteLine($"Author: {video.Author}");
                    Console.WriteLine($"Duration: {video.Duration}");
                    Console.WriteLine($"Size: {streamInfo.Size}");
                    Console.WriteLine();

                    var progress = new ProgressIndicator();
                    await client.Videos.Streams.DownloadAsync(streamInfo, filePath, progress);

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    
                    Console.WriteLine($"Download succeeded. Your video will be at {filePath.Substring(0, 22)}");
                    Console.ResetColor();
                    Console.WriteLine();

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Cannot find data stream");
                    Console.ResetColor();

                    Console.WriteLine();

                    Console.WriteLine("Press any key to continue...");

                    Console.ReadKey();
                    Console.Clear();
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Invalid Video Url '{videoUrl}'");
                Console.ResetColor();

                Console.WriteLine();

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();

            }
        }
        public static async Task DownloadPlaylistAsync(string playlistUrl)
        {
            try
            {

                var youtube = new YoutubeClient();
                var playlist = await youtube.Playlists.GetAsync(playlistUrl);
                var videos = youtube.Playlists.GetVideosAsync(playlist.Id);
                var totalVideos = videos.CollectAsync().Result.Count;

                Console.WriteLine();

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Playlist Information");
                Console.ResetColor();

                Console.WriteLine($"Title: {playlist.Title} ");
                Console.WriteLine($"Author: {playlist.Author}");
                Console.WriteLine($"Total Vídeos: {totalVideos}");

                Console.WriteLine();

                Console.Write("Do you want to create a folder for the playlist? [y] / [n]: ");
                char folderOption = Convert.ToChar(Console.ReadLine());

                do
                {
                    switch (folderOption)
                    {
                        case YesOption:
                            Console.Write("Enter the folder name: ");
                            folderName = Console.ReadLine();

                            directory = FileHelper.CreateDirectory(folderName);                           
                            break;

                        case NoOption:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("No folder will be created!");
                            Console.ResetColor();

                            break;

                        default:
                            Console.WriteLine("Invalid option. Choose between y or n");
                            break;

                    }     

                } while (folderOption != YesOption && folderOption != NoOption);

                Console.WriteLine();
                var count = 0;

                await foreach (var video in videos)
                {
                    count++;
                    var streamInfoSet = await youtube.Videos.Streams.GetManifestAsync(video.Id);
                    var streamInfo = streamInfoSet.GetMuxedStreams().GetWithHighestVideoQuality();
                    if (streamInfo != null)
                    {
                        var stream = await youtube.Videos.Streams.GetAsync(streamInfo);

                        Console.WriteLine($"=== Video {count} Details ===");
                        Console.WriteLine($"Title: {video.Title}");
                        Console.WriteLine($"Author: {video.Author}");
                        Console.WriteLine($"Duration: {video.Duration}");
                        Console.WriteLine($"Size: {streamInfo.Size}");
                        Console.WriteLine();

                        filePath = StringHelper.GetFilePath(directory, video.Title);

                        var progress = new ProgressIndicator();
                        await youtube.Videos.Streams.DownloadAsync(streamInfo, filePath, progress);

                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Download secceeded!");
                        Console.ResetColor();
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Cannot find the video stream");
                        Console.ResetColor();
                    }

                }

                var downloadedFolder = folderOption == 'y' ? directory : filePath.Substring(0, 22);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"All videos have been downloaded. Your videos will be in {downloadedFolder}");
                Console.ResetColor();
                Console.WriteLine();

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();

            }
            catch (ArgumentException)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Invalid Playlist Url '{playlistUrl}'");
                Console.ResetColor();

                Console.WriteLine();

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

                Console.Clear();
            }
            catch (PlaylistUnavailableException)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Playlist not available or private");
                Console.ResetColor();

                Console.WriteLine();

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

                Console.Clear();
            }
        }
    }

}

