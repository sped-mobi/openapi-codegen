// -----------------------------------------------------------------------
// <copyright file="ApiProjectGenerator.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public class ApiProjectGenerator : ProjectGeneratorBase, IApiProjectGenerator
    {
        public ApiProjectGenerator(GeneratorDependencies dependencies) : base(dependencies)
        {
        }

        public override string WriteProjectFile(OpenApiOptions options)
        {
            Clear();
            using (OpenProjectBlock())
            {
                using (OpenPropertyGroupBlock())
                {
                    WriteProperty("RootNamespace", options.RootNamespace);
                    WriteProperty("TargetFramework", "netcoreapp2.2");
                }

                WriteLine();
                WriteSdkElement("Microsoft.NET.Sdk.Web");
                WriteLine();
                using (OpenItemGroupBlock())
                {
                    WriteLine("<Content Update=\"appsettings.json\" CopyToOutputDirectory=\"Always\"/>");
                }

                using (OpenItemGroupBlock())
                {
                    WriteLine("<PackageReference Include=\"Microsoft.AspNetCore.App\" />");
                    WriteLine("<PackageReference Include=\"Microsoft.Extensions.Logging\" Version=\"2.2.0\" />");
                    WriteLine("<PackageReference Include=\"Newtonsoft.Json\" Version=\"12.0.2\" />");
                    WriteLine("<PackageReference Include=\"Swashbuckle.AspNetCore\" Version=\"4.0.1\" />");
                }

                using (OpenItemGroupBlock())
                {
                    WriteLine("<DotNetCliToolReference Include=\"Microsoft.VisualStudio.Web.CodeGeneration.Tools\" Version=\"2.0.0\" />");
                }

                using (OpenItemGroupBlock())
                {
                    WriteLine($"<ProjectReference Include=\"..\\{options.DataProjectName}\\{options.DataProjectName}.csproj\"/>");
                    WriteLine($"<ProjectReference Include=\"..\\{options.CoreProjectName}\\{options.CoreProjectName}.csproj\"/>");
                }
            }

            return GetText();
        }

        public string WriteProgramCSFile(OpenApiOptions options)
        {
            Clear();
            GenerateFileHeader();
            WriteLine("using Microsoft.AspNetCore;");
            WriteLine("using Microsoft.AspNetCore.Hosting;");
            WriteLine();
            WriteLine($"namespace {options.RootNamespace}");
            using (OpenBlock())
            {
                WriteLine("public class Program");
                using (OpenBlock())
                {
                    WriteLine("public static void Main(string[] args) => BuildWebHost(args).Run();");
                    WriteLine();
                    WriteLine("private static IWebHost BuildWebHost(string[] args) =>");
                    PushIndent();
                    WriteLine("WebHost.CreateDefaultBuilder(args)");
                    PushIndent();
                    WriteLine(".UseStartup<Startup>()");
                    WriteLine(".Build();");
                    PopIndent();
                    PopIndent();
                }
            }

            return GetText();
        }

        public string WriteStartupCSFile(OpenApiOptions options)
        {
            Clear();
            WriteLine("using Microsoft.AspNetCore.Builder;");
            WriteLine("using Microsoft.AspNetCore.Hosting;");
            WriteLine("using Microsoft.Extensions.Configuration;");
            WriteLine("using Microsoft.Extensions.DependencyInjection;");
            WriteLine("using Microsoft.Extensions.Logging;");
            WriteLine("using Swashbuckle.AspNetCore.Swagger;");
            WriteLine();
            WriteLine($"namespace {options.RootNamespace}");
            using (OpenBlock())
            {
                WriteLine("public class Startup");
                using (OpenBlock())
                {
                    WriteLine("private IConfiguration Configuration { get; }");
                    WriteLine();
                    WriteLine("public Startup(IConfiguration configuration)");
                    using (OpenBlock())
                    {
                        WriteLine("Configuration = configuration;");
                    }

                    WriteLine();
                    WriteLine("public void ConfigureServices(IServiceCollection services)");
                    using (OpenBlock())
                    {
                        WriteLine("services.AddMemoryCache();");
                        WriteLine("services.AddResponseCaching();");
                        WriteLine("services.AddMvc();");
                        WriteLine("services.ConfigureRepositories()");
                        PushIndent();
                        WriteLine(".ConfigureSupervisor()");
                        WriteLine(".AddMiddleware()");
                        WriteLine(".AddCorsConfiguration()");
                        WriteLine(".AddConnectionProvider(Configuration);");

                        //WriteLine(".AddAppSettings(Configuration);");
                        PopIndent();
                        WriteLine("services.AddSwaggerGen(s => s.SwaggerDoc(\"v1\", new Info");
                        using (OpenBlockString("));"))
                        {
                            WriteLine($"Title = \"{options.Document.Info.Title}\",");
                            WriteLine($"Description = \"{options.Document.Info.Description}\"");
                        }
                    }

                    WriteLine();
                    WriteLine("public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)");
                    using (OpenBlock())
                    {
                        WriteLine("if (env.IsDevelopment())");
                        using (OpenBlock())
                        {
                            WriteLine("app.UseDeveloperExceptionPage();");
                        }

                        WriteLine("app.UseCors(\"AllowAll\");");
                        WriteLine("app.UseStaticFiles();");
                        WriteLine("app.UseMvc(");
                        WriteLine("routes => routes.MapRoute(");
                        WriteLine("\"default\",");
                        WriteLine("\"{controller=Home}/{action=Index}/{id?}\"));");
                        WriteLine("app.UseSwagger();");
                        WriteLine("app.UseSwaggerUI(s => s.SwaggerEndpoint(\"/swagger/v1/swagger.json\", \"v1 docs\"));");
                    }
                }


            }
            return GetText();
        }

        public string WriteAppSettingsJSONFile(OpenApiOptions options)
        {
            Clear();
            WriteLine("{");
            WriteLine("  \"ConnectionStrings\": {");
            WriteLine($"    \"DefaultDbConnection\": \"Server=.;Database={options.DatabaseName};Trusted_Connection=True\"");
            WriteLine("  },");
            WriteLine("  \"Logging\": {");
            WriteLine("    \"IncludeScopes\": false,");
            WriteLine("    \"Debug\": {");
            WriteLine("      \"LogLevel\": {");
            WriteLine("        \"Default\": \"Warning\"");
            WriteLine("      }");
            WriteLine("    },");
            WriteLine("    \"Console\": {");
            WriteLine("      \"LogLevel\": {");
            WriteLine("        \"Default\": \"Warning\"");
            WriteLine("      }");
            WriteLine("    }");
            WriteLine("  }");
            WriteLine("}");
            return GetText();
        }

        public string WriteAppSettingsDevelopmentJSONFile(OpenApiOptions options)
        {
            Clear();
            WriteLine("{");
            WriteLine("  \"Logging\": {");
            WriteLine("    \"IncludeScopes\": false,");
            WriteLine("    \"LogLevel\": {");
            WriteLine("      \"Default\": \"Debug\",");
            WriteLine("      \"System\": \"Information\",");
            WriteLine("      \"Microsoft\": \"Information\"");
            WriteLine("    }");
            WriteLine("  }");
            WriteLine("}");
            return GetText();
        }

        public string WriteServicesConfigurationCSFile(OpenApiOptions options)
        {
            Clear();
            string repositoryNamespace = Dependencies.Namespace.Repository(options.RootNamespace);
            string supervisorNamespace = Dependencies.Namespace.Supervisor(options.RootNamespace);
            string contextNamespace = Dependencies.Namespace.Context(options.RootNamespace);
            WriteLine("using Microsoft.EntityFrameworkCore;");
            WriteLine("using Microsoft.Extensions.Configuration;");
            WriteLine("using Microsoft.Extensions.DependencyInjection;");
            WriteLine("using Microsoft.Extensions.Logging;");
            WriteLine("using Newtonsoft.Json;");
            WriteLine($"using {repositoryNamespace};");
            WriteLine($"using {supervisorNamespace};");
            WriteLine($"using {contextNamespace};");
            WriteLine();
            WriteLine($"namespace {options.RootNamespace}");
            using (OpenBlock())
            {
                WriteLine("public static class ServicesConfiguration");
                using (OpenBlock())
                {
                    WriteLine();
                    WriteLine(
                        "public static IServiceCollection AddConnectionProvider(this IServiceCollection services, IConfiguration configuration)");
                    using (OpenBlock())
                    {
                        WriteLine("string connection = configuration.GetConnectionString(\"DefaultDbConnection\");");
                        WriteLine($"services.AddDbContextPool<{options.ContextClassName}>(options => options.UseSqlServer(connection));");
                        WriteLine("return services;");
                    }

                    WriteLine("public static IServiceCollection ConfigureRepositories(this IServiceCollection services)");
                    using (OpenBlock())
                    {
                        Write("services");
                        var schemas = options.Document.GetSchemas().ToList();
                        for (int i = 0; i < schemas.Count; i++)
                        {
                            var kvp = schemas[i];
                            string name = kvp.Key;
                            string repositoryInterface = Dependencies.Namer.RepositoryInterface(name);
                            string repositoryClass = Dependencies.Namer.Repository(name);
                            if (i == 0)
                            {
                                WriteLine($".AddScoped<{repositoryInterface}, {repositoryClass}>()");
                            }
                            else
                            {
                                PushIndent();
                                Write($".AddScoped<{repositoryInterface}, {repositoryClass}>()");
                                if (i == schemas.Count - 1)
                                {
                                    WriteLine(";");
                                }
                                else
                                {
                                    WriteLine();
                                }

                                PopIndent();
                            }
                        }

                        WriteLine("return services;");
                    }

                    WriteLine("public static IServiceCollection ConfigureSupervisor(this IServiceCollection services)");
                    using (OpenBlock())
                    {
                        WriteLine($"services.AddScoped<{options.SupervisorInterfaceName}, {options.SupervisorClassName}>();");
                        WriteLine("return services;");
                    }

                    WriteLine("public static IServiceCollection AddMiddleware(this IServiceCollection services)");
                    using (OpenBlock())
                    {
                        WriteLine("services.AddMvc().AddJsonOptions(options =>");
                        using (OpenBlockString(");"))
                        {
                            WriteLine("options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;");
                            WriteLine("options.SerializerSettings.Formatting = Formatting.Indented;");
                        }

                        WriteLine("return services;");
                    }

                    WriteLine();
                    WriteLine("        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services) =>");
                    WriteLine("            services.AddCors(options =>");
                    WriteLine("            {");
                    WriteLine("                options.AddPolicy(\"AllowAll\",");
                    WriteLine("                    x =>");
                    WriteLine("                    {");
                    WriteLine("                        new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder()");
                    WriteLine("                            .AllowAnyHeader()");
                    WriteLine("                            .AllowAnyMethod()");
                    WriteLine("                            .AllowAnyOrigin()");
                    WriteLine("                            .AllowCredentials()");
                    WriteLine("                            .Build();");
                    WriteLine("                    });");
                    WriteLine("            });");

                    WriteLine();
                    WriteLine("public static IServiceCollection AddLogging(this IServiceCollection services)");
                    using (OpenBlock())
                    {
                        WriteLine("services.AddLogging(builder => builder");
                        PushIndent();
                        WriteLine(".AddConsole()");
                        Write(".AddFilter(level => level >= LogLevel.Information)");
                        WriteLine(");");
                        PopIndent();
                        WriteLine("return services;");
                    }
                }
            }

            return GetText();
        }
    }
}
