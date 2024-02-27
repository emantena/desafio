using DesafioB3.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioB3.Repository.Mapping;
public class StatusMap : IEntityTypeConfiguration<Status>
{
	public void Configure(EntityTypeBuilder<Status> builder)
	{
		builder.ToTable("Status");

		builder.HasKey(s => s.StatusId);

		builder.Property(s => s.StatusId)
			.IsRequired();

		builder.Property(s => s.Name)
			.IsRequired()
			.HasMaxLength(15);

		builder.Property(s => s.OrderExibition)
			.IsRequired();

		builder.Property(s => s.Active)
			.IsRequired();
	}
}