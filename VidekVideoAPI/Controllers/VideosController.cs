using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VidekVideoAPI.Models;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;

namespace VidekVideoAPI.Controllers
{
    [Route("api/video")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly VidekVideoAPIContext _context;

        public VideosController(VidekVideoAPIContext context)
        {
            _context = context;
        }

        // GET: api/Videos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Video>>> GetVideo()
        {
            return await _context.Video.ToListAsync();
        }

        // GET: api/Videos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Video>> GetVideo(int id)
        {
            var video = await _context.Video.FindAsync(id);

            if (video == null)
            {
                return NotFound();
            }

            return video;
        }

        // POST: api/Videos
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult> PostVideo()
        {

            try
            {
                var file = Request.Form.Files[0];
                var title = Request.Form["Title"];
                var description = Request.Form["Description"];
                Video video = new Video();
                video.Title = title;
                var folderName = Path.Combine("Resources", "Videos");
                var thumbnailFolder = Path.Combine("Resources", "Thumbnails");
                var ffmpegPath = Path.Combine("Resources", "ffmpeg", "bin","ffmpeg.exe");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var thumbnailPath = Path.Combine(Directory.GetCurrentDirectory(), thumbnailFolder);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(folderName, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    video.SourcePath = fullPath;
                    MediaFile videofile = new MediaFile(fullPath);
                    MediaFile imagefile = new MediaFile(Path.Combine(thumbnailPath, "thumbnail_" + fileName + ".jpg"));
                    using (Engine engine = new Engine(ffmpegPath))
                    {
                        engine.GetMetadata(videofile);
                        engine.GetThumbnail(videofile, imagefile, new ConversionOptions { Seek = TimeSpan.FromSeconds(videofile.Metadata.Duration.TotalSeconds / 2) });
                    }


                    _context.Video.Add(video);
                    await _context.SaveChangesAsync();
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }

        }


        // DELETE: api/Videos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Video>> DeleteVideo(int id)
        {
            var video = await _context.Video.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }

            _context.Video.Remove(video);
            await _context.SaveChangesAsync();

            return video;
        }
    }
}
