using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    [HttpGet("/clients")]
    public ActionResult Index()
    {
      List<Client> allClients = Client.GetAll();
      return View(allClients);
    }
    [HttpGet("/clients/new")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpPost("/clients")]
    public ActionResult Create(string clientName, string stylistName)
    {
      Client createClient = new Client(clientName, stylistName);
      createClient.Save();
      return RedirectToAction("Index");
    }
  }
}
