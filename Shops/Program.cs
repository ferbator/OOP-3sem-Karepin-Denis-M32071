using System;
using Shops.Objects;
using Shops.Services;

namespace Shops
{
    internal class Program
    {
        private static void Main()
        {
            var shopSystem = new ShopManager();

            Shop shopFirst = shopSystem.AddShop("Spar", "CЪЪ");
        }
    }
}