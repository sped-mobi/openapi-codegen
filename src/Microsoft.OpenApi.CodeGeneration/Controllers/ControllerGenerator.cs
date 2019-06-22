// -----------------------------------------------------------------------
// <copyright file="ControllerGenerator.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.Controllers
{
    public class ControllerGenerator : AbstractGenerator, IControllerGenerator
    {
        public ControllerGenerator(GeneratorDependencies dependencies) : base(dependencies)
        {
        }

        public string WriteCode(OpenApiSchema schema, string name, string @namespace)
        {
            string defaultRoute = Dependencies.Document.Options.DefaultRoute;
            string rootNamespace = Dependencies.Document.Options.RootNamespace;
            string supervisorNamespace = Dependencies.Namespace.Supervisor(rootNamespace);
            string viewModelNamespace = Dependencies.Namespace.ViewModel(rootNamespace);
            Clear();
            GenerateFileHeader();
            WriteLine("using System;");
            WriteLine("using System.Collections.Generic;");
            WriteLine("using System.Linq;");
            WriteLine("using System.Threading.Tasks;");
            WriteLine("using Microsoft.AspNetCore.Mvc;");
            WriteLine("using System.Threading;");
            WriteLine("using Newtonsoft.Json;");
            WriteLine("using System.Diagnostics;");
            WriteLine($"using {supervisorNamespace};");
            WriteLine($"using {viewModelNamespace};");
            WriteLine();
            WriteLine($"namespace {@namespace}");
            using (OpenBlock())
            {
                string supervisorInterfaceName = Dependencies.Document.Options.SupervisorInterfaceName;
                string supervisorFieldName = Dependencies.Document.Options.SupervisorFieldName;
                string supervisorParmetername = Dependencies.Document.Options.SupervisorParameterName;
                string entityName = Dependencies.Namer.Entity(name);
                string viewModelName = Dependencies.Namer.ViewModel(name);
                string primaryKeyTypeName = Dependencies.Document.Options.PrimaryKeyTypeName;
                string controllerName = Dependencies.Namer.Controller(name);
                WriteLine($"[Route(\"{defaultRoute}\")]");
                WriteLine($"public class {controllerName} : Controller");
                using (OpenBlock())
                {
                    WriteLine($"private readonly {supervisorInterfaceName} {supervisorFieldName};");
                    WriteLine();
                    WriteLine($"public {controllerName}({supervisorInterfaceName} {supervisorParmetername})");
                    using (OpenBlock())
                    {
                        WriteLine($"{supervisorFieldName} = {supervisorParmetername};");
                    }

                    WriteLine();
                    WriteLine("[HttpGet]");
                    WriteLine($"[Produces(typeof(List<{viewModelName}>))]");
                    WriteLine("public async Task<IActionResult> Get(CancellationToken ct = default)");
                    using (OpenBlock())
                    {
                        WriteLine("try");
                        using (OpenBlock())
                        {
                            WriteLine($"return new ObjectResult(await {supervisorFieldName}.GetAll{entityName}Async(ct));");
                        }

                        WriteLine("catch(Exception ex)");
                        using (OpenBlock())
                        {
                            WriteLine("return StatusCode(500, ex);");
                        }
                    }

                    WriteLine();
                    WriteLine("[HttpGet(\"{id}\")]");
                    WriteLine($"[Produces(typeof({viewModelName}))]");
                    WriteLine(
                        $"public async Task<ActionResult<{viewModelName}>> Get({primaryKeyTypeName} id, CancellationToken ct = default)");
                    using (OpenBlock())
                    {
                        WriteLine("try");
                        using (OpenBlock())
                        {
                            WriteLine($"var entity = await {supervisorFieldName}.Get{entityName}ByIdAsync(id, ct);");
                            WriteLine("if (entity == null)");
                            PushIndent();
                            WriteLine("return NotFound();");
                            PopIndent();
                            WriteLine("return Ok(entity);");
                        }

                        WriteLine("catch(Exception ex)");
                        using (OpenBlock())
                        {
                            WriteLine("return StatusCode(500, ex);");
                        }
                    }

                    WriteLine();
                    WriteLine("[HttpPost]");
                    WriteLine($"[Produces(typeof({viewModelName}))]");
                    WriteLine(
                        $"public async Task<ActionResult<{viewModelName}>> Post({primaryKeyTypeName} id, [FromBody] {viewModelName} input, CancellationToken ct = default)");
                    using (OpenBlock())
                    {
                        WriteLine("try");
                        using (OpenBlock())
                        {
                            WriteLine("if (input == null)");
                            PushIndent();
                            WriteLine("return BadRequest();");
                            PopIndent();
                            WriteLine();
                            WriteLine($"return StatusCode(201, await {supervisorFieldName}.Add{entityName}Async(input, ct));");
                        }

                        WriteLine("catch(Exception ex)");
                        using (OpenBlock())
                        {
                            WriteLine("return StatusCode(500, ex);");
                        }
                    }

                    WriteLine();
                    WriteLine("[HttpPut(\"{id}\")]");
                    WriteLine($"[Produces(typeof({viewModelName}))]");
                    WriteLine(
                        $"public async Task<ActionResult<{viewModelName}>> Put({primaryKeyTypeName} id, [FromBody] {viewModelName} input, CancellationToken ct = default)");
                    using (OpenBlock())
                    {
                        WriteLine("try");
                        using (OpenBlock())
                        {
                            WriteLine("if (input == null)");
                            PushIndent();
                            WriteLine("return BadRequest();");
                            PopIndent();
                            WriteLine();
                            WriteLine($"if (await {supervisorFieldName}.Get{entityName}ByIdAsync(id, ct) == null)");
                            PushIndent();
                            WriteLine("return NotFound();");
                            PopIndent();
                            WriteLine();
                            WriteLine($"if (await {supervisorFieldName}.Update{entityName}Async(input, ct))");
                            PushIndent();
                            WriteLine("return Ok(input);");
                            PopIndent();
                            WriteLine();
                            WriteLine("return StatusCode(500);");
                        }

                        WriteLine("catch(Exception ex)");
                        using (OpenBlock())
                        {
                            WriteLine("return StatusCode(500, ex);");
                        }
                    }

                    WriteLine();
                    WriteLine("[HttpDelete(\"{id}\")]");
                    WriteLine("[Produces(typeof(void))]");
                    WriteLine(
                        $"public async Task<ActionResult<{viewModelName}>> Delete({primaryKeyTypeName} id, CancellationToken ct = default)");
                    using (OpenBlock())
                    {
                        WriteLine("try");
                        using (OpenBlock())
                        {
                            WriteLine();
                            WriteLine($"if (await {supervisorFieldName}.Get{entityName}ByIdAsync(id, ct) == null)");
                            PushIndent();
                            WriteLine("return NotFound();");
                            PopIndent();
                            WriteLine();
                            WriteLine($"if (await {supervisorFieldName}.Delete{entityName}Async(id, ct))");
                            PushIndent();
                            WriteLine("return Ok();");
                            PopIndent();
                            WriteLine();
                            WriteLine("return StatusCode(500);");
                        }

                        WriteLine("catch(Exception ex)");
                        using (OpenBlock())
                        {
                            WriteLine("return StatusCode(500, ex);");
                        }
                    }
                }
            }

            return GetText();
        }
    }
}
