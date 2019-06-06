// -----------------------------------------------------------------------
// <copyright file="INameHelper.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
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

    public class NameHelper : INameHelper
    {
        public string Configuration(string name)
        {
            return name + "Configuration";
        }

        public string Controller(string name)
        {
            return name + "Controller";
        }

        public string Converter(string name)
        {
            return name + "Converter";
        }

        public string Entity(string name)
        {
            return name;
        }

        public string Repository(string name)
        {
            return name + "Repository";
        }

        public string RepositoryInterface(string name)
        {
            return "I" + Repository(name);
        }

        public string RepositoryFieldName(string name)
        {
            return "_" + RepositoryParameterName(name);
        }

        public string RepositoryParameterName(string name)
        {
            return StringUtilities.MakeCamel(Repository(name)); ;
        }

        public string ViewModel(string name)
        {
            return name + "ViewModel";
        }

    }
}
