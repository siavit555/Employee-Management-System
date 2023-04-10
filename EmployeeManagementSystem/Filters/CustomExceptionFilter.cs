using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace EmployeeManagementSystem.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly IModelMetadataProvider _modelMetadataProvider;
        public CustomExceptionFilter(IModelMetadataProvider modelMetadataProvider) 
        {
            _modelMetadataProvider = modelMetadataProvider;
        }

        public void OnException(ExceptionContext context) 
        {
            var result = new ViewResult {ViewName="ErrorPage" };
            result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState)
            {
                { "Exception", context.Exception }
            };

            context.ExceptionHandled = true; 
            context.Result = result;
        }
    }
}
