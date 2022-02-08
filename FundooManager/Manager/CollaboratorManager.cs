using FundooManager.Interface;
using FundooModels;
using FundooRepositry.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Manager
{
    public class CollaboratorManager : ICollaboratorManager
    {
        private readonly ICollaboratorRepositry repositry;
        public CollaboratorManager(ICollaboratorRepositry repositry)
        {
            this.repositry = repositry;
        }
        public string AddCollaborator(ColloboratorModel colloboratorModel)
        {
            try
            {
                return this.repositry.AddCollaborator(colloboratorModel);
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
                return this.repositry.DeleteCollaborator(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<CollaboratorEntity> GetCollaborators()
        {
            try
            {
                return this.repositry.GetCollaborators();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
