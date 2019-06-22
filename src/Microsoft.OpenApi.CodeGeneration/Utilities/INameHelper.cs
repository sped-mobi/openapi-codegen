// -----------------------------------------------------------------------
// <copyright file="INameHelper.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Utilities
{
    public interface INameHelper
    {
        string Configuration(string name);

        string Controller(string name);

        string Converter(string name);

        string Entity(string name);

        string Repository(string name);

        string RepositoryInterface(string name);

        string RepositoryFieldName(string name);

        string RepositoryParameterName(string name);

        string ViewModel(string name);
    }
}
