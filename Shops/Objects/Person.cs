using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Objects
{
    public class Person
    {
        private double _walletCount;
        public Person(double walletCount)
        {
            _walletCount = walletCount;
            ShoppingList = new Dictionary<Product, int>();
        }

        public Dictionary<Product, int> ShoppingList { get; }

        public double HowMuchMoney()
        {
            return this._walletCount;
        }

        public void AddInShoppingList(Product product, int countOfProduct)
        {
            ShoppingList.Add(product, countOfProduct);
        }

        public void MinusMoneyOfPerson(BelongProduct product, int quantity)
        {
            if (this._walletCount - (product.Count * quantity) < 0)
            {
                throw new DealException("You can't afford this product");
            }
            else
            {
                this._walletCount -= product.Count * quantity;
            }
        }
    }
}