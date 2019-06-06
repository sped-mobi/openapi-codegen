// -----------------------------------------------------------------------
// <copyright file="ISchemaConverter.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.Utilities
{
    public interface ISchemaConverter
    {
        string ConvertToType(OpenApiSchema schema);
    }

    public class SchemaConverter : ISchemaConverter
    {
        public string ConvertToType(OpenApiSchema schema)
        {

            switch (schema.Type)
            {
                case "array":
                    {
                        if (!string.IsNullOrEmpty(schema.Items.Reference.Id))
                        {
                            return $"ICollection<{schema.Items.Reference.Id}>";
                        }
                        else
                        {
                            string type = ConvertToType(schema.Items);
                            return $"ICollection<{type}>";
                        }
                    }

                case "integer":
                case "number": return "int";
                case "boolean": return "bool";
                case "string":
                    {
                        if (!string.IsNullOrEmpty(schema.Format))
                        {
                            switch (schema.Format)
                            {
                                case "uri": return "Uri";
                                case "uuid": return "Guid";
                                case "datetime":
                                case "date": return "DateTime";
                                case "time": return "TimeSpan";
                                default: return "string";
                            }
                        }
                        return "string";
                    }
                default: return schema.Reference.Id;
            }
        }
    }
}
