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
using VidekVideoAPI.Services;

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
                string fullPath;
                string thumbnailFullPath;

                Video video = new Video();
                video.Title = title;
                video.Descirption = description;

                var targetFolder = Path.Combine("Resources", "Videos");
                VideoStorage videoStorage = new VideoStorage();
                try
                {
                    fullPath = videoStorage.StoreVideo(targetFolder,file, video.Id);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

                var thumbnailFolder = Path.Combine("Resources", "Thumbnails");
                thumbnailFullPath = Path.Combine(thumbnailFolder, "thumbnail_" + file.FileName + video.Id + ".jpg");


                ThumbnailExtractor thumbnailExtractor = new ThumbnailExtractor();
                try
                {
                    thumbnailExtractor.ExtractThumbnail(fullPath, thumbnailFullPath);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
                video.SourcePath = fullPath;
                video.ThumbnailPath = thumbnailFullPath;

                _context.Video.Add(video);
                await _context.SaveChangesAsync();
                return Ok(new { fullPath });
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
