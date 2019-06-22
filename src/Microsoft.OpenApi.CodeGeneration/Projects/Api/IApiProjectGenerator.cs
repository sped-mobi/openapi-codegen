// -----------------------------------------------------------------------
// <copyright file="IApiProjectGenerator.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public interface IApiProjectGenerator
    {
        string WriteProjectFile(OpenApiOptions options);

        string WriteProgramCSFile(OpenApiOptions options);

        string WriteStartupCSFile(OpenApiOptions options);

        string WriteAppSettingsJSONFile(OpenApiOptions options);

        string WriteAppSettingsDevelopmentJSONFile(OpenApiOptions options);

        string WriteServicesConfigurationCSFile(OpenApiOptions options);
    }
}
