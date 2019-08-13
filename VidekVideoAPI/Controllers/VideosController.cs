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
        public async Task<ActionResult> PostVideo(Video video)
        {

            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Videos");
                var thumbnailFolder = Path.Combine("Resources", "Thumbnails");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var thumbnailPath = Path.Combine(Directory.GetCurrentDirectory(), thumbnailFolder);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    var ffmpeg = new NReco.VideoConverter.FFMpegConverter();
                    ffmpeg.FFMpegExeName = "ffmpeg.exe"; // for Linux/OS-X: "ffmpeg" 
                    ffmpeg.FFMpegToolPath = "../../../../../ffmpeg/bin";
                    var fullThumbnailPath = Path.Combine(thumbnailFolder, "thumbnail_" + fileName);
                    ffmpeg.GetVideoThumbnail(fullPath, new FileStream(fullThumbnailPath, FileMode.Create)); ;
                    video.ThumbnailPath = fullThumbnailPath;
                    video.SourcePath = fullPath;
                    _context.Video.Add(video);
                    await _context.SaveChangesAsync();
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
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
