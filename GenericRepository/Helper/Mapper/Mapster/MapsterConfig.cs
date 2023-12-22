namespace GenericRepository.Helper.Mapper.Mapster
{
    public class MapsterConfig
    {
        public static void Configure()
        {
            // İlk tip için TypeAdapter tanımı
            new UserTypeAdapter();
        
        }
    }
}

