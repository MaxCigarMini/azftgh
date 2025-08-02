using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAMETKI_FINAL.Model;

namespace ZAMETKI_FINAL.Database
{
    public class NoteEntityConfiguretion
    {
        /*public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(note => note.NoteId);
            builder.Property(note => note.Title)
                .IsRequired()
                .HasMaxLength(300);
            builder.Property(note => note.Description)
                .IsRequired()
                .HasColumnType("text");
            builder.HasOne(note => note.Owner)
                .WithMany(user => user.Notes)
                .HasForeignKey(note => note.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }*/
    }
}
