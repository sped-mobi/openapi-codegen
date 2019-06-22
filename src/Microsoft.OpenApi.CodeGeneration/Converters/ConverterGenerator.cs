// -----------------------------------------------------------------------
// <copyright file="ConverterGenerator.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.Converters
{
    public class ConverterGenerator : AbstractGenerator, IConverterGenerator
    {
        public ConverterGenerator(GeneratorDependencies dependencies) : base(dependencies)
        {
        }

        public string WriteCode(OpenApiSchema schema, string name, string @namespace)
        {
            string rootNamespace = Dependencies.Document.Options.RootNamespace;
            string entityNamespace = Dependencies.Namespace.Entity(rootNamespace);
            string viewModelNamespace = Dependencies.Namespace.ViewModel(rootNamespace);
            Clear();
            GenerateFileHeader();
            WriteLine("using System;");
            WriteLine("using System.Linq;");
            WriteLine("using System.Threading;");
            WriteLine("using System.Diagnostics;");
            WriteLine("using System.Threading.Tasks;");
            WriteLine("using System.Collections.Generic;");
            WriteLine($"using {entityNamespace};");
            WriteLine($"using {viewModelNamespace};");
            WriteLine();
            WriteLine($"namespace {@namespace}");
            using (OpenBlock())
            {
                string converterName = Dependencies.Namer.Converter(name);
                string entityName = Dependencies.Namer.Entity(name);
                string viewModelName = Dependencies.Namer.ViewModel(name);
                WriteLine($"public static class {converterName}");
                using (OpenBlock())
                {
                    WriteLine($"public static {viewModelName} Convert({entityName} entity)");
                    using (OpenBlock())
                    {
                        WriteLine($"return new {viewModelName}");
                        using (OpenBlockSemicolon())
                        {
                            foreach ((string key, OpenApiSchema value) in schema.GetSimpleProperties())
                            {
                                WriteLine($"{key} = entity.{key},");
                            }
                        }
                    }

                    WriteLine();
                    WriteLine($"public static List<{viewModelName}> ConvertList(IEnumerable<{entityName}> entities)");
                    using (OpenBlock())
                    {
                        WriteLine("return entities.Select(e =>");
                        using (OpenBlockString(").ToList();"))
                        {
                            WriteLine($"var model = new {viewModelName}();");

                            foreach ((string key, OpenApiSchema value) in schema.GetSimpleProperties())
                            {
                                WriteLine($"model.{key} = e.{key};");
                            }
                            WriteLine("return model;");
                        }
                    }
                }
            }

            return GetText();
        }
    }
}
