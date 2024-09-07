using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ShiftLogger.Helper
{
    public class ModelStateErrorMessageGenerator
    {

        public static string modelStateErrorMessage(ModelStateDictionary modelStateDictionary)
        {
            return string.
                Join("\n", modelStateDictionary
                .Values
                .SelectMany(model => model.Errors)
                .Select(e => e.ErrorMessage));
        }
    }
}
