using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Infrastructure.EF.Config
{
    internal sealed class PublicDocumentConfiguration : IEntityTypeConfiguration<PublicDocument>
    {
        public void Configure(EntityTypeBuilder<PublicDocument> builder)
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

            builder.OwnsMany(e => e._attachmentFilePaths, a =>
            {
                a.Property(e => e.Value)
                    .HasColumnName("FilePath")
                    .HasConversion(
                     p => p,
                     p => new DocumentFilePath(p));
            });

            // since for document access premission reasons we need only userId
            // we don't create relationship with ApplicationUser entity since name, surname etc..
            // wouldn't be usefull in this scenario
            builder.OwnsMany(e => e._eligibleForRead, a =>
            {
                a.ToTable("PublicDocumentEligibleForRead");

                a.Property(e => e.Value)
                    .HasColumnName("UserId")
                    .HasConversion(
                        p => p,
                        p => new ApplicationUserId(p));
            });

            builder.OwnsMany(e => e._eligibleForWrite, a =>
            {
                a.ToTable("PublicDocumentEligibleForWrite");

                a.Property(e => e.Value)
                .HasColumnName("UserId")
                .HasConversion(
                    p => p,
                    p => new ApplicationUserId(p));
            });

            builder.OwnsOne(e => e._creationDetails, a =>
            {
                a.ToTable("DocumentCreationDetails");

                a.Property(e => e.DocumentCreationDate)
                    .HasColumnName("CreationDate")
                    .HasConversion(
                    p => p.Value,
                    p => new DocumentCreationDate(p));

                a.Property(e => e.UserId)
                    .HasColumnName("CreatedByUserId")
                    .HasConversion(
                    p => p.Value,
                    p => new ApplicationUserId(p));
            });
        }
    }
}