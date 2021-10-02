using Shops.Objects;

namespace Shops.Services
{
    public interface IShopManager
    {
        public Shop AddShop(string name, string address);
        public Product CreateProduct(string nameProduct);
        public BelongProduct RegisterProduct(Shop shop, Product product, int quantityProduct, double countProduct);
        public void ChangeQuantityOfProduct(BelongProduct product, int delQuantity);
        public void ChangeCountOfProduct(BelongProduct product, double newCount);
        public void AlgoMakePurchase(Shop shop, Person customer);
        public Shop SearсhMinCountOfProduct(Person customer);
    }
}