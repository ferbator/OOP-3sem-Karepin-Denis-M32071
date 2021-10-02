using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Objects
{
    public class Shop
    {
        private readonly List<BelongProduct> _shop = new List<BelongProduct>();
        public Shop(string name, string address)
             {
                 Name = name;
                 Budget = 0;
                 Address = address;
             }

        public string Name { get; }
        public string Address { get; }
        public double Budget { get; private set; }

        public bool AbilityToBuy(Person customer)
        {
            double priceTag = 0.0;
            int counter = 0;

            foreach ((Product needProduct, int count) in customer.ShoppingList)
            {
                foreach (BelongProduct product in _shop)
                {
                    if (product.RegGood.Equals(needProduct))
                    {
                        counter++;
                        priceTag += product.Count;
                    }
                }
            }

            if (counter == customer.ShoppingList.Count && customer.HowMuchMoney() >= priceTag)
                return true;
            else
                return false;
        }

        public BelongProduct FindProductInShop(Product product)
        {
            foreach (BelongProduct regProduct in _shop)
            {
                if (product.Equals(regProduct.RegGood))
                {
                    return regProduct;
                }
            }

            return null;
        }

        public void AddRegProduct(BelongProduct regProduct)
        {
            _shop.Add(regProduct);
        }

        public void DelRegProduct(BelongProduct regProduct)
        {
            _shop.Remove(regProduct);
        }

        public void UpBudget(BelongProduct product, int quantity)
        {
            Budget += product.Count * quantity;
        }

        public double SearchMinCollectionProduct(Dictionary<Product, int> shoppingList)
        {
            if (shoppingList.Count != 0)
            {
                double priceTag = 0.0;
                int counter = 0;

                foreach ((Product needProduct, int count) in shoppingList)
                {
                    foreach (BelongProduct product in _shop.Where(product =>
                        product.RegGood.Equals(needProduct) && product.Quantity >= count))
                    {
                        counter++;
                        priceTag += product.Count;
                    }
                }

                if (counter == shoppingList.Count)
                    return priceTag;
                else
                    return double.MaxValue;
            }
            else
            {
                throw new DealException("Shopping list is empty");
            }
        }
    }
}