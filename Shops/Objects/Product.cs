using System;

namespace Shops.Objects
{
    public class Product
    {
        public Product(string name)
        {
            Name = name;
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; }
        public string Name { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is Product product)) return false;
            return Equals(product);
        }

        public override int GetHashCode()
        {
            return Name != null ? Name.GetHashCode() : 0;
        }

        private bool Equals(Product other)
        {
            return Name == other.Name;
        }
    }
}