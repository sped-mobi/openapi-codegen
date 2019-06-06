// -----------------------------------------------------------------------
// <copyright file="EntityModel.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Models
{
    public class EntityModel
    {
        private readonly Dictionary<string, Entity> _entities = new Dictionary<string, Entity>();

        public Entity AddEntity(string name)
        {
            if (_entities.TryGetValue(name, out _)) return null;
            Entity entity = new Entity(this, name);
            _entities.Add(name, entity);
            return entity;
        }

        public Entity GetOrAddEntity(string name)
        {
            return FindEntity(name) ?? AddEntity(name);
        }

        public Entity FindEntity(string name)
        {
            return _entities.TryGetValue(name, out Entity value) ? value : null;
        }

        public IList<Entity> GetEntities()
        {
            return new List<Entity>(_entities.Values);
        }
    }
}
