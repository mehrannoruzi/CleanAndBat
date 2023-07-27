namespace CleanAndBat.Persistence.EntityConfigurations;

public class OtpConfig : IEntityTypeConfiguration<Otp>
{
	public void Configure(EntityTypeBuilder<Otp> builder)
	{
		builder.ToTable(nameof(Otp), "Auth");

		builder.Property(x => x.OtpId)
			.UseIdentityColumn();

		builder.Property(x => x.IsUsed)
			.HasDefaultValue(false);

		builder.Property(x => x.PinCode)
			.IsRequired()
			.HasColumnType("varchar")
			.HasMaxLength(8);
	}
}