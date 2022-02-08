using FundooManager.Interface;
using FundooModels;
using FundooRepositry.Interface;
using FundooRepositry.Repositry;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class LabelManager : ILabelManager
    {
        private readonly ILabelRepositry repositry;
        public LabelManager(ILabelRepositry repositry)
        {
            this.repositry = repositry;
        }
        public Task<LabelModel> CreateLabel(LabelModel labelModel, int noteId, int userId)
        {
            try
            {
                return this.repositry.CreateLabel(labelModel, noteId, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Task<LabelEntity> DeleteLabel(LabelEntity labelEntity, int id)
        {
            try
            {
                return this.repositry.DeleteLabel(labelEntity, id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
