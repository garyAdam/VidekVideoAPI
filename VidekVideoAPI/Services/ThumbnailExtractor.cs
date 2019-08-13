using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VidekVideoAPI.Services
{
    public class ThumbnailExtractor
    {
        public void ExtractThumbnail(string videoPath, string imagePath)
        {
            var ffmpegPath = Path.Combine("Resources", "ffmpeg", "bin", "ffmpeg.exe");

            MediaFile videofile = new MediaFile(videoPath);
            MediaFile imagefile = new MediaFile(imagePath);
            using (Engine engine = new Engine(ffmpegPath))
            {
                engine.GetMetadata(videofile);
                engine.GetThumbnail(videofile, imagefile, new ConversionOptions { Seek = TimeSpan.FromSeconds(videofile.Metadata.Duration.TotalSeconds / 2) });
            }
        }
    }
}
