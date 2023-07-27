namespace CleanAndBat.Persistence.EntityConfigurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable(nameof(User), "Base");

		builder.Property(x => x.UserId)
			.UseIdentityColumn();

		builder.Property(x => x.MobileNumber)
			.IsRequired();

		builder.Property(x => x.IsActive)
			.HasDefaultValue(true);

		builder.Property(x => x.IsDeleted)
			.HasDefaultValue(false);

		builder.Property(x => x.PasswordSalt)
			.IsRequired()
			.HasColumnType("char")
			.HasMaxLength(8);

		builder.Property(x => x.FirstName)
			.IsRequired()
			.HasMaxLength(30);

		builder.Property(x => x.LastName)
			.IsRequired()
			.HasMaxLength(30);

		builder.Property(x => x.Password)
			.IsRequired()
			.HasColumnType("varchar")
			.HasMaxLength(50);

		builder.Property(x => x.Email)
			.HasColumnType("varchar")
			.HasMaxLength(55);


		builder.HasIndex(x => x.MobileNumber)
			.IsUnique();

		builder
			.HasMany(x => x.Otps)
			.WithOne(x => x.User)
			.HasForeignKey(x => x.UserId)
			.IsRequired();
	}
}