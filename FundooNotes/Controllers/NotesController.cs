using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    public class NotesController : ControllerBase
    {
        private readonly INoteManager manager;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public NotesController(INoteManager manager, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.manager = manager;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
        [Authorize]
        [HttpPost]
        [Route("api/addnote")]
        public IActionResult AddNotes([FromBody] UpdateModel updateModel)
        {
            try
            {
                var result = this.manager.AddNotes(updateModel);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "Note Added Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Note cannot be Deleted successfully" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
        [HttpDelete]
        [Route("api/deletenote")]
        public IActionResult DeleteNode(int noteId)
        {
            try
            {
                var result = this.manager.DeleteNode(noteId);
                if (result != 0)
                {
                    return this.Ok(new { Status = true, Message = "Note Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Note cannot be deleted successfully" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
        [HttpPut]
        [Route("api/updatenote")]
        public IActionResult UpdateNotes(UpdateModel updateModel, int Id)
        {
            try
            {
                var result = this.manager.UpdateNotes(updateModel, Id);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "Note Updated Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Note cannot be deleted successfully", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
        [HttpGet]
        [Route("api/retrievenotes")]
        public IActionResult RetriveNotes()
        {
            try
            {
                var result = this.manager.RetriveNotes();
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Notes Retrieved Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Nots Not Retrieved Successfully", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
        [HttpGet]
        [Route("api/istrash")]
        public IActionResult IsTrash(int NoteId)
        {
            try
            {
                var result = this.manager.IsTrash(NoteId);
                if (result != false)
                {
                    return this.Ok(new { Status = true, Message = "Notes is in trash", Data = result });
                }
                else
                {
                    return this.Ok(new { Status = true, Message = "Nots is not in trash", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
        [HttpGet]
        [Route("api/ispin")]
        public IActionResult IsPin(int NoteId)
        {
            try
            {
                var result = this.manager.IsPin(NoteId);
                if (result != false)
                {
                    return this.Ok(new { Status = true, Message = "Notes is Pin", Data = result });
                }
                else
                {
                    return this.Ok(new { Status = true, Message = "Nots is not Pin", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
        [HttpGet]
        [Route("api/isarchieve")]
        public IActionResult IsArchive(int NoteId)
        {
            try
            {
                var result = this.manager.IsArchive(NoteId);
                if (result != false)
                {
                    return this.Ok(new { Status = true, Message = "Notes is Archieve", Data = result });
                }
                else
                {
                    return this.Ok(new { Status = true, Message = "Nots is not Archieve", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
        [HttpGet]
        [Route("api/getbyid")]
        public IActionResult GetById(int UserId)
        {
            try
            {
                var result = this.manager.GetById(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Notes Retrieved Successfully", Data = result });
                }
                else
                {
                    return this.Ok(new { Status = true, Message = "Notes Not Retrieved Successfully", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
        [HttpGet("redis")]
        public async Task<IActionResult> RetreiveALLNotes()
        {
            var cacheKey = "NotesList";
            string serializedNotesList;
            var NotesList = new List<NotesEntity>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
            }
            else
            {
                NotesList = await this.manager.RetriveNotes().ToListAsync();
                serializedNotesList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(NotesList);
        }
    }
}
