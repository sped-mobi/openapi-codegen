// -----------------------------------------------------------------------
// <copyright file="EntityGenerator.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Microsoft.OpenApi.CodeGeneration.Utilities;
using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.Entities
{
    public class EntityGenerator : AbstractGenerator, IEntityGenerator
    {
        public EntityGenerator(GeneratorDependencies dependencies) : base(dependencies)
        {
        }

        public string WriteCode(OpenApiSchema schema, string name, string @namespace)
        {
            Clear();
            GenerateFileHeader();
            WriteLine("using System;");
            WriteLine("using System.Collections.Generic;");
            WriteLine();
            WriteLine($"namespace {@namespace}");
            using (OpenBlock())
            {
                string className = Dependencies.Namer.Entity(name);
                WriteLine($"public partial class {className}");
                using (OpenBlock())
                {
                    WriteLine($"public {className}()");
                    var simpleProperties = schema.GetSimpleProperties();
                    var navigationProperties = schema.GetNavigationProperties();
                    var allproperties = schema.GetAllPropertiesRecursive();

                    using (OpenBlock())
                    {
                        foreach (var kvp in navigationProperties)
                        {
                            string n = StringUtilities.MakePascal(kvp.Key);
                            string t = Dependencies.Schema.ConvertToType(kvp.Value);
                            if (t.StartsWith("ICollection", StringComparison.CurrentCultureIgnoreCase))
                            {
                                t = t.Replace("ICollection", string.Empty);
                                t = t.TrimStart('<');
                                t = t.TrimEnd('>');
                                WriteLine($"{n} = new HashSet<{t}>();");
                            }
                        }
                    }

                    foreach (var kvp in allproperties)
                    {
                        string n = StringUtilities.MakePascal(kvp.Key);
                        string t = Dependencies.Schema.ConvertToType(kvp.Value);
                        WriteLine();
                        WriteLine($"public {t} {n} {{ get; set; }}");
                    }
                }
            }

            return GetText();
        }
    }
}
