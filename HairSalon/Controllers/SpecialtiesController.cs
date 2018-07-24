using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class SpecialtiesController : Controller
  {
    [HttpGet("/specialties")]
    public ActionResult Index()
    {
      List<Specialties> allSpecialties = Specialties.GetAll();
      return View(allSpecialties);
    }
    [HttpGet("/specialties/new")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpPost("/specialties")]
    public ActionResult Create(string addSpecialties)
    {
      string newAddSpecialties = "";
      if(!string.IsNullOrWhiteSpace(Request.Form["description"]))
      {
        newAddSpecialties = addSpecialties;
      }
      Specialties createSpecialties = new Specialties(addSpecialties);
      createSpecialties.Save();
      return RedirectToAction("Index");
    }
  }
}
