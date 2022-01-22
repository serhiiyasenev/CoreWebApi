namespace Common.Storages
{
    public static class StringStorage
    {
        public static string SecretSectionName => "JwtConfig:Secret";
        public static string ExpirationSectionName => "JwtConfig:ExpirationInSeconds";
    }
}
