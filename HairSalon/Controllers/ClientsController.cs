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
    [HttpGet("/client/{ID}")]
    public ActionResult Detail(int ID)
    {
      return View(Client.FindById(ID));
    }
    [HttpGet("/clients/{clientId}/Edit")]
    public ActionResult EditForm(int clientId)
    {
      return View(Client.FindById(clientId));
    }
    [HttpPost("/clients/{clientId}/EditName")]
    public ActionResult EditName(string clientName, int clientId)
    {
      Client.FindById(clientId).EditName(clientName);
      return RedirectToAction("Index");
    }
    [HttpPost("/clients/{clientId}/EditStylist")]
    public ActionResult EditStylist(int stylistId, int clientId)
    {
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
