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
    [HttpPost("/specialties")]
    public ActionResult Create(string addSpecialty)
    {
      Specialties createSpecialty = new Specialties(addSpecialty);
      createSpecialty.Save();
      return RedirectToAction("Index");
    }
    [HttpGet("/specialties/{ID}")]
    public ActionResult Detail(int ID)
    {
      return View(Specialties.FindById(ID).GetStylists());
    }
    [HttpGet("/specialties/Create")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpPost("/specialties/CreatePairs")]
    public ActionResult CreatePairs(int stylistId, int specialtyId)
    {
      StylistSpecialties newPair = new StylistSpecialties(stylistId, specialtyId);
      newPair.Save();
      return RedirectToAction("Index");
    }
  }
}
