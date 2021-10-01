using System;
using NUnit.Framework;
using Shops.Objects;
using Shops.Services;
using Shops.Tools;

namespace Shops.Tests
{
    public class Tests
    {
        private ShopManager _shopManager;

        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }

        [Test]
        public void RegisterProductToShop_AfterRegisterCanBuyProductInShop()
        {
            Shop testShop = _shopManager.AddShop("Spar", "Cumennoostrovskiy street");
            Product testProduct = _shopManager.CreateProduct("Milk");
            BelongProduct testRegProduct = _shopManager.RegisterProduct(testShop, testProduct, 1, 59.5);
            if (!testShop.FindProductInShop(testProduct).Equals(testRegProduct))
                Assert.Fail();
            var testCustomer = new Person(1000);
            testCustomer.AddInShoppingList(testProduct, 1);
            _shopManager.AlgoMakePurchase(testShop, testCustomer);
            if (testShop.FindProductInShop(testProduct) != null)
                Assert.Fail();
        }

        [Test]
        public void SettingAndChangingPrices_CheckChanges()
        {
            Shop testShop = _shopManager.AddShop("Magnit", "Podumenskoe avenue");
            Product testProduct = _shopManager.CreateProduct("Milk");
            BelongProduct testRegProduct = _shopManager.RegisterProduct(testShop, testProduct, 1, 59.5);
            if (testRegProduct.Count == 0)
                Assert.Fail();
            _shopManager.ChangeCountOfProduct(testRegProduct, 69.99);
            if (Math.Abs(testRegProduct.Count - 69.99) > 0.00001)
                Assert.Fail();
        }
        
        [Test]
        public void SearchShopWithGoodsPossibleAndProductNotEnough_ThrowException()
        {
            Assert.Catch<DealException>(() =>
            {
                Shop testFirstShop = _shopManager.AddShop("Magnit", "Podumenskoe avenue");
                Shop testSecondShop = _shopManager.AddShop("Pyatiorochka", "Nowiy square");
                Product testProduct = _shopManager.CreateProduct("Salo");
                BelongProduct testRegProduct1 = _shopManager.RegisterProduct(testFirstShop, testProduct, 10, 59.5);
                BelongProduct testRegProduct2 = _shopManager.RegisterProduct(testSecondShop, testProduct, 10, 59.5);
                
                var testCustomer = new Person(100000);
                testCustomer.AddInShoppingList(testProduct, 11);
                _shopManager.AlgoMakePurchase(_shopManager.SearсhMinCountOfProduct(testCustomer), testCustomer);
            });
        }
        
        [Test]
        public void SearchShopWithGoodsPossibleAndProductAbsent_ThrowException()
        {
            Assert.Catch<DealException>(() =>
            {
                Shop testFirstShop = _shopManager.AddShop("Magnit", "Podumenskoe avenue");
                Shop testSecondShop = _shopManager.AddShop("Pyatiorochka", "Nowiy square");
                Product testProduct1 = _shopManager.CreateProduct("Salo");
                Product testProduct2 = _shopManager.CreateProduct("Bred");
                BelongProduct testRegProduct1 = _shopManager.RegisterProduct(testFirstShop, testProduct1, 10, 59.5);
                BelongProduct testRegProduct2 = _shopManager.RegisterProduct(testSecondShop, testProduct1, 10, 59.5);
                
                var testCustomer = new Person(100000);
                testCustomer.AddInShoppingList(testProduct2, 1);
                _shopManager.AlgoMakePurchase(_shopManager.SearсhMinCountOfProduct(testCustomer), testCustomer);
            });
        }
        [Test]
        public void BuyChangeQuantityAndBalansPerson_NumbersChanged()
        {
            Shop shop = _shopManager.AddShop("Magnit", "Podumenskoe avenue");
            Product testProduct = _shopManager.CreateProduct("Salo");
            BelongProduct testRegProduct = _shopManager.RegisterProduct(shop, testProduct, 2, 59.5);
                            
            var testCustomer = new Person(60);
            testCustomer.AddInShoppingList(testProduct, 1);
            _shopManager.AlgoMakePurchase(shop, testCustomer);
            
            if (Math.Abs(testCustomer.HowMuchMoney() - (60 - 59.5)) > 0.0001 || testRegProduct.Quantity != 1)
                Assert.Fail();
        }
        
        [Test]
        public void PersonHaveEnoughMoney_ThrowException()
        {
            Assert.Catch<DealException>(() =>
            {
                Shop shop = _shopManager.AddShop("Magnit", "Podumenskoe avenue");
                Product testProduct = _shopManager.CreateProduct("Salo");
                BelongProduct testRegProduct = _shopManager.RegisterProduct(shop, testProduct, 2, 59.5);
                
                var testCustomer = new Person(10);
                testCustomer.AddInShoppingList(testProduct, 1);
                _shopManager.AlgoMakePurchase(shop, testCustomer);
            });
        }
        
        [Test]
        public void ThereAreEnoughProduct_ThrowException()
        {
            Assert.Catch<DealException>(() =>
            {
                Shop shop = _shopManager.AddShop("Magnit", "Podumenskoe avenue");
                Product testProduct = _shopManager.CreateProduct("Salo");
                BelongProduct testRegProduct = _shopManager.RegisterProduct(shop, testProduct, 2, 59.5);
                
                var testCustomer = new Person(200);
                testCustomer.AddInShoppingList(testProduct, 3);
                _shopManager.AlgoMakePurchase(shop, testCustomer);
            });
        }
    }
}