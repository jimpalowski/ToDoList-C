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
            Category.DeleteAll();
        }
        public void ItemTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=todo_test;";
        }

//         [TestMethod]
//         public void GetAll_DbStartsEmpty_0()
//         {
//             //Arrange
//             //Act
//             int result = Category.GetAll().Count;
//
//             //Assert
//             Assert.AreEqual(0, result);
//         }
//
          [TestMethod]
          public void Test_AddItem_AddsItemToCategory()
          {
            //Arrange
            Category testCategory = new Category("Household chores");
            testCategory.Save();

            Item testItem = new Item("Mow the lawn");
            testItem.Save();

            Item testItem2 = new Item("Water the garden");
            testItem2.Save();

            //Act
            testCategory.AddItem(testItem);
            testCategory.AddItem(testItem2);

            List<Item> result = testCategory.GetItems();
            List<Item> testList = new List<Item>{testItem, testItem2};

            //Assert
            CollectionAssert.AreEqual(testList, result);
          }
      [TestMethod]
          public void GetItems_ReturnsAllCategoryItems_ItemList()
          {
            //Arrange
            Category testCategory = new Category("Household chores");
            testCategory.Save();

            Item testItem1 = new Item("Mow the lawn");
            testItem1.Save();

            Item testItem2 = new Item("Buy plane ticket");
            testItem2.Save();

            //Act
            testCategory.AddItem(testItem1);
            List<Item> savedItems = testCategory.GetItems();
            List<Item> testList = new List<Item> {testItem1};

            //Assert
            CollectionAssert.AreEqual(testList, savedItems);
          }
          
        [TestMethod]
        public void Delete_DeletesCategoryAssociationsFromDatabase_CategoryList()
        {
          //Arrange
          Item testItem = new Item("Mow the lawn");
          testItem.Save();

          string testName = "Home stuff";
          Category testCategory = new Category(testName);
          testCategory.Save();

          //Act
          testCategory.AddItem(testItem);
          testCategory.Delete();

          List<Category> resultItemCategories = testItem.GetCategories();
          List<Category> testItemCategories = new List<Category> {};

          //Assert
          CollectionAssert.AreEqual(testItemCategories, resultItemCategories);
        }

    }
}
