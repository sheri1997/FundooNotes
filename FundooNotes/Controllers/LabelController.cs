using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooNotes.Controllers
{
    public class LabelController : Controller
    {
        private readonly ILabelManager manager;
        public LabelController(ILabelManager manager)
        {
            this.manager = manager;
        }
        [HttpPost]
        [Route("api/addlabel")]
        public IActionResult CreateLabel(LabelModel labelModel, int noteid, int userId)
        {
            try
            {
                var token = this.User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
                var result = this.manager.CreateLabel(labelModel, noteid, userId);
                if (result == null)
                {
                    return this.Ok(new { Status = true, Message = "Label Added Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Label cannot be Added successfully" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult DeleteLabel(LabelEntity labelEntity, int id)
        {
            try
            {
                var token = this.User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
                var result = this.manager.DeleteLabel(labelEntity, id);
                if (result == null)
                {
                    return this.Ok(new { Status = true, Message = "Label Deleted Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Label cannot be Deleted successfully" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}