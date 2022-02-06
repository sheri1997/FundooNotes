using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositry.Interface
{
    public interface INoteRepositry
    {
        public bool AddNotes(UpdateModel updateModel);
        public int DeleteNode(int noteId);
        public bool UpdateNotes(UpdateModel updateModel, int Id);
        public IEnumerable<NotesEntity> RetriveNotes();
        public bool IsTrash(int NoteId);
        public bool IsPin(int NoteId);
        public bool IsArchive(int NoteId);
        public NotesEntity GetById(int UserId);

    }
}
