using System.Collections.Generic;
using System;
namespace ToDoList.Models
{
  public class Item
  {
    private string _description;
    private int _id;
    private static List<Item> _instances = new List<Item> {};

    public Item (string description)
    {
      _description = description;
      _id = _instances.Count + 1;
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
      return _instances;
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
