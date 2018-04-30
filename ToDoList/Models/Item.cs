using System.Collections.Generic;
using System;
namespace ToDoList.Models
{
  public class Item
  {
    private string _description;
    private int _id;
    private static List<Item> _instances = new List<Item> {};

    public Item (string Description, int Id)
    {
      _description = Description;
      _id = Id;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetDescription()
    {
      return _description;
    }
    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }
    public static List<Item> GetAll()
       {
           List<Item> allItems = new List<Item> {};
           MySqlConnection conn = DB.Connection();
           conn.Open();
           MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"SELECT * FROM items;";
           MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
           while(rdr.Read())
           {
             int itemId = rdr.GetInt32(0);
             string itemDescription = rdr.GetString(1);
             Item newItem = new Item(itemDescription, itemId);
             allItems.Add(newItem);
           }
           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
           return allItems;
       }
    public void Save()
    {
      _instances.Add(this);
    }
    public static void ClearAll()
    {
      _instances.Clear();
    }
    public static Item Find(int searchId)
    {
      return _instances[searchId-1];
    }
  }
}
