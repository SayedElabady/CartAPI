namespace WebApplication.Models
{
    public class MoviestoreDatabaseSettings: IMoviestoreDatabaseSettings
    {
        public string MoviesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    
    public interface IMoviestoreDatabaseSettings
    {
        string MoviesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
    public class ProductStoreDatabaseSettings: IProductStoreDatabaseSettings
    {
        public string ProductsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IProductStoreDatabaseSettings
    {
        string ProductsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}