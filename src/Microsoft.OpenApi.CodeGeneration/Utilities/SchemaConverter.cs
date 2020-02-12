using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.Utilities
{
    public class SchemaConverter : ISchemaConverter
    {
        public string ConvertToType(OpenApiSchema schema)
        {
            if (schema == null)
                return string.Empty;

            switch (schema.Type)
            {
                case "array":
                    {
                        var reference = schema.Items.Reference;
                        if (reference != null)
                        {
                            if (!string.IsNullOrEmpty(reference.Id))
                            {
                                return $"ICollection<{Base(reference.Id)}>";
                            }
                        }

                        string type = ConvertToType(schema.Items);
                        return $"ICollection<{type}>";
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
                                case "uuid": return "Guid";
                                case "datetime":
                                case "date": return "DateTime";
                                case "time": return "TimeSpan";
                                default: return "string";
                            }
                        }

                        return "string";
                    }

                default: return Base(schema.Reference.Id);
            }
        }

        private static string Base(string name)
        {
            if (name.Contains("_"))
            {
                name = StringUtilities.MakePascal(name.Split("_")[1]);
            }

            return StringUtilities.MakePascal(name);
        }
    }
}
