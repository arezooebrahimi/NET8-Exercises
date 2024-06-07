using System.ComponentModel.DataAnnotations;

namespace MinimalApi.Validators
{
    public static class Validators
    {
        public static(bool isValid, string?[] errors) Validate(Object input)
        {
            var context = new ValidationContext(input);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(input, context, results, true)){
                return(true,Array.Empty<string>());
            }
            else
            {
                return(false,results.Select(t=>t.ErrorMessage).ToArray());
            }
        }
    }
}
