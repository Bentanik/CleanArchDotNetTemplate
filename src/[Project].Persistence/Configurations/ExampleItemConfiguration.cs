namespace _Project_.Persistence.Configurations;

public class ExampleItemConfiguration : IEntityTypeConfiguration<ExampleItemEntity>
{
       public void Configure(EntityTypeBuilder<ExampleItemEntity> builder)
       {
              builder.HasKey(e => e.Id);

              builder.Property(e => e.ExampleText)
                     .IsRequired()
                     .HasMaxLength(256);
       }
}