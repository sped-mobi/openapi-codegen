using System;

namespace Microsoft.OpenApi.CodeGeneration.Internal
{
    public static class CodeGenerator
    {
        private static readonly CodeWriter _writer;

        static CodeGenerator()
        {
            _writer = new CodeWriter();
        }

        public static string Generate(CodeGenerationOptions options, FileType type, Kind kind = Kind.None)
        {
            switch (type)
            {
                case FileType.SupervisorModelClass:
                    return GenerateSupervisorModelClass(options);
                case FileType.ContextModelClass:
                    return GenerateContextModelClass(options);
                case FileType.ContextInterface:
                    return GenerateContextInterface(options);
                case FileType.ContextClass:
                    return GenerateContextClass(options);
                case FileType.SupervisorInterface:
                    return GenerateSupervisorInterface(options);
                case FileType.SupervisorClass:
                    return GenerateSupervisorClass(options);
                case FileType.ScaffoldedModelClass:
                    return GenerateScaffoldedModelClass(options);
                case FileType.ServicesClass:
                    return GenerateServicesClass(options);

                case FileType.ModelClass:
                    return GenerateModelClass(options, kind);
                case FileType.GeneratorClass:
                    return GenerateGeneratorClass(options, kind);
                case FileType.ScaffolderClass:
                    return GenerateScaffolderClass(options, kind);
                case FileType.GeneratorInterface:
                    return GenerateGeneratorInterface(options, kind);
                case FileType.ScaffolderInterface:
                    return GenerateScaffolderInterface(options, kind);
                default:
                    return string.Empty;
            }
        }

        private static string GenerateServicesClass(CodeGenerationOptions options)
        {
            Clear();
            string ns = options.RootNamespace;
            WriteUsings(options, Kind.None);
            WriteLine($"namespace {ns}");
            using (OpenBlock())
            {
                WriteLine("public static class OpenApiServices");
                using (OpenBlock())
                {
                    WriteLine("private static IServiceProvider _serviceProvider;");

                    WriteLine();
                    WriteLine("public static TService GetService<TService>()");
                    using (OpenBlock())
                    {
                        WriteLine("return ServiceProvider.GetService<TService>();");
                    }

                    WriteLine();
                    WriteLine("private static IServiceProvider ServiceProvider");
                    using (OpenBlock())
                    {
                        WriteLine("get");
                        using (OpenBlock())
                        {
                            WriteLine("if (_serviceProvider == null)");
                            using (OpenBlock())
                            {
                                WriteLine("_serviceProvider = new ServiceCollection()");
                                PushIndent();
                                WriteLine($".AddSingleton<GeneratorDependencies>()");
                                WriteLine($".AddSingleton<ScaffolderDependencies>()");
                                WriteLine($".AddSingleton<IFileWriter, FileWriter>()");
                                WriteLine($".AddSingleton<IPathHelper, PathHelper>()");
                                WriteLine($".AddSingleton<ITextProvider, TextProvider>()");
                                WriteLine($".AddSingleton<INamespaceHelper, NamespaceHelper>()");
                                WriteLine($".AddSingleton<IScaffoldingManager, ScaffoldingManager>()");
                                foreach (var kind in options.Kinds)
                                {
                                    string generatorInterface = NameHelper.MakeGeneratorInterfaceName(kind);
                                    string generatorClass = NameHelper.MakeGeneratorClassName(kind);
                                    WriteLine($".AddSingleton<{generatorInterface}, {generatorClass}>()");
                                }
                                foreach (var kind in options.Kinds)
                                {
                                    string scaffolderInterface = NameHelper.MakeScaffolderInterfaceName(kind);
                                    string scaffolderClass = NameHelper.MakeScaffolderClassName(kind);
                                    WriteLine($".AddSingleton<{scaffolderInterface}, {scaffolderClass}>()");
                                }
                                WriteLine(".BuildServiceProvider();");
                                PopIndent();
                            }
                            WriteLine("return _serviceProvider;");
                        }
                    }
                }
            }
            return GetText();
        }

        private static string GenerateScaffolderInterface(CodeGenerationOptions options, Kind kind)
        {
            Clear();
            string ns = NamespaceHelper.MakeNamespace(options.RootNamespace, kind);
            WriteUsings(options, kind);
            WriteLine($"namespace {ns}");
            using (OpenBlock())
            {
                string modelName = NameHelper.MakeModelClassName(kind);
                string typeName = NameHelper.MakeScaffolderInterfaceName(kind);

                WriteLine($"public interface {typeName}");
                using (OpenBlock())
                {
                    WriteLine($"void Save({modelName} model);");
                    WriteLine();
                    WriteLine($"{modelName} ScaffoldModel(OpenApiOptions options);");
                }
            }
            return GetText();
        }

        private static string GenerateScaffolderClass(CodeGenerationOptions options, Kind kind)
        {
            Clear();
            string ns = NamespaceHelper.MakeNamespace(options.RootNamespace, kind);
            WriteUsings(options, kind);
            WriteLine($"namespace {ns}");
            using (OpenBlock())
            {
                string modelName = NameHelper.MakeModelClassName(kind);
                string typeName = NameHelper.MakeScaffolderClassName(kind);
                string baseTypeName = NameHelper.MakeScaffolderInterfaceName(kind);
                string generatorInterfaceName = NameHelper.MakeGeneratorInterfaceName(kind);

                WriteLine($"public class {typeName} : AbstractScaffolder, {baseTypeName}");
                using (OpenBlock())
                {
                    WriteLine($"public {typeName}(ScaffolderDependencies dependencies, {generatorInterfaceName} generator) : base(dependencies)");
                    using (OpenBlock())
                    {
                        WriteLine("Generator = generator;");
                    }

                    WriteLine();
                    WriteLine($"protected {generatorInterfaceName} Generator {{ get; }} ");

                    WriteLine();
                    WriteLine($"public void Save({modelName} model)");
                    using (OpenBlock())
                    {
                        if (kind == Kind.Context || kind == Kind.Supervisor)
                        {
                            WriteLine($"Dependencies.FileWriter.WriteFile(model.Class);");
                            WriteLine($"Dependencies.FileWriter.WriteFile(model.Interface);");
                        }
                        else
                        {
                            WriteLine($"Dependencies.FileWriter.WriteFiles(model.Files);");
                        }
                    }
                    WriteLine();
                    WriteLine($"public {modelName} ScaffoldModel(OpenApiOptions options)");
                    using (OpenBlock())
                    {
                        WriteLine($"var model = new {modelName}();");

                        if (kind == Kind.Context)
                        {
                            WriteLine("var classFile = new ScaffoldedFile();");
                            WriteLine("var interfaceFile = new ScaffoldedFile();");
                            WriteLine("classFile.Code = Generator.WriteClassCode(options.Document, options.RootNamespace);");
                            WriteLine($"classFile.Path = Dependencies.PathHelper.Context(options.OutputDir, options.ContextClassName);");
                            WriteLine("interfaceFile.Code = Generator.WriteInterfaceCode(options.Document, options.RootNamespace);");
                            WriteLine($"interfaceFile.Path = Dependencies.PathHelper.Context(options.OutputDir, options.ContextInterfaceName);");
                            WriteLine("model.Class = classFile;");
                            WriteLine("model.Interface = interfaceFile;");
                        }
                        else if (kind == Kind.Supervisor)
                        {
                            WriteLine("var classFile = new ScaffoldedFile();");
                            WriteLine("var interfaceFile = new ScaffoldedFile();");
                            WriteLine("classFile.Code = Generator.WriteClassCode(options.Document, options.RootNamespace);");
                            WriteLine($"classFile.Path = Dependencies.PathHelper.Supervisor(options.OutputDir, options.SupervisorClassName);");
                            WriteLine("interfaceFile.Code = Generator.WriteInterfaceCode(options.Document, options.RootNamespace);");
                            WriteLine($"interfaceFile.Path = Dependencies.PathHelper.Supervisor(options.OutputDir, options.SupervisorInterfaceName);");
                            WriteLine("model.Class = classFile;");
                            WriteLine("model.Interface = interfaceFile;");
                        }
                        else
                        {
                            WriteLine("foreach(var kvp in options.Document.Components.Schemas)");
                            using (OpenBlock())
                            {
                                WriteLine("var name = kvp.Key;");
                                WriteLine("var schema = kvp.Value;");
                                WriteLine($"var code = Generator.WriteCode(schema, name, Dependencies.Namespace.{kind}(options.RootNamespace));");
                                WriteLine($"var path = Dependencies.PathHelper.{kind}(options.OutputDir, name);");
                                WriteLine($"var file = new ScaffoldedFile {{ Code = code, Path = path }};");
                                WriteLine($"model.Files.Add(file);");
                            }
                        }

                        WriteLine("return model;");
                    }
                }
            }
            return GetText();
        }

