namespace CleanAndBat.Dto;

public static class DtoExtensions
{
	public static string GetValidationErrors(this ValidationResult validationResult, string errorSeperator = " | ")
		=> string.Join(errorSeperator, validationResult.Errors.Select(x => x.ErrorMessage).ToList());
}