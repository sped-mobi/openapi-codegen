// -----------------------------------------------------------------------
// <copyright file="ViewModelGenerator.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

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
            var nameList = Dependencies.Document.GetSchemaKeys();

            string entityNamespace = Dependencies.Namespace.Entity(Dependencies.Document.Options.RootNamespace);

            Clear();
            GenerateFileHeader();
            WriteLine("using System;");
            WriteLine("using System.Threading;");
            WriteLine("using System.Threading.Tasks;");
            WriteLine("using System.Collections.Generic;");
            WriteLine($"using {entityNamespace};");
            WriteLine();
            WriteLine($"namespace {@namespace}");
            using (OpenBlock())
            {
                string className = Dependencies.Namer.ViewModel(name);
                string entityName = Dependencies.Namer.Entity(name);
                WriteLine($"public partial class {className}");
                using (OpenBlock())
                {
                    var primaryKeyTypeName = Dependencies.Document.Options.PrimaryKeyTypeName;
                    var properties = schema.GetAllPropertiesRecursive();

                    WriteLine($"public {primaryKeyTypeName} {entityName}ID {{ get; set; }}");

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
                            var keys = Dependencies.Document.GetSchemaKeys();
                            if (keys.Contains(originalItemType))
                            {
                                newItemType = Dependencies.Namer.ViewModel(originalItemType);
                                t = t.Replace(originalItemType, newItemType);
                            }
                        }

                        if (nameList.Contains(t))
                        {
                            t = Dependencies.Namer.ViewModel(t);
                        }

                        if (n == className)
                        {
                            n = char.ToLower(n[0]) + n.Substring(1);
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
