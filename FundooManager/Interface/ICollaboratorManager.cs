using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Interface
{
    public interface ICollaboratorManager
    {
        public string AddCollaborator(ColloboratorModel colloboratorModel);
        public string DeleteCollaborator(int id);
        public IEnumerable<CollaboratorEntity> GetCollaborators();

    }
}
