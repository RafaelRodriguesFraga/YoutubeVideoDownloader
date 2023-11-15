using ConsoleTables;
using System.Data;
using System.Text;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using YoutubeVideoDownloader;

class Program
{

    static async Task Main(string[] args)
    {
        int option;

        do
        {
            Console.Title = "Youtube Video Downloader by Rafael Fraga";
            Console.WriteLine("===================================================");
            Console.WriteLine("=          Youtube Video Downloader               =");
            Console.WriteLine("=           Rafael Rodrigues Fraga                =");
            Console.WriteLine("=    https://github.com/RafaelRodriguesFraga      =");
            Console.WriteLine("===================================================");
            Console.WriteLine();

            Console.WriteLine("1 - Download a video");
            Console.WriteLine("2 - Download a playlist");
            Console.WriteLine("0 - Exit");
            Console.WriteLine();

            Console.Write("Option: ");        
            option = Convert.ToInt32(Console.ReadLine());           

            switch (option)
            {
                case 1:

                    Console.Write("Video Url: ");
                    var videoUrl = Console.ReadLine();                  

                    await DownloadVideo.DownloadOneAsync(videoUrl);
                    break;

                case 2:
                    Console.Write("Playlist Url: ");
                    var playlistUrl = Console.ReadLine();

                    await DownloadVideo.DownloadPlaylistAsync(playlistUrl);
                    break;

                case 0:
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid option. Chosse betweem 0 and 2");
                    Console.Clear();
                    break;

            }

        } while (option != 0);

    }


}