using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class AuthConfiguration : IEntityTypeConfiguration<Auth>
{
    public void Configure(EntityTypeBuilder<Auth> builder)
    {
        builder.ToTable(nameof(Auth));

        builder.Property(user => user.Email).HasMaxLength(320);
        builder.Property(user => user.Email).IsRequired();
        
        builder.Property(auth => auth.PassHash).IsRequired();
        
        builder.Property(auth => auth.PassSalt).IsRequired();
    }
}