using FundooManager.Interface;
using FundooModels;
using FundooRepositry.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class NoteManager : INoteManager
    {
        private readonly INoteRepositry repositry;
        public NoteManager(INoteRepositry repositry)
        {
            this.repositry = repositry;
        }
        public bool AddNotes(UpdateModel updateModel)
        {
            try
            {
                return this.repositry.AddNotes(updateModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int DeleteNode(int noteId)
        {
            try
            {
                return this.repositry.DeleteNode(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateNotes(UpdateModel updateModel, int Id)
        {
            try
            {
                return this.repositry.UpdateNotes(updateModel, Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<NotesEntity> RetriveNotes()
        {
            try
            {
                return this.repositry.RetriveNotes();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsTrash(int NoteId)
        {
            try
            {
                return this.repositry.IsTrash(NoteId);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsPin(int NoteId)
        {
            try
            {
                return this.repositry.IsPin(NoteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsArchive(int NoteId)
        {
            try
            {
                return this.repositry.IsArchive(NoteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public NotesEntity GetById(int UserId)
        {
            try
            {
                return this.GetById(UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
