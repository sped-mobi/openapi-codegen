// -----------------------------------------------------------------------
// <copyright file="ISupervisorInterfaceGenerator.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.SupervisorInterface
{
    public interface ISupervisorInterfaceGenerator
    {
        string WriteCode(OpenApiOptions options);
    }
}

