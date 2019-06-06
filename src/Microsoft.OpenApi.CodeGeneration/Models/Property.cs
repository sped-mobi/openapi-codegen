using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Models
{
    public class Property
    {

        public Property(Entity definingEntity, string name, string type)
        {
            DefiningEntity = definingEntity;
            Name = name;
            Type = type;
        }

        public IList<Key> Keys { get; } = new List<Key>();

        public IList<ForeignKey> ForeignKeys { get; } = new List<ForeignKey>();

        public Entity DefiningEntity { get; }

        public string Name { get; }

        public string Type { get; }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"({DefiningEntity})[{Name} {Type}]";
        }
    }
}
