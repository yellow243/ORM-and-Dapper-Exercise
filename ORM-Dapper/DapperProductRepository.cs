﻿using Dapper;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO products (Name, Price, CategoryID) VALUES (@name, @price, @categoryID);",
                new {name = name, price = price, categoryID = categoryID});
        }

        public IEnumerable<Product> GetAllProducts()
        {
            //dapper starts here
            //dapper extends ==> IDbConnection
            return _connection.Query<Product>("SELECT * FROM PRODUCTS;");
        }
        //Update data
        //Bonus
        public void UpdateProductName(int productID, string updatedName)
        {
            _connection.Execute("UPDATE products SET Name = @updatedName WHERE ProductID = @productID;",
                new { updatedName = updatedName, productID = productID });
        }

        //Delete data
        //Bonus
        public void DeleteProduct(int productID)
        {
            _connection.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
                new { productID = productID });

            _connection.Execute("DELETE FROM sales WHERE ProductID = @productID;",
               new { productID = productID });

            _connection.Execute("DELETE FROM products WHERE ProductID = @productID;",
               new { productID = productID });
        }

    }
}
