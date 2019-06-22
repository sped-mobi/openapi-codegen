// -----------------------------------------------------------------------
// <copyright file="ForeignKey.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Models
{
    public class ForeignKey
    {
        public ForeignKey(Property property, Key principalKey, Entity declaringEntity, Entity principalEntity)
        {
            Property = property;
            PrincipalKey = principalKey;
            DeclaringEntity = declaringEntity;
            PrincipalEntity = principalEntity;
        }

        public Property Property { get; }

        public Key PrincipalKey { get; }

        public Entity DeclaringEntity { get; }

        public Entity PrincipalEntity { get; }
    }
}
