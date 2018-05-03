using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ToDoList.Models;
using System;

namespace ToDoList.Models
{
  public class Item
  {
    private string _description;
    private int _id;
    private string _dueDate;
    private int _categoryId;


    public Item (string description, int categoryId, int id = 0)
    {
        _description = description;
        _categoryId = categoryId;
        _id = id;
    }

    public override bool Equals(System.Object otherItem)
    {
        if (!(otherItem is Item))
        {
            return false;
        }
        else
        {
            Item newItem = (Item) otherItem;
            bool idEquality = this.GetId() == newItem.GetId();
            bool descriptionEquality = this.GetDescription() == newItem.GetDescription();
            bool dueDateEquality = this.GetDate() == newItem.GetDate();
            bool categoryEquality = this.GetCategoryId() == newItem.GetCategoryId();
            return (idEquality && descriptionEquality && categoryEquality && dueDateEquality);
        }
    }

    public override int GetHashCode()
    {
         return this.GetDescription().GetHashCode();
    }

    public void Save()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO `items` (`description`, `due_date`, category_id) VALUES (@ItemDescription, @ItemDueDate, @category_id);";

        MySqlParameter description = new MySqlParameter();
        description.ParameterName = "@ItemDescription";
        description.Value = this._description;
        cmd.Parameters.Add(description);

        MySqlParameter due_date = new MySqlParameter();
        due_date.ParameterName = "@ItemDueDate";
        due_date.Value = this._dueDate;
        cmd.Parameters.Add(due_date);

        MySqlParameter categoryId = new MySqlParameter();
        categoryId.ParameterName = "@category_id";
        categoryId.Value = this._categoryId;
        cmd.Parameters.Add(categoryId);

        cmd.ExecuteNonQuery();
        _id = (int) cmd.LastInsertedId;

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }

    public int GetId()
    {
        return _id;
    }

    public string GetDescription()
    {
        return _description;
    }

    public string GetDate()
    {
        return _dueDate;
    }

    public int GetCategoryId()
    {
        return _categoryId;
    }

    public void SetDescription(string newDescription)
    {
        _description = newDescription;
    }

    public void Edit(string newDescription)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE items SET description = @newDescription WHERE id = @searchId;";

        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = _id;
        cmd.Parameters.Add(searchId);

        MySqlParameter description = new MySqlParameter();
        description.ParameterName = "@newDescription";
        description.Value = newDescription;
        cmd.Parameters.Add(description);

        cmd.ExecuteNonQuery();
        _description = newDescription;

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }

    public void SetId(int newId)
    {
        _id = newId;
    }

    public void SetDate(string newDate)
    {
        _dueDate = newDate;
    }

    public static List<Item> GetAll()
    {
        List<Item> allItems = new List<Item> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM items ORDER BY due_date ASC;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int itemId = rdr.GetInt32(0);
          string itemDescription = rdr.GetString(1);
          string itemDueDate = rdr.GetString(2);
          int itemCategoryId = rdr.GetInt32(3);
          Item newItem = new Item(itemDescription, itemCategoryId, itemId);
          newItem.SetDate(itemDueDate);
          allItems.Add(newItem);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allItems;
    }

    public static void DeleteItem(int id)
    {
      Console.WriteLine(id);
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM items WHERE id = @thisId;";

        MySqlParameter thisId = new MySqlParameter();
        thisId.ParameterName = "@thisId";
        thisId.Value = id;
        cmd.Parameters.Add(thisId);

        cmd.ExecuteNonQuery();

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }

    public static void DeleteAll()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM items;";

        cmd.ExecuteNonQuery();

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }


    public static Item Find(int id)
    {
        MySqlConnection conn = DB.Connection();
          conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM `items` WHERE id = @thisId;";

        MySqlParameter thisId = new MySqlParameter();
        thisId.ParameterName = "@thisId";
        thisId.Value = id;
        cmd.Parameters.Add(thisId);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;

        int itemId = 0;
        string itemDescription = "";
        string itemDueDate = "";
        int itemCategoryId = 0;

        while (rdr.Read())
        {
            itemId = rdr.GetInt32(0);
            itemDescription = rdr.GetString(1);
            itemDueDate = rdr.GetString(2);
            itemCategoryId = rdr.GetInt32(3);
        }

        Item foundItem = new Item(itemDescription, itemCategoryId, itemId);
        foundItem.SetDate(itemDueDate);

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }

        return foundItem;
    }
  }
}
