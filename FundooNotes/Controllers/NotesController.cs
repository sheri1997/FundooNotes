using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    public class NotesController : ControllerBase
    {
        private readonly INoteManager manager;
        public NotesController(INoteManager manager)
        {
            this.manager = manager;
        }
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
        [HttpPost]
        [Route("api/deletenote")]
        public IActionResult DeleteNode(int noteId)
        {
            try
            {
                var result = this.manager.DeleteNode(noteId);
                if (result != true)
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
        [HttpPost]
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
        [HttpPost]
        [Route("api/retrievenotes")]
        public IActionResult RetriveNotes()
        {
            try
            {
                var result = this.manager.RetriveNotes();
                if(result == null)
                {
                    return this.Ok(new { Status = true, Message = "Notes Retrieved Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Nots Not Retrieved Successfully", Data = result });
                }
            }
            catch(Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message});
            }
        }
    }
}
