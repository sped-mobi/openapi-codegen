namespace Microsoft.OpenApi.CodeGeneration.Utilities
{
    public class NameHelper : INameHelper
    {
        private static string Base(string name)
        {
            if (name.Contains("_"))
            {
                name = StringUtilities.MakePascal(name.Split("_")[1]);
            }

            return StringUtilities.MakePascal(name);
        }

        public string Configuration(string name)
        {
            return Base(name) + "Configuration";
        }

        public string Controller(string name)
        {
            return Base(name) + "Controller";
        }

        public string Converter(string name)
        {
            return Base(name) + "Converter";
        }

        public string Entity(string name)
        {
            return Base(name);
        }

        public string Repository(string name)
        {
            return Base(name) + "Repository";
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
            return StringUtilities.MakeCamel(Repository(name));
            ;
        }

        public string ViewModel(string name)
        {
            return Base(name) + "ViewModel";
        }
    }
}
