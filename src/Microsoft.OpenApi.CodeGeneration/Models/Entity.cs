// -----------------------------------------------------------------------
// <copyright file="Entity.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OpenApi.CodeGeneration.Models
{
    public class Entity
    {
        private Key _primaryKey;
        private readonly Dictionary<Property, Key> _keys = new Dictionary<Property, Key>();
        private readonly Dictionary<Property, ForeignKey> _foreignkeys = new Dictionary<Property, ForeignKey>();
        private readonly Dictionary<string, Property> _properties = new Dictionary<string, Property>();

        public Entity(EntityModel model, string name)
        {
            Name = name;
            Model = model;
        }

        internal bool Initialized { get; set; }

        public EntityModel Model { get; }

        public Key PrimaryKey
        {
            get
            {
                return _primaryKey;
            }
        }

        public string Name { get; }

        public Key FindPrimaryKey()
        {
            return _keys.Values.FirstOrDefault();
        }

        public Key SetPrimaryKey(Property property)
        {
            if (!_keys.TryGetValue(property, out Key value))
            {
                Key key = new Key(property);
                _keys.Add(property, key);
                _primaryKey = key;
            }

            return _primaryKey;
        }

        public IEnumerable<Property> GetProperties()
        {
            return _properties.Values;
        }

        public Property AddProperty(string name, string type)
        {
            if (!_properties.TryGetValue(name, out Property value))
            {
                Property property = new Property(this, name, type);
                _properties.Add(name, property);
                return property;
            }

            return null;
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
