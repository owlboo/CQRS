namespace CQRS.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static Type[] RegisterMappings()
        {
            return new Type[]
            {
                typeof(ViewModelToDTO)
            };
        }
    }
}
