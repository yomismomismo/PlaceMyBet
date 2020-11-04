using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace PlaceMyBet.Models
{
    public class MercadoRepository
    {

        private MySqlConnection Connect()
        {

            string connString = "Server=127.0.0.1;Port=3306;Database=placemybet;Uid=root;Password=;SslMode=none";
            MySqlConnection con = new MySqlConnection(connString);
            return con;

        }

        internal List<Mercado> RetrieveList()
        {

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT * FROM Mercado";

            con.Open();
            MySqlDataReader res = command.ExecuteReader();

            Mercado m = null;
            List<Mercado> mercado = new List<Mercado>();
            while (res.Read())
            {

                Debug.WriteLine("Recuparado: " + res.GetInt16(0) + res.GetDouble(1) + res.GetDouble(2) + res.GetDouble(3) + res.GetDouble(4) + res.GetDouble(5) + res.GetInt16(6));
                m = new Mercado(res.GetInt16(0), res.GetDouble(1), res.GetDouble(2), res.GetDouble(3), res.GetDouble(4), res.GetDouble(5), res.GetInt16(6));
                mercado.Add(m);
            }

            return mercado;

        }

        internal Mercado Retrieve(int Id)
        {

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT * FROM Mercado WHERE ID=@id";
            command.Parameters.AddWithValue("@id", Id);

            con.Open();
            MySqlDataReader res = command.ExecuteReader();

            Mercado m = null;

            if (res.Read())
            {

                Debug.WriteLine("Recuparado: " + res.GetInt16(0) + res.GetDouble(1) + res.GetDouble(2) + res.GetDouble(3) + res.GetDouble(4) + res.GetDouble(5) + res.GetInt16(6));
                m = new Mercado(res.GetInt16(0), res.GetDouble(1), res.GetDouble(2), res.GetDouble(3), res.GetDouble(4), res.GetDouble(5), res.GetInt16(6));
            }

            return m;

        }



    }
}