// -----------------------------------------------------------------------
// <copyright file="IApiProjectGenerator.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
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
