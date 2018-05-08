using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ToDoList.Models;
using System;

namespace ToDoList.Controllers
{
    public class ItemsController : Controller
    {
       [HttpGet("/items")]
       public ActionResult Index()
       {
           List<Item> allItems = Item.GetAll();
           return View(allItems);
       }

       [HttpGet("/items/new")]
        public ActionResult CreateForm()
        {
            return View();
        }
        [HttpPost("/items")]
        public ActionResult Create()
        {
            Item newItem = new Item(Request.Form["item-description"]);
            newItem.Save();
            return RedirectToAction("Success", "Home");
        }

      //  [HttpGet("/categories/{categoryId}/items/new")]
      //  public ActionResult CreateForm(int categoryId)
      //  {
      //     Dictionary<string, object> model = new Dictionary<string, object>();
      //     Category category = Category.Find(categoryId);
      //     return View(category);
      //  }

       [HttpGet("/items/{id}")]
        public ActionResult Details(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Item selectedItem = Item.Find(id);
            List<Category> itemCategories = selectedItem.GetCategories();
            List<Category> allCategories = Category.GetAll();
            model.Add("selectedItem", selectedItem);
            model.Add("itemCategories", itemCategories);
            model.Add("allCategories", allCategories);
            return View(model);

        }

      //  [HttpGet("/categories/{categoryId}/items/{itemId}")]
      //  public ActionResult Details(int categoryId, int itemId)
      //  {
      //     Item item = Item.Find(itemId);
      //     Dictionary<string, object> model = new Dictionary<string, object>();
      //     Category category = Category.Find(categoryId);
      //     model.Add("item", item);
      //     model.Add("category", category);
      //     return View(item);
      //  }

      [HttpPost("/items/{itemId}/categories/new")]
        public ActionResult AddCategory(int itemId)
        {
            Item item = Item.Find(itemId);
            Category category = Category.Find(Int32.Parse(Request.Form["category-id"]));
            item.AddCategory(category);
            return RedirectToAction("Details",  new { id = itemId });
        }
      //  [HttpGet("/items/{id}/update")]
      //   public ActionResult UpdateForm(int id)
      //   {
      //       Item thisItem = Item.Find(id);
      //       return View(thisItem);
      //   }
       //
      //   [HttpGet("/items/{id}/delete")]
      //    public ActionResult DeleteItem(int id)
      //    {
      //        //Item.Delete();
      //        return RedirectToAction("Details", "Categories", new { id = id });
      //    }
    }
}
