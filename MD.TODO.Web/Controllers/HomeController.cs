using MD.TODO.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MD.TODO.Web.Controllers
{
    public class HomeController : Controller
    {
        private ITodoItemService _todoItemService;

        public HomeController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;

        }

        public ActionResult Index()
        {
            var list=_todoItemService.GetTodoItems().ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}