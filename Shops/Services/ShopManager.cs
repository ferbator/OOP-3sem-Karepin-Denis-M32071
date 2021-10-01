using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Objects;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager
    {
        private readonly List<Shop> _shopAccount = new List<Shop>();

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

        public Product CreateProduct(string nameProduct)
        {
            var product = new Product(nameProduct);
            return product;
        }

        public BelongProduct RegisterProduct(Shop shop, Product product, int quantityProduct, double countProduct)
        {
            var regProduct = new BelongProduct(shop, product, quantityProduct, countProduct);
            shop.AddRegProduct(regProduct);
            return regProduct;
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

        public Shop SearсhMinCountOfProduct(Person customer)
        {
            double minOfCountProduct = double.MaxValue;
            Shop bufShop = null;
            foreach (Shop shops in _shopAccount)
            {
                double tmp = shops.SearchRelevantShop(customer.ShoppingList);
                if (tmp < minOfCountProduct)
                {
                    minOfCountProduct = tmp;
                    bufShop = shops;
                }
            }

            if (bufShop == null)
                throw new DealException("There are no such products in one store");

            return bufShop;
        }

        public void HelpOutput(Shop shop)
        {
            shop.HelpOutputForShop();
        }
    }
}