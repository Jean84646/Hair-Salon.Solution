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
    [HttpGet("/stylists/{stylistId}/Edit")]
    public ActionResult EditForm(int stylistID)
    {
      return View(Stylist.FindById(stylistID));
    }
    [HttpPost("/stylists/{stylistId}/EditName")]
    public ActionResult EditName(int stylistId, string newName)
    {
      Stylist.FindById(stylistId).EditName(newName);
      return RedirectToAction("Detail", new { id = stylistId});
    }
    [HttpPost("/stylists/delete")]
    public ActionResult Delete(int stylistId)
    {
      Stylist.FindById(stylistId).Delete();
      return RedirectToAction("Index");
    }
  }
}
