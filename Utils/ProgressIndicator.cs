using System;

namespace YoutubeVideoDownloader.Utils
{
    public class ProgressIndicator : IProgress<double>, IDisposable
    {
        private readonly int _positionLeft;
        private readonly int _positionTop;

        public ProgressIndicator()
        {
            _positionLeft = Console.CursorLeft;
            _positionTop = Console.CursorTop;
        }

        public void Report(double value)
        {
            Console.SetCursorPosition(_positionLeft, _positionTop);
            Console.Write($"Downloading... {value:P0}");
        }

        public void Dispose()
        {
            Console.SetCursorPosition(_positionLeft, _positionTop);
        }
    }
}
