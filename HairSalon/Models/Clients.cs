using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
  public class Client
  {
    private string client;
    private string stylist_id;

    public Client(string newClient, string newStylist = "")
    {
      client = newClient;
      stylist_id = newStylist;
    }
    public string GetClient()
    {
      return client;
    }
    public string GetStylist()
    {
      return stylist_d;
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
        bool clientEquality = (this.GetClient() == newClient.GetClient());
        bool stylistEquality = (this.GetStylist() == newClient.GetStylist());
        return (clientEquality && stylistEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (client, stylist) VALUES (@inputClient, @inputStylist);";
      MySqlParameter newName = new MySqlParameter();
      newName.ParameterName = "@inputClient";
      newName.Value = this.client;
      cmd.Parameters.Add(newName);
      MySqlParameter newStylist = new MySqlParameter();
      newStylist.ParameterName = "@inputStylist";
      newStylist.Value = this.stylist_id;
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
      cmd.CommandText = @"SELECT * FROM clients;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string client = rdr.GetString(0);
        string stylist_id = rdr.GetString(1);
        Client newClient = new Client(client, stylist_id);
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
      cmd.CommandText = @"DELETE FROM clients;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
