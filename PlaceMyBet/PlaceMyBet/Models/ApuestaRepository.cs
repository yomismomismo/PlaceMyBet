using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace PlaceMyBet.Models
{
    public class ApuestaRepository
    {
        private MySqlConnection Connect()
        {

            string connString = "Server=127.0.0.1;Port=3306;Database=placemybet;Uid=root;Password=;SslMode=none";
            MySqlConnection con = new MySqlConnection(connString);
            return con;

        }

        internal List<Apuesta> RetrieveList()
        {

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT * FROM Apuesta";

            con.Open();
            MySqlDataReader res = command.ExecuteReader();

            Apuesta a = null;
            List<Apuesta> apuesta = new List<Apuesta>();

            while (res.Read())
            {

                Debug.WriteLine("Recuperado: " + res.GetInt16(0) + res.GetDouble(1) + res.GetString(2) + res.GetDouble(3) + res.GetString(4) + res.GetInt16(5) + res.GetString(6));
                a = new Apuesta(res.GetInt16(0), res.GetDouble(1), res.GetString(2), res.GetDouble(3), res.GetString(4), res.GetInt16(5), res.GetString(6));
                apuesta.Add(a);

            }
            return apuesta;
        }

        internal Apuesta Retrieve(int Id)
        {

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT * FROM Apuesta WHERE ID=@id";
            command.Parameters.AddWithValue("@id", Id);

            con.Open();
            MySqlDataReader res = command.ExecuteReader();

            Apuesta a = null;
            
            if (res.Read())
            {

                Debug.WriteLine("Recuperado: " + res.GetInt16(0) + res.GetDouble(1) + res.GetString(2) + res.GetDouble(3) + res.GetString(4) + res.GetInt16(5) + res.GetString(6));
                a = new Apuesta(res.GetInt16(0), res.GetDouble(1), res.GetString(2), res.GetDouble(3), res.GetString(4), res.GetInt16(5), res.GetString(6));
            }
            return a;
        }

    }
}