using FluentValidation.Results;

namespace NET_WebApp_Backend.Models
{
    public static class FluentValidationExtend
    {
        public static Dictionary<string, List<string>> GetErrors(List<ValidationFailure> errors)
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            foreach (ValidationFailure failure in errors)
            {
                List<string> errorsList = null;
                if (!result.TryGetValue(failure.PropertyName, out errorsList))
                {
                    errorsList = new List<string>();
                    result[failure.PropertyName] = errorsList;
                }

                errorsList.Add(failure.ErrorMessage);
            }
            return result;
        }
    }
}
