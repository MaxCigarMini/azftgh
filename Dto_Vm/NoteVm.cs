using ZAMETKI_FINAL.Model;

namespace ZAMETKI_FINAL.Dto_Vm
{
   public record NoteVm(int NoteId, string Title, string Description,
       DateTime DateTimeCreate, Priority NotePriority, bool IsCompleted);
}
