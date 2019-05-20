namespace Microsoft.OpenApi.CodeGeneration.Internal
{
    public static class PluralNameHelper
    {
        public static string MakePlural(Kind kind)
        {
            string name = kind.ToString();
            return MakePlural(name);
        }

        public static string MakePlural(string name)
        {
            switch (name)
            {
                case "Supervisor":
                case "Context":
                    return name;
            }

            if (!name.EndsWith("y"))
            {
                return name + "s";
            }
            else
            {
                int length = name.Length - 1;
                string nameNoY = name.Substring(0, length);

                return nameNoY + "ies";
            }
        }
    }
}
