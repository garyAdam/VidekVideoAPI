using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VidekVideoAPI.Models;
using VidekVideoAPI.Services;

namespace VidekVideoAPI.Controllers
{
    [Route("api/video")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly VidekVideoAPIContext _context;

        public VideoController(VidekVideoAPIContext context)
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

        [HttpGet("{id}/stream")]
        public async Task<ActionResult> GetVideoContent(int id)
        {
            var video = await _context.Video.FindAsync(id);

            FileInfo fileInfo = new FileInfo(video.SourcePath);
            if (fileInfo.Exists)
            {
                FileStream fs = new FileStream(video.SourcePath, FileMode.Open);

                return new FileStreamResult(fs, new MediaTypeHeaderValue("video/mp4").MediaType);

            }

            return BadRequest();
        }

        // GET: api/Videos/5/thumbnail
        [HttpGet("{id}/thumbnail")]
        public async Task<ActionResult> GetVideoThumbnail(int id)
        {
            var thumbnail = await _context.Thumbnails.FindAsync(id);

            if (thumbnail == null)
            {
                return NotFound();
            }

            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(),thumbnail.SourcePath), "image/jpeg");
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

                Video video = new Video
                {
                    Title = title,
                    Descirption = description
                };

                try
                {
                    var targetFolder = Path.Combine("Resources", "Videos");
                    VideoStorage videoStorage = new VideoStorage();
                    fullPath = videoStorage.StoreVideo(targetFolder, file, video.Id);

                    var thumbnailFolder = Path.Combine("Resources", "Thumbnails");
                    thumbnailFullPath = Path.Combine(thumbnailFolder, video.Id + "thumbnail_" + file.FileName + ".jpg");
                    ThumbnailExtractor thumbnailExtractor = new ThumbnailExtractor();
                    thumbnailExtractor.ExtractThumbnail(fullPath, thumbnailFullPath);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
                await UpdateContext(fullPath, thumbnailFullPath, video);
                return Ok(new { fullPath });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }

        }

        private async Task UpdateContext(string fullPath, string thumbnailFullPath, Video video)
        {
            video.SourcePath = fullPath;
            Thumbnail thumbnail = new Thumbnail();
            thumbnail.VideoID = video.Id;
            thumbnail.SourcePath = thumbnailFullPath;
            ThumbnailViewItem thumbnailViewItem = new ThumbnailViewItem();
            thumbnailViewItem.Title = video.Title;
            thumbnailViewItem.VideoId = video.Id;
            thumbnailViewItem.URL = Request.Path + "/" + video.Id + "/thumbnail";

            _context.ThumbnailViewItem.Add(thumbnailViewItem);
            _context.Thumbnails.Add(thumbnail);
            _context.Video.Add(video);
            await _context.SaveChangesAsync();
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
