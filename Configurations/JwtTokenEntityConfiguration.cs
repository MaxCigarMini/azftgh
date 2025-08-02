﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ZAMETKI_FINAL.Configurations
{
    public sealed class JwtTokenEntityConfiguration : IEntityTypeConfiguration<JwtToken>
    {
        public void Configure(EntityTypeBuilder<JwtToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId)
            .ValueGeneratedNever()
            .IsRequired();
            builder.HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<JwtToken>()
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
