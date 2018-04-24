using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/home")]
      public ActionResult Index()
      {
          List<Category> allCategories = Category.GetAll();
          return View(allCategories);
      }
    }
}
