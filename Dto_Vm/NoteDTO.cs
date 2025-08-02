using ZAMETKI_FINAL.Model;

namespace ZAMETKI_FINAL.Dto_Vm
{
    public record NoteDto(string title, string description, Priority NotePriority);

    public record NoteUpdateDto(string title, string description, Priority NotePriority, bool isCompleted);
}
