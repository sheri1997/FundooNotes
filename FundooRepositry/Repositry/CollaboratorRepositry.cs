using FundooModels;
using FundooRepositry.Context;
using FundooRepositry.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositry.Repositry
{
    public class CollaboratorRepositry : ICollaboratorRepositry
    {
        private readonly UserContext context;
        private readonly IConfiguration configuration;

        public CollaboratorRepositry(UserContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        public string AddCollaborator(ColloboratorModel colloboratorModel)
        {
            try
            {
                if (colloboratorModel != null)
                {
                    CollaboratorEntity collaboratorEntity = new CollaboratorEntity();
                    collaboratorEntity.SenderEmail = colloboratorModel.SenderEmail;
                    collaboratorEntity.RecieverEmail = colloboratorModel.RecieverEmail;
                    this.context.Collaborator.Add(collaboratorEntity);
                    this.context.SaveChanges();
                    return collaboratorEntity.SenderEmail;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string DeleteCollaborator(int id)
        {
            try
            {
                var collaborator = this.context.Collaborator.Find(id);
                if (collaborator != null)
                {
                    this.context.Collaborator.Remove(collaborator);
                    this.context.SaveChangesAsync();
                    return "Collaborator Deleted Successfully !";
                }

                return "Unable to delete this Collaborator.";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<CollaboratorEntity> GetCollaborators()
        {
            return this.context.Collaborator.ToList();
        }
    }
}
