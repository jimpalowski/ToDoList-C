using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ToDoList.Models;
using System;

namespace ToDoList.Models
{
  public class Category
  {
    private string _name;
    private int _id;
    private List<Item> _items;

    public Category(string categoryName)
    {
        _name = categoryName;
    }

    public List<Item> GetItems()
    {
        return _items;
    }
    public void AddItem(Item item)
    {
        _items.Add(item);
    }

    public string GetName()
    {
        return _name;
    }
    public int GetId()
    {
        return _id;
    }

    public void SetId(int newId)
    {
        _id = newId;
    }

    public void SetName(string newName)
    {
        _name = newName;
    }

    public void SetList(List<Item> items)
    {
        _items = items;
    }

    public void Save()
    {

    }

    public static List<Category> GetAll()
    {
        List<Category> allCategories = new List<Category> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM categories;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int categoryId = rdr.GetInt32(0);
          string categoryName = rdr.GetString(1);
          Category newCategory = new Category(categoryName);
          newCategory.SetId(categoryId);
          allCategories.Add(newCategory);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allCategories;
    }

    public static Category Find(int searchId)
    {
        return null;
    }
  }
}
