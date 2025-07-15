using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Data.Entities;

namespace University.Data.Context.ClassMappings
{
    internal class CourseMapping : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable(nameof(Course));

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("CourseId");
        }
    }
}