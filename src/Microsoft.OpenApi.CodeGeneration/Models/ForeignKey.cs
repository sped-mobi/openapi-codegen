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