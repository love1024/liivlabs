using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Text;

namespace liivlabs_core.Helper
{
    public class Helper
    {
        public static object FormatErrorResponse(ModelStateDictionary modelState) {
            IList<string> errors = new List<string>();
            foreach(var state in modelState) {
                foreach (var error in state.Value.Errors) {
                    errors.Add(error.ErrorMessage);
                }
            }
            var response =  new { message = errors };
            return response;
        }
    }
}
