using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Infrastructure.EF.Config
{
    internal sealed class PrivateDocumentConfiguration : IEntityTypeConfiguration<PrivateDocument>
    {
        public void Configure(EntityTypeBuilder<PrivateDocument> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasConversion(
                p => p.Value,
                p => new DocumentId(p));

            builder.Property(e => e._title).HasConversion(
                p => p.Value,
                p => new DocumentTitle(p));

            builder.Property(e => e._content).HasConversion(
                p => p.Value,
                p => new DocumentContent(p));

            builder.Property(e => e._creationDate).HasConversion(
                p => p.Value,
                p => new DocumentCreationDate(p));

            builder.OwnsMany(e => e._attachmentFilePaths, a =>
            {
                a.Property(fp => fp.Value)
                    .HasColumnName("FilePath")
                    .HasConversion(
                     p => p,
                     p => new DocumentFilePath(p));
            });
        }
    }
}