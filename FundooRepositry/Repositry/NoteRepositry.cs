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
    public class NoteRepositry : INoteRepositry
    {
        private readonly UserContext context;
        private readonly IConfiguration configuration;

        public NoteRepositry(UserContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        public bool AddNotes(UpdateModel updateModel)
        {
            try
            {
                NotesEntity notesEntity = new NotesEntity();
                notesEntity.Title = updateModel.Title;
                notesEntity.Note = updateModel.Note;
                notesEntity.Colour = updateModel.Colour;
                notesEntity.Trash = updateModel.Trash;
                notesEntity.Image = updateModel.Image;
                notesEntity.Archive = updateModel.Archive;
                notesEntity.Delete = updateModel.Delete;
                notesEntity.Pin = updateModel.Pin;
                notesEntity.Remainder = updateModel.Remainder;
                this.context.Add(notesEntity);
                var result = this.context.SaveChanges();
                return true;              
            }
            catch(ArgumentNullException ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }
        public int DeleteNode(int noteId)
        {
            try
            {
                var checkIserID = this.context.Note.FirstOrDefault(x => x.Id == noteId);
                if (checkIserID != null)
                {
                    this.context.Remove(checkIserID);
                    this.context.SaveChanges();
                    return 1;
                }
                return 0;
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
                var checkUserID = this.context.Note.Where(x => x.Id == Id).FirstOrDefault();
                if (checkUserID != null)
                {
                    checkUserID.Title = updateModel.Title;
                    checkUserID.Note = updateModel.Note;
                    checkUserID.Colour = updateModel.Colour;
                    checkUserID.Trash = updateModel.Trash;
                    checkUserID.Image = updateModel.Image;
                    checkUserID.Archive = updateModel.Archive;
                    checkUserID.Delete = updateModel.Delete;
                    checkUserID.Pin = updateModel.Pin;
                    checkUserID.Remainder = updateModel.Remainder;
                    this.context.Update(checkUserID);
                    var result = this.context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (ArgumentNullException ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<NotesEntity> RetriveNotes()
        {
            return this.context.Note.ToList();
        }
        public bool IsTrash(int NoteId)
        {
            try
            {
                var checkUserID = this.context.Note.Where(x => x.Id == NoteId).FirstOrDefault();
                if(checkUserID != null)
                {
                    if (checkUserID.Trash == true)
                    {
                        return true;
                    }
                    else return false;
                }
                return false;
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
                var checkUserID = this.context.Note.Where(x => x.Id == NoteId).FirstOrDefault();
                if (checkUserID != null)
                {
                    if (checkUserID.Pin == true)
                    {
                        return true;
                    }
                    else return false;
                }
                return false;
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
                var checkUserID = this.context.Note.Where(x => x.Id == NoteId).FirstOrDefault();
                if (checkUserID != null)
                {
                    if (checkUserID.Archive == true)
                    {
                        return true;
                    }
                    else return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public NotesEntity GetById(int UserId)
        {
            return this.context.Note.FirstOrDefault(x => x.Id == UserId);
        }
    }
}
