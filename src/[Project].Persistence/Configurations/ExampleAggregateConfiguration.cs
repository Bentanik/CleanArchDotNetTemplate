namespace _Project_.Persistence.Configurations;

public class ExampleAggregateConfiguration : IEntityTypeConfiguration<ExampleAggregate>
{
    public void Configure(EntityTypeBuilder<ExampleAggregate> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ExampleText)
               .IsRequired()
               .HasMaxLength(256);

        builder.OwnsOne(e => e.ExampleValueObject, vo =>
        {
            vo.Property(v => v.ExampleValue)
                .IsRequired()
                .HasMaxLength(256);
        });

        builder.Property(e => e.ExampleStatus)
               .IsRequired();
    }
}