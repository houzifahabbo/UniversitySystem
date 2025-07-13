using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Data.Entities;

namespace University.Data.Context.Mapping
{
    public class StudentMapping : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");

            builder.HasKey(s=>s.Id);
            builder.Property(s => s.Id).HasColumnName("StudentId");

            builder.Property(s => s.Name).HasMaxLength(256);

            builder.HasIndex(s => s.Email).IsUnique();

        }
    }
}