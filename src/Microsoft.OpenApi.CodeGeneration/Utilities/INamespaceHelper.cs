// -----------------------------------------------------------------------
// <copyright file="INamespaceHelper.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Utilities
{
    public interface INamespaceHelper
    {
        string Configuration(string rootNamespace);

        string Controller(string rootNamespace);

        string Converter(string rootNamespace);

        string Entity(string rootNamespace);

        string Repository(string rootNamespace);

        string ViewModel(string rootNamespace);

        string Supervisor(string rootNamespace);

        string Context(string rootNamespace);
    }
}
