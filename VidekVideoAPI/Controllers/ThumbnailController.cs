using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VidekVideoAPI.Models;

namespace VidekVideoAPI.Controllers
{
    [Route("api/thumbnail")]
    [ApiController]
    public class ThumbnailController : ControllerBase
    {
        private readonly VidekVideoAPIContext _context;

        public ThumbnailController(VidekVideoAPIContext context)
        {
            _context = context;
        }

        // GET: api/Thumbnail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThumbnailViewItem>>> GetThumbnailViewItem()
        {
            return await _context.ThumbnailViewItem.ToListAsync();
        }

        // GET: api/Thumbnail
        [HttpGet("{id}")]
        public async Task<ActionResult<ThumbnailViewItem>> GetThumbnailViewItem(int id)
        {

            var thumbnailViewItem = await _context.ThumbnailViewItem.Where(tn => tn.VideoId == id).FirstAsync();

            if (thumbnailViewItem == null)
            {
                return NotFound();
            }

            return thumbnailViewItem;
        }


        private bool ThumbnailViewItemExists(int id)
        {
            return _context.ThumbnailViewItem.Any(e => e.VideoId == id);
        }
    }
}
