// -----------------------------------------------------------------------
// <copyright file="ISupervisorGenerator.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Supervisor
{
    public interface ISupervisorGenerator
    {
        string WriteClassCode(IOpenApiDocument document, string supervisorName, string supervisorInterfaceName, string @namespace);

        string WriteInterfaceCode(IOpenApiDocument document, string supervisorInterfaceName, string @namespace);
    }
}
