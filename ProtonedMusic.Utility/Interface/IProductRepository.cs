using ProtonedMusic.Utility.Models;

namespace ProtonedMusic.Utility.Interface
{
    // Grænseflade til repositorylagets produktrelaterede operationer
    public interface IProductRepository
    {
        // Metode til at hente alle produkter
        public Task<List<ProductModel>> GetAllProduct();

        // Metode til at hente et produkt efter ID
        public Task<ProductModel> GetProductById(int id);

        // Metode til at slette et produkt efter ID
        public Task<ProductModel> DeleteProductById(int id);

        // Metode til at oprette et nyt produkt
        public Task<ProductModel> CreateProduct(ProductModel product);

        public Task<ProductModel> UpdateProduct(ProductModel UpdateProduct);

    }
}
