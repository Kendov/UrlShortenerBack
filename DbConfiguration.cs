namespace urlShortener
{
    public class DbConfiguration : IDbConfiguration
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string IsSSL { get; set; }
    }

    public interface IDbConfiguration
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string IsSSL { get; set; }
    }
}