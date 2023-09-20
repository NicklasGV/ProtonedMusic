using ProtonedMusic.Utility.Models;

namespace ProtonedMusic.Utility.Interface
{
    // Grænseflade til tjenestelagets produktrelaterede operationer
    public interface IProductService
    {
        // Metode til at hente alle produkter
        public Task<List<ProductModel>> GetAllProduct();

        // Metode til at hente et produkt efter ID
        public Task<ProductModel> GetProductById(int id);

        // Metode til at slette et produkt efter ID
        public Task<ProductModel> DeleteProductById(int id);

        // Metode til at oprette et nyt produkt
        public Task<ProductModel> CreateProduct(ProductModel product);
    }
}
