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
    [HttpGet("/clients/{clientId}/Edit")]
    public ActionResult EditForm()
    {
      return View();
    }
    [HttpPost("/clients/{clientId}/Edit")]
    public ActionResult Edit(string clientName, int stylistId, int clientId)
    {
      Client.FindById(clientId).EditName(clientName);
      Client.FindById(clientId).EditStylistId(stylistId);
      return RedirectToAction("Index");
    }
    [HttpPost("/clients/delete")]
    public ActionResult Delete(int clientId)
    {
      Client.FindById(clientId).Delete();
      return RedirectToAction("Index");
    }
  }
}