        private static string GenerateGeneratorInterface(CodeGenerationOptions options, Kind kind)
        {
            Clear();
            string ns = NamespaceHelper.MakeNamespace(options.RootNamespace, kind);
            WriteUsings(options, kind);
            WriteLine($"namespace {ns}");
            using (OpenBlock())
            {
                string typeName = NameHelper.MakeGeneratorInterfaceName(kind);

                WriteLine($"public interface {typeName}");
                using (OpenBlock())
                {
                    if (kind == Kind.Context || kind == Kind.Supervisor)
                    {
                        WriteLine($"string WriteClassCode(OpenApiDocument document, string @namespace);");
                        WriteLine();
                        WriteLine($"string WriteInterfaceCode(OpenApiDocument document, string @namespace);");
                    }
                    else
                    {
                        WriteLine($"string WriteCode(OpenApiSchema schema, string name, string @namespace);");
                    }
                }
            }
            return GetText();
        }

        private static string GenerateGeneratorClass(CodeGenerationOptions options, Kind kind)
        {
            Clear();
            string ns = NamespaceHelper.MakeNamespace(options.RootNamespace, kind);
            WriteUsings(options, kind);
            WriteLine($"namespace {ns}");
            using (OpenBlock())
            {
                string typeName = NameHelper.MakeGeneratorClassName(kind);
                string baseTypeName = NameHelper.MakeGeneratorInterfaceName(kind);

                WriteLine($"public class {typeName} : AbstractGenerator, {baseTypeName}");
                using (OpenBlock())
                {
                    WriteLine($"public {typeName}(GeneratorDependencies dependencies) : base(dependencies)");
                    using (OpenBlock())
                    {
                    }

                    if (kind == Kind.Context || kind == Kind.Supervisor)
                    {
                        WriteLine($"public string WriteClassCode(OpenApiDocument document, string @namespace)");
                        using (OpenBlock())
                        {
                            WriteLine("Clear();");
                            WriteLine("GenerateFileHeader();");
                            WriteLine("WriteLine($\"namespace {@namespace}\");");
                            WriteLine("using (OpenBlock())");
                            using (OpenBlock())
                            {
                            }
                            WriteLine("return GetText();");
                        }
                        WriteLine();
                        WriteLine($"public string WriteInterfaceCode(OpenApiDocument document, string @namespace)");
                        using (OpenBlock())
                        {
                            WriteLine("Clear();");
                            WriteLine("GenerateFileHeader();");
                            WriteLine("WriteLine($\"namespace {@namespace}\");");
                            WriteLine("using (OpenBlock())");
                            using (OpenBlock())
                            {
                            }
                            WriteLine("return GetText();");
                        }
                    }
                    else
                    {
                        WriteLine($"public string WriteCode(OpenApiSchema schema, string name, string @namespace)");
                        using (OpenBlock())
                        {
                            WriteLine("Clear();");
                            WriteLine("GenerateFileHeader();");
                            WriteLine("WriteLine($\"namespace {@namespace}\");");
                            WriteLine("using (OpenBlock())");
                            using (OpenBlock())
                            {
                                WriteLine("");
                            }
                            WriteLine("return GetText();");
                        }
                    }

                }
            }
            return GetText();
        }

        private static string GenerateSupervisorModelClass(CodeGenerationOptions options)
        {
            Clear();
            string ns = NamespaceHelper.MakeNamespace(options.RootNamespace, Kind.Supervisor);
            WriteUsings(options, Kind.Supervisor);
            WriteLine($"namespace {ns}");
            using (OpenBlock())
            {
                string typeName = NameHelper.MakeSupervisorModelClassName();

                WriteLine($"public class {typeName}");
                using (OpenBlock())
                {
                    WriteLine($"public ScaffoldedFile SupervisorClass {{ get; set; }}");
                    WriteLine();
                    WriteLine($"public ScaffoldedFile SupervisorInterface {{ get; set; }}");
                }
            }
            return GetText();
        }

        private static string GenerateContextModelClass(CodeGenerationOptions options)
        {
            Clear();
            string ns = NamespaceHelper.MakeNamespace(options.RootNamespace, Kind.Context);
            WriteUsings(options, Kind.Context);
            WriteLine($"namespace {ns}");
            using (OpenBlock())
            {
                string typeName = NameHelper.MakeSupervisorModelClassName();

                WriteLine($"public class {typeName}");
                using (OpenBlock())
                {
                    WriteLine($"public ScaffoldedFile ContextClass {{ get; set; }}");
                    WriteLine();
                    WriteLine($"public ScaffoldedFile ContextInterface {{ get; set; }}");
                }
            }
            return GetText();
        }

        private static string GenerateModelClass(CodeGenerationOptions options, Kind kind)
        {
            Clear();
            string ns = NamespaceHelper.MakeNamespace(options.RootNamespace, kind);
            WriteUsings(options, kind);
            WriteLine($"namespace {ns}");
            using (OpenBlock())
            {
                string typeName = NameHelper.MakeModelClassName(kind);

                WriteLine($"public class {typeName}");
                using (OpenBlock())
                {
                    if (kind == Kind.Context || kind == Kind.Supervisor)
                    {
                        WriteLine($"public ScaffoldedFile Class {{ get; set; }}");
                        WriteLine();
                        WriteLine($"public ScaffoldedFile Interface {{ get; set; }}");
                    }
                    else
                    {
                        WriteLine($"public IList<ScaffoldedFile> Files {{ get; }} = new List<ScaffoldedFile>();");
                    }
                }
            }
            return GetText();
        }

