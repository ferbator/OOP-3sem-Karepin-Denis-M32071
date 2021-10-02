using System;
using Shops.Tools;

namespace Shops.Objects
{
    public class BelongProduct : IEquatable<BelongProduct>
    {
        public BelongProduct(Shop shop, Product good, int quantity, double count)
        {
            RegShop = shop;
            RegGood = good;
            Quantity = quantity;
            Count = count;
        }

        public Product RegGood { get; }
        public Shop RegShop { get; }
        public int Quantity { get; private set; }
        public double Count { get; private set; }

        public void ChangeQuantity(int newQuantity)
        {
            if (newQuantity > 0 && newQuantity <= this.Quantity)
            {
                Quantity -= newQuantity;
            }
            else
            {
                throw new DealException($"So many products are not in the store. Product in stock: {Quantity}");
            }
        }

        public void ChangeCount(double newCount)
        {
            if (newCount > 0)
            {
               Count = newCount;
            }
            else
            {
                throw new DealException("Incorrect price");
            }
        }

        public bool Equals(BelongProduct other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(RegGood, other.RegGood);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BelongProduct)obj);
        }

        public override int GetHashCode()
        {
            return RegGood != null ? RegGood.GetHashCode() : 0;
        }
    }
}