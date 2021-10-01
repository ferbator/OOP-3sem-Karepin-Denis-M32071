using Shops.Tools;

namespace Shops.Objects
{
    public class BelongProduct
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

        public override bool Equals(object obj)
        {
            if (!(obj is BelongProduct product)) return false;
            return Equals(product);
        }

        public override int GetHashCode()
                {
                    return RegGood != null ? RegGood.GetHashCode() : 0;
                }

        private bool Equals(BelongProduct other)
                {
                    return RegGood == other.RegGood;
                }
    }
}