using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Models;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
    [HttpGet("/stylists")]
    public ActionResult Index()
    {
      List<Stylists> allStylists = Stylists.GetAll();
      return View(allStylists);
    }
    [HttpGet("/stylists/new")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpPost("/stylists")]
    public ActionResult Create(string stylistName, string description)
    {
      string newDescription = "";
      if(!string.IsNullOrWhiteSpace(Request.Form["description"]))
      {
        newDescription = description;
      }
      Stylists createStylist = new Stylists(stylistName, newDescription);
      createStylist.Save();
      return RedirectToAction("Index");
    }

    [HttpPost("/stylists/search")]
    public ActionResult Search(string searchFx, string searchTerm)
    {
      List<Stylists> foundStylists = new List<Stylists> {};
      if(searchFx.Equals("byStylist"))
      {
        foundStylists = Stylists.FindByStylist(searchTerm);
      }
      else
      {
        foundStylists = Stylists.FindByClient(searchTerm);
      }
      return View("Index", foundStylists);
    }
  }
}
