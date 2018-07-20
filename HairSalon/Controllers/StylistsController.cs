using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
    [HttpGet("/stylists")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
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
      Stylist createStylist = new Stylist(stylistName, description);
      createStylist.Save();
      return RedirectToAction("Index");
    }
    [HttpGet("/stylists/{ID}")]
    public ActionResult Detail(int ID)
    {
      return View(Stylist.FindById(ID));
    }
    [HttpPost("/stylists/{stylistID}/AddClient")]
    public ActionResult AddClient(string clientName, int stylistID)
    {
      Client newPair = new Client(clientName, stylistID);
      newPair.Save();
      return RedirectToAction("Detail", new {ID = stylistID});
    }
    [HttpGet("/stylists/search")]
    public ActionResult SearchForm()
    {
      return View();
    }
  }
}
