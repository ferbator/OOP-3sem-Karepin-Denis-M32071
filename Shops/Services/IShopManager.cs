using Shops.Objects;

namespace Shops.Services
{
    public interface IShopManager
    {
        Shop AddShop(string name, string address);
        Product CreateProductAndRegInBase(string nameProduct);
        BelongProduct RegisterProduct(Shop shop, Product product, int quantityProduct, double countProduct);
        void ChangeQuantityOfProduct(BelongProduct product, int delQuantity);
        void ChangeCountOfProduct(BelongProduct product, double newCount);
        void AlgoMakePurchase(Shop shop, Person customer);
        Shop SearсhShopWithMinCountOfProduct(Person customer);
    }
}