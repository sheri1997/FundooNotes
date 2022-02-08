using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepositry.Interface
{
    public interface ICollaboratorRepositry
    {
        public string AddCollaborator(ColloboratorModel colloboratorModel);
        public string DeleteCollaborator(int id);
        public IEnumerable<CollaboratorEntity> GetCollaborators();

    }
}
