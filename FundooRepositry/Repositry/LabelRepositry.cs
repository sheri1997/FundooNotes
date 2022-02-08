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
    public class LabelRepositry: ILabelRepositry
    {
        private readonly UserContext context;
        private readonly IConfiguration configuration;

        public LabelRepositry(UserContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<LabelModel> CreateLabel(LabelModel labelModel, int noteId, int userId)
        {
            try
            {
                var checkuser = this.context.User.Where(x => x.UserId == userId);
                if (checkuser != null)
                {
                    LabelEntity labelEntity = new LabelEntity();
                    labelEntity.LabelName = labelModel.LabelName;
                    this.context.Add(labelEntity);
                    await this.context.SaveChangesAsync();
                    return labelModel;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<LabelEntity> DeleteLabel(LabelEntity labelEntity, int id)
        {
            try
            {
                var checkUser = this.context.User.Where(x => x.UserId == id);
                if(checkUser!= null)
                {
                    this.context.Label.Remove(labelEntity);
                    await this.context.SaveChangesAsync();
                    return labelEntity;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //public LabelModel GetModel(int labelid, int id)
        //{
        //    var checkuser = this.context.User.Where(x => x.UserId == id);
        //    if(checkuser!= null)
        //    {
        //        return this.context.Label.FirstOrDefault(y => y.LabelId == labelid);

        //    }
        //}
    }
}
