using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface ILabelManager
    {
        public Task<LabelModel> CreateLabel(LabelModel labelModel, int noteId, int userId);
        public Task<LabelEntity> DeleteLabel(LabelEntity labelEntity, int id);

    }
}
