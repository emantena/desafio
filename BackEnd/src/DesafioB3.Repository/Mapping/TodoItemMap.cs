using DesafioB3.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioB3.Repository.Mapping;

public class TodoItemMap : IEntityTypeConfiguration<TodoItem>
{
	public void Configure(EntityTypeBuilder<TodoItem> builder)
	{
		builder.ToTable("TodoItem");

		builder.HasKey(t => t.TodoItemId);

		builder.Property(t => t.TodoItemId);
		builder.Property(t => t.StatusId).IsRequired();

		builder.Property(t => t.Name)
			.IsRequired()
			.HasMaxLength(150);

		builder.Property(t => t.Description)
			.HasMaxLength(500);

		builder.Property(t => t.CreateAt)
			.IsRequired()
			.HasDefaultValueSql("GETDATE()");

		//builder.HasOne(t => t.Status)
		//	.WithMany()
		//	.HasForeignKey(t => t.StatusId);
	}
}