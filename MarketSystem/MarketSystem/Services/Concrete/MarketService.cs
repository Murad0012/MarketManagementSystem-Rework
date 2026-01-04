using System;
using System.Collections;
using System.Diagnostics;
using MarketSystem.Data.Enums;
using MarketSystem.Data.Models;
using MarketSystem.Services.Abstract;

namespace MarketSystem.Services.Concrete
{
    public class MarketService : IMarketable
    {
        private List<Product> _products = new();
        private List<Sale> _sales = new();

        // Product methods
        public List<Product> GetProducts()
        {
            return _products;
        }

        public int AddProduct(string name, decimal price, Category category, int count)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Product name cannot be empty!");

            if (price < 0)
                throw new Exception("Product price cannot be less than zero!");

            if(count < 0)
                throw new Exception("Product count cannot be less than zero!");

            var existingProduct = _products
                .FirstOrDefault(p => p.Name == name && p.Category == category && p.Price == price);

            if (existingProduct != null)
            {
                existingProduct.Count += count;
                return existingProduct.ID;
            }

            var product = new Product()
            {
                Name = name,
                Price = price,
                Count = count,
                Category = category
            };

            _products.Add(product);

            return product.ID;
        }

        public void UpdateProduct(int id, string name, decimal price, Category category, int count)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Product name cannot be empty!");

            if (price < 0)
                throw new Exception("Product price cannot be less than zero!");

            if (count < 0)
                throw new Exception("Product count cannot be less than zero!");

            var product = _products.FirstOrDefault(p => p.ID == id)
                ?? throw new Exception($"Product with ID:{id} was not found! ");

            product.Name = name;
            product.Category = category;
            product.Count = count;
            product.Price = price;
        }

        public void DeleteProduct(int id)
        {
            if (id < 0)
                throw new Exception("ID of product can't be less than zero!");

            var product = _products.FirstOrDefault(p => p.ID == id)
                ?? throw new Exception($"Product with ID {id} was not found!");

            _products.Remove(product!);
        }

        public List<Product> GetProductsByCategory(Category category)
        {
            var products = _products.Where(p => p.Category == category).ToList();

            return products;
        }

        public List<Product> GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            if (minPrice > maxPrice)
                throw new Exception("Minimum price cannot be greater than maximum price!");

            if (minPrice < 0)
                throw new Exception("Min price can't be less than zero!");

            if (maxPrice < 0)
                throw new Exception("Max price can't be less than zero.");

            var products = _products
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();

            return products;
        }

        public List<Product> GetProductsByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Product name cannot be empty.");

            var products = _products
                .Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return products;
        }


        // Sale Methods
        public List<Sale> GetSales()
        {
            return _sales;
        }

        public Sale GetSale(int id)
        {
            if (id < 0)
                throw new Exception("Sale ID must be greater than zero!");

            var sale = _sales.FirstOrDefault(s => s.ID == id)
                ?? throw new Exception($"Sale with ID {id} was not found!");

            return sale;
        }

        public void AddSale(Dictionary<int, int> products)
        {
            foreach (var item in products)
            {
                int productID = item.Key;
                int quantity = item.Value!;

                var product = _products.FirstOrDefault(p => p.ID == productID)
                    ?? throw new Exception($"Product with ID {productID} was not found.");

                if (product.Count < quantity)
                    throw new Exception(
                        $"Not enough stock for '{product.Name}'. Available: {product.Count}, Requested: {quantity}");
            }

            var sale = new Sale();

            foreach (var item in products)
            {
                int productID = item.Key;
                int quantity = item.Value!;

                var product = _products.FirstOrDefault(p => p.ID == productID);

                product!.Count -= quantity;

                if (product.Count == 0)
                    _products.Remove(product);

                var saleItem = new SaleItem()
                {
                    Product = product,
                    Quantity = quantity
                };

                sale.Amount += product.Price * quantity;

                sale.SaleItems.Add(saleItem);
            }

            _sales.Add(sale);
        }

        public List<Sale> GetSalesByAmountRange(decimal minAmount, decimal maxAmount)
        {
            if (minAmount > maxAmount)
                throw new Exception("Minimum amount cannot be greater than maximum amount.");

            if (minAmount < 0)
                throw new Exception("Minimum amount cannot be less than zero.");

            if (maxAmount < 0)
                throw new Exception("Maximum amount cannot be less than zero.");

            return _sales
                .Where(s => s.Amount >= minAmount && s.Amount <= maxAmount)
                .ToList();
        }

        public List<Sale> GetSalesByDate(DateTime date)
        {
            var sales = _sales.Where(s => s.Date == date).ToList();

            return sales;
        }

        public List<Sale> GetSalesByDateRange(DateTime minDate, DateTime maxDate)
        {
            if (minDate > maxDate)
                throw new Exception("Min date can't be larger than max date!");

            var sales = _sales.Where(x => x.Date >= minDate && x.Date <= maxDate).ToList() ??
                throw new Exception($"Sale was not found!");

            return sales;
        }

        public void DeleteSale(int id)
        {
            if (id < 0)
                throw new Exception("ID of sale can't be less than zero!");

            var sale = _sales.FirstOrDefault(p => p.ID == id)
                ?? throw new Exception($"Sale with ID {id} was not found.");

            foreach (var saleItem in sale.SaleItems)
            {
                saleItem.Product.Count += saleItem.Quantity;
            }

            _sales.Remove(sale);
        }

        public void ReturnProductFromSale(int saleID, Dictionary<int, int> products)
        {
            var sale = _sales.FirstOrDefault(s => s.ID == saleID)
                ?? throw new Exception($"Sale with ID {saleID} was not found.");

            var saleItemMap = sale.SaleItems.ToDictionary(si => si.Product.ID);

            foreach (var item in products)
            {
                int productID = item.Key;
                int count = item.Value;

                if (count <= 0)
                    throw new Exception("Returned product count must be greater than zero.");

                if (!saleItemMap.TryGetValue(productID, out var saleItem))
                    throw new Exception($"Sale does not contain product with ID {productID}.");

                if (count > saleItem.Quantity)
                    throw new Exception(
                        $"You cannot return more than sold. Sold: {saleItem.Quantity}, Requested: {count}");
            }

            foreach (var item in products)
            {
                int productID = item.Key;
                int count = item.Value;

                var saleItem = saleItemMap[productID];

                var product = _products.FirstOrDefault(p => p.ID == productID);
                if (product == null)
                {
                    product = saleItem.Product;
                    _products.Add(product);
                }

                sale.Amount -= count * saleItem.Product.Price;
                saleItem.Quantity -= count;
                product.Count += count;

                if (saleItem.Quantity == 0)
                    sale.SaleItems.Remove(saleItem);
            }

            if (sale.SaleItems.Count == 0)
                _sales.Remove(sale);
        }

    }
}