namespace Microsoft.OpenApi.CodeGeneration.Models
{
    public class Key
    {
        public Key(Property property, bool isUnique = false)
        {
            Property = property;
            IsUnique = isUnique;
        }

        public Property Property { get; }

        public bool IsUnique { get; }

        public Entity DefiningEntity
        {
            get
            {
                return Property.DefiningEntity;
            }
        }
    }
}