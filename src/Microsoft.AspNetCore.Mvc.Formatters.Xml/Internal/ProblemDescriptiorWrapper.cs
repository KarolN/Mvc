// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Extensions.Internal;

namespace Microsoft.AspNetCore.Mvc.Formatters.Xml.Internal
{
    /// <summary>
    /// Wrapper class for <see cref="Mvc.ProblemDescriptor"/> to enable it to be serialized by the xml formatters.
    /// </summary>
    [XmlRoot("Error")]
    public sealed class ProblemDescriptorWrapper : IXmlSerializable, IUnwrappable
    {
        // Note: XmlSerializer requires to have default constructor
        public ProblemDescriptorWrapper()
        {
            ProblemDescriptor = new ProblemDescriptor();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemDescriptorWrapper"/> class.
        /// </summary>
        /// <param name="problem">The <see cref="Mvc.ProblemDescriptor"/> object that needs to be wrapped.</param>
        public ProblemDescriptorWrapper(ProblemDescriptor problem)
        {
            ProblemDescriptor = problem ?? throw new ArgumentNullException(nameof(ProblemDescriptor));
        }

        /// <summary>
        /// Gets the wrapped object which is serialized/deserialized into XML
        /// representation.
        /// </summary>
        public ProblemDescriptor ProblemDescriptor { get; }

        /// <inheritdoc />
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates a <see cref="ProblemDescriptor"/> object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="XmlReader"/> stream from which the object is deserialized.</param>
        public void ReadXml(XmlReader reader) => throw new NotSupportedException();

        /// <summary>
        /// Converts the wrapped <see cref="ProblemDescriptor"/> object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="XmlWriter"/> stream to which the object is serialized.</param>
        public void WriteXml(XmlWriter writer)
        {
            WriteProperty(nameof(ProblemDescriptor.Title), ProblemDescriptor.Title);

            if (ProblemDescriptor.Status != null)
            {
                WriteProperty(nameof(ProblemDescriptor.Status), ProblemDescriptor.Status);
            }

            if (!string.IsNullOrEmpty(ProblemDescriptor.Instance))
            {
                WriteProperty(nameof(ProblemDescriptor.Instance), ProblemDescriptor.Instance);
            }

            if (!string.IsNullOrEmpty(ProblemDescriptor.Type))
            {
                WriteProperty(nameof(ProblemDescriptor.Type), ProblemDescriptor.Type);
            }

            if (!string.IsNullOrEmpty(ProblemDescriptor.Detail))
            {
                WriteProperty(nameof(ProblemDescriptor.Detail), ProblemDescriptor.Detail);

            }

            if (ProblemDescriptor.GetType() != typeof(ProblemDescriptor))
            {
                // Derived type
                var properties = PropertyHelper.GetVisibleProperties(ProblemDescriptor.GetType());
                for (var i = 0; i < properties.Length; i++)
                {
                    var property = properties[i];
                    if (property.Property.DeclaringType != typeof(ProblemDescriptor))
                    {
                        WriteProperty(property.Name, property.GetValue(ProblemDescriptor));
                    }
                }
            }

            foreach (var extendedProperty in ProblemDescriptor.AdditionalProperties)
            {
                WriteProperty(extendedProperty.Key, extendedProperty.Value);
            }

            void WriteProperty<TValue>(string name, TValue value)
            {
                writer.WriteStartElement(XmlConvert.EncodeLocalName(name));
                writer.WriteValue(value);
                writer.WriteEndElement();
            }
        }

        /// <inheritdoc />
        public object Unwrap(Type declaredType)
        {
            if (declaredType == null)
            {
                throw new ArgumentNullException(nameof(declaredType));
            }

            return ProblemDescriptor;
        }
    }
}