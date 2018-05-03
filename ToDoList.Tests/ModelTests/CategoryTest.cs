using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using ToDoList.Models;
using System;

namespace ToDoList.Tests
{

    [TestClass]
    public class CategoryTests : IDisposable
    {
        public void Dispose()
        {
            Item.DeleteAll();
        }
        public void ItemTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=todo_test;";
        }


        [TestMethod]
        public void GetAll_DbStartsEmpty_0()
        {
            //Arrange
            //Act
            int result = Category.GetAll().Count;

            //Assert
            Assert.AreEqual(0, result);
        }
    }
}
