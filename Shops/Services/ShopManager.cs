using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Objects;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager : IShopManager
    {
        private readonly List<Shop> _shopAccount = new List<Shop>();
        private readonly Dictionary<Product, string> _productInBase = new Dictionary<Product, string>();

        public ShopManager()
        {
            _shopAccount = new List<Shop>();
        }

        public ShopManager(Shop shop)
        {
            _shopAccount.Add(shop);
        }

        public Shop AddShop(string name, string address)
        {
            var shop = new Shop(name, address);
            _shopAccount.Add(shop);

            return shop;
        }

        public Product CreateProductAndRegInBase(string nameProduct)
        {
            string idOfProduct = Guid.NewGuid().ToString();
            var product = new Product(nameProduct, idOfProduct);
            _productInBase.Add(product, idOfProduct);
            return product;
        }

        public BelongProduct RegisterProduct(Shop shop, Product product, int quantityProduct, double countProduct)
        {
            if (_productInBase.ContainsKey(product))
            {
                var regProduct = new BelongProduct(shop, product, quantityProduct, countProduct);
                BelongProduct tmpBelongProduct = shop.FindProductInShop(product);
                if (tmpBelongProduct == null)
                    shop.AddRegProduct(regProduct);
                else
                    return tmpBelongProduct;
                return regProduct;
            }
            else
            {
                throw new DealException("There aren't product in base");
            }
        }

        public void ChangeQuantityOfProduct(BelongProduct product, int delQuantity)
        {
            product.ChangeQuantity(delQuantity);
        }

        public void ChangeCountOfProduct(BelongProduct product, double newCount)
        {
            product.ChangeCount(newCount);
        }

        public void AlgoMakePurchase(Shop shop, Person customer)
        {
            if (shop.AbilityToBuy(customer))
            {
                foreach ((Product products, int count) in customer.ShoppingList)
                {
                    BelongProduct regProduct = shop.FindProductInShop(products);
                    if (regProduct != null)
                    {
                        ChangeQuantityOfProduct(regProduct, count);
                        if (regProduct.Quantity == 0)
                            shop.DelRegProduct(regProduct);
                        shop.UpBudget(regProduct, count);
                        customer.MinusMoneyOfPerson(regProduct, count);
                    }
                    else
                    {
                        throw new DealException("The product is out of stock");
                    }
                }
            }
            else
            {
                throw new DealException("You can't buy it");
            }
        }

        public Shop SearсhShopWithMinCountOfProduct(Person customer)
        {
            return _shopAccount.Select(x => new
                {
                    Shop = x,
                    MinCount = x.SearchMinCollectionProduct(customer.ShoppingList),
                })
                .OrderBy(x => x.MinCount)
                .Take(1)
                .Select(x => x.Shop)
                .FirstOrDefault();
        }
    }
}