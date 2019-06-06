using System;
using Microsoft.OpenApi.CodeGeneration.Utilities;
using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.ViewModels
{
    public class ViewModelGenerator : AbstractGenerator, IViewModelGenerator
    {
        public ViewModelGenerator(GeneratorDependencies dependencies) : base(dependencies)
        {
        }
        public string WriteCode(OpenApiSchema schema, string name, string @namespace)
        {
            Clear();
            GenerateFileHeader();
            WriteLine();
            WriteLine($"namespace {@namespace}");
            using (OpenBlock())
            {
                string className = Dependencies.Namer.ViewModel(name);
                WriteLine($"public partial class {className}");
                using (OpenBlock())
                {
                    var properties = schema.GetAllPropertiesRecursive();

                    foreach (var kvp in properties)
                    {
                        string n = StringUtilities.MakePascal(kvp.Key);
                        string t = Dependencies.Schema.ConvertToType(kvp.Value);

                        if (t.StartsWith("ICollection<", StringComparison.CurrentCultureIgnoreCase))
                        {
                            string originalItemType = null;
                            string newItemType = null;
                            string temp = t;

                            originalItemType = temp.Substring(12).TrimEnd('>');

                            var keys = Dependencies.Document.Components.Schemas.Keys;

                            if (keys.Contains(originalItemType))
                            {
                                newItemType = Dependencies.Namer.ViewModel(originalItemType);
                                t = t.Replace(originalItemType, newItemType);
                            }
                        }

                        WriteLine();
                        WriteLine($"public {t} {n} {{ get; set; }}");
                    }
                }
            }
            return GetText();
        }
    }
}
