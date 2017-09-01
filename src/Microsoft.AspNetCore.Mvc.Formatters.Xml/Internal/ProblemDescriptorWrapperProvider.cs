// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.AspNetCore.Mvc.Formatters.Xml.Internal
{
    /// <summary>
    /// Wraps the object of type <see cref="ProblemDescriptor"/>.
    /// </summary>
    public class ProblemDescriptorWrapperProvider : IWrapperProvider
    {
        /// <inheritdoc />
        public Type WrappingType => typeof(ProblemDescriptorWrapper);

        /// <inheritdoc />
        public object Wrap(object original)
        {
            if (original == null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            if (original is ProblemDescriptor problem)
            {
                return new ProblemDescriptorWrapper(problem);
            }

            throw new ArgumentException(
                Resources.FormatWrapperProvider_MismatchType(
                    typeof(ProblemDescriptorWrapper).Name,
                    original.GetType().Name),
                nameof(original));
        }
    }
}