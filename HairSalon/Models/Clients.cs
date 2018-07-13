using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
  public class Client
  {
    private string client;
    private string stylist;

    public Client(string newClient, string newStylist = "")
    {
      client = newClient;
      stylist = newStylist;
    }
    public string GetClient()
    {
      return client;
    }
    public string GetStylist()
    {
      return stylist;
    }

    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool clientEqual = (this.GetClient() == newClient.GetClient());
        bool stylistEqual = (this.GetStylist() == newClient.GetStylist());
        return (clientEqual && stylistEqual);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO client (client, stylist) VALUES (@inputClient, @inputStylist);";
      MySqlParameter newName = new MySqlParameter();
      newName.ParameterName = "@inputClient";
      newName.Value = this.client;
      cmd.Parameters.Add(newName);
      MySqlParameter newStylist = new MySqlParameter();
      newStylist.ParameterName = "@inputStylist";
      newStylist.Value = this.stylist;
      cmd.Parameters.Add(newStylist);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM client;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string client = rdr.GetString(0);
        string stylist = rdr.GetString(1);
        Client newClient = new Client(client, stylist);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylist;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
