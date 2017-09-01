using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    /// A <see cref="ProblemDescriptor"/> for validation errors.
    /// </summary>
    public class ValidationProblemDescriptor : ProblemDescriptor
    {
        /// <summary>
        /// Intializes a new instance of <see cref="ValidationProblemDescriptor"/>.
        /// </summary>
        public ValidationProblemDescriptor()
        {
            Title = "One or more validation errors occured";
        }

        public ValidationProblemDescriptor(ModelStateDictionary modelState)
            : this()
        {
            if (modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }

            foreach (var keyModelStatePair in modelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    if (errors.Count == 1)
                    {
                        Errors.Add(key, GetErrorMessage(errors[0]));
                    }
                    else
                    {
                        var errorMessages = errors.Select(error =>
                        {
                            return GetErrorMessage(error);
                        }).ToArray();

                        Errors.Add(key, errorMessages);
                    }
                }
            }

            string GetErrorMessage(ModelError error)
            {
                return string.IsNullOrEmpty(error.ErrorMessage) ?
                    Resources.SerializableError_DefaultError :
                    error.ErrorMessage;
            }
        }


        /// <summary>
        /// Gets or sets the validation errors associated with this instance of <see cref="ValidationProblemDescriptor"/>.
        /// </summary>
        public IDictionary<string, object> Errors { get; set; } = new Dictionary<string, object>(StringComparer.Ordinal);
    }
}