        private static string GenerateScaffoldedModelClass(CodeGenerationOptions options)
        {
            Clear();
            WriteUsings(options, Kind.None);
            WriteLine($"namespace {options.RootNamespace}");
            using (OpenBlock())
            {
                WriteLine("public class ScaffoldedModel");
                using (OpenBlock())
                {
                    foreach (var kind in options.Kinds)
                    {
                        //if (kind == Kind.Supervisor || kind == Kind.Context)
                        //    continue;

                        string modelName = NameHelper.MakeModelClassName(kind);
                        string name = NameHelper.MakeName(kind);
                        WriteLine();
                        WriteLine($"public {modelName} {name} {{ get; set; }}");
                    }
                }
            }
            return GetText();
        }

        private static string GenerateSupervisorClass(CodeGenerationOptions options)
        {
            Clear();
            string ns = NamespaceHelper.MakeNamespace(options.RootNamespace, Kind.Supervisor);
            WriteUsings(options, Kind.Supervisor);
            WriteLine($"namespace {ns}");
            using (OpenBlock())
            {
                string typeName = NameHelper.MakeSupervisorClassName();
                string baseTypeName = NameHelper.MakeSupervisorInterfaceName();

                WriteLine($"public class {typeName} : {baseTypeName}");
                using (OpenBlock())
                {
                }
            }
            return GetText();
        }

        private static string GenerateSupervisorInterface(CodeGenerationOptions options)
        {
            Clear();
            string ns = NamespaceHelper.MakeNamespace(options.RootNamespace, Kind.Supervisor);
            WriteUsings(options, Kind.Supervisor);
            WriteLine($"namespace {ns}");
            using (OpenBlock())
            {
                string typeName = NameHelper.MakeSupervisorInterfaceName();

                WriteLine($"public interface {typeName}");
                using (OpenBlock())
                {
                }
            }
            return GetText();
        }

        private static string GenerateContextClass(CodeGenerationOptions options)
        {
            Clear();
            string ns = NamespaceHelper.MakeNamespace(options.RootNamespace, Kind.Context);
            WriteUsings(options, Kind.Context);
            WriteLine($"namespace {ns}");
            using (OpenBlock())
            {
                string typeName = NameHelper.MakeContextClassName();
                string baseTypeName = NameHelper.MakeContextInterfaceName();

                WriteLine($"public class {typeName} : {baseTypeName}");
                using (OpenBlock())
                {
                }
            }
            return GetText();
        }

        private static string GenerateContextInterface(CodeGenerationOptions options)
        {
            Clear();
            string ns = NamespaceHelper.MakeNamespace(options.RootNamespace, Kind.Context);
            WriteUsings(options, Kind.Context);
            WriteLine($"namespace {ns}");
            using (OpenBlock())
            {
                string typeName = NameHelper.MakeContextInterfaceName();

                WriteLine($"public interface {typeName}");
                using (OpenBlock())
                {
                }
            }
            return GetText();
        }

        public static void WriteUsings(CodeGenerationOptions options, Kind skip)
        {
            WriteLine("using System;");
            WriteLine("using System.IO;");
            WriteLine("using System.Collections.Generic;");
            WriteLine("using Microsoft.OpenApi.Models;");
            WriteLine("using Microsoft.Extensions.DependencyInjection;");
            WriteLine($"using {options.RootNamespace}.Utilities;");
            WriteLine($"using {options.RootNamespace}.Scaffolding;");
            foreach (var kind in options.Kinds)
            {
                if (kind == skip)
                    continue;
                string ns = NamespaceHelper.MakeNamespace(options.RootNamespace, kind);
                WriteLine($"using {ns};");
            }
            WriteLine();
        }

        public static void PushIndent()
        {
            _writer.PushIndent();
        }

        public static void PopIndent()
        {
            _writer.PopIndent();
        }

        public static void Write(string value)
        {
            _writer.Write(value);
        }

        public static void WriteLine(string value)
        {
            _writer.WriteLine(value);
        }

        public static void WriteLine()
        {
            _writer.WriteLine();
        }

        public static IDisposable OpenBlock()
        {
            return _writer.OpenBlock();
        }

        public static void Clear()
        {
            _writer.Clear();
        }

        public static string GetText()
        {
            return _writer.GetText();
        }
    }
}
