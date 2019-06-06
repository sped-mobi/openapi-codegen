// -----------------------------------------------------------------------
// <copyright file="EntityModelBuilder.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using System.Xml.Serialization;
using Microsoft.OpenApi.CodeGeneration.Utilities;
using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.Models
{
    public class EntityModelBuilder : IEntityModelBuilder
    {
        protected ISchemaConverter Converter { get; }

        public EntityModelBuilder(ISchemaConverter converter)
        {
            Converter = converter;
        }


        private EntityModel Model { get; } = new EntityModel();

        public EntityModel BuildModel(OpenApiOptions options)
        {
            foreach (var kvp in options.Document.Components.Schemas.Where(x => x.Value.Type == "object"))
            {
                string key = kvp.Key;
                OpenApiSchema schema = kvp.Value;
                Entity entity = Model.AddEntity(key);
                if (entity != null)
                    Initialize(schema, entity, options);
            }

            return Model;
        }

        private void Initialize(OpenApiSchema schema, Entity entity, OpenApiOptions options)
        {
            if (entity.Initialized)
                return;
            entity.Initialized = true;
            AddPrimaryKeyProperty(entity, options);
            foreach (var kvp in schema.Properties)
            {
                //if (schema.Type == "array" || schema.Reference != null)
                //{
                //    continue;
                //}

                string propertyName = CodeIdentifier.MakePascal(kvp.Key);
                OpenApiSchema propertySchema = kvp.Value;
                string propertyType = Converter.ConvertToType(propertySchema);
                Property property = entity.AddProperty(propertyName, propertyType);
            }

            foreach (var kvp in schema.Properties)
            {
                string propertyName = kvp.Key;
                OpenApiSchema propertySchema = kvp.Value;

                if (propertySchema.Type == "array")
                {
                    Property property = entity.GetProperties().FirstOrDefault(p => p.Name == propertyName);

                    if (propertySchema.Items.Reference?.IsLocal == true)
                    {
                        string findName = propertySchema.Items.Reference.Id;
                        Entity dependentEntity = Model.GetOrAddEntity(findName);
                        OpenApiSchema openApiSchema = options.Document.Components.Schemas[findName];
                        Initialize(openApiSchema, dependentEntity, options);
                        Key principalKey = entity.FindPrimaryKey();
                        Entity principalEntity = entity;

                        string foreignKeyName = principalKey.Property.Name;

                        dependentEntity.AddProperty(foreignKeyName, options.PrimaryKeyTypeName);

                        dependentEntity.AddProperty(principalEntity.Name, principalEntity.Name);
                    }
                }
            }
        }

        private void AddPrimaryKeyProperty(Entity entity, OpenApiOptions options)
        {
            Property idProperty = new Property(entity, $"{entity.Name}ID", options.PrimaryKeyTypeName);
            entity.SetPrimaryKey(idProperty);
        }
    }
}
