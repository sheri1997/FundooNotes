using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FundooNotes.Controllers
{
    public class CollaboratorController : Controller
    {
        private readonly ICollaboratorManager manager;
        public CollaboratorController(ICollaboratorManager manager)
        {
            this.manager = manager;
        }
        [HttpPost]
        [Route("api/addcoloborator")]
        public IActionResult AddCollaborator(ColloboratorModel colloboratorModel)
        {
            try
            {
                var message = this.manager.AddCollaborator(colloboratorModel);
                if (message.Equals("New Collaborator added Successfully !"))
                {
                    return this.Ok(new { Status = true, Message = message, Data = colloboratorModel });
                }

                return this.BadRequest(new { Status = false, Message = message });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/deletecoloborator")]
        public IActionResult DeleteCollaborator(int id)
        {
            try
            {
                var message = this.manager.DeleteCollaborator(id);
                if (message.Equals("Collaborator deleted Successfully !"))
                {
                    return this.Ok(new { Status = true, Message = message, Data = id });
                }

                return this.BadRequest(new { Status = false, Message = message });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("api/getallcoloborator")]
        public IActionResult GetAllColaborators()
        {
            try
            {
                var message = this.manager.GetCollaborators();
                if (message.Equals("Collaborator deleted Successfully !"))
                {
                    return this.Ok(new { Status = true, Message = message });
                }

                return this.BadRequest(new { Status = false, Message = message });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
