using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositry.Interface
{
    public interface ILabelRepositry
    {
        public Task<LabelModel> CreateLabel(LabelModel labelModel, int noteId, int userId);
        public Task<LabelEntity> DeleteLabel(LabelEntity labelEntity, int id);

    }
}
