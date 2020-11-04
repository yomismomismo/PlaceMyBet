using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Web;
using MySql.Data.MySqlClient;

    namespace PlaceMyBet.Models
    {
        public class EventoRepository
        {

            private MySqlConnection Connect()
            {

                string connString = "Server=127.0.0.1;Port=3306;Database=placemybet;Uid=root;Password=;SslMode=none";
                MySqlConnection con = new MySqlConnection(connString);
                return con;

            }

        internal List<Evento> RetrieveList()
        {

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT * FROM Evento";

            con.Open();
            MySqlDataReader res = command.ExecuteReader();

            Evento e = null;
            List<Evento> evento = new List<Evento>();

            while (res.Read())
            {
                Debug.WriteLine("Recuperado: " + res.GetString(1) + res.GetString(2) + res.GetString(3));
                e = new Evento(res.GetInt16(0), res.GetString(1), res.GetString(2), res.GetString(3), res.GetInt16(4));
                evento.Add(e);
            }


            return evento;

        }


        internal EventoDTO Retrieve(int Id)
            {

                MySqlConnection con = Connect();
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "SELECT * FROM Evento WHERE ID=@id";
            command.Parameters.AddWithValue("@id", Id);

                con.Open();
                MySqlDataReader res = command.ExecuteReader();

                EventoDTO e = null;

                if (res.Read())
                {
                    Debug.WriteLine("Recuperado: " + res.GetString(1) + res.GetString(2) + res.GetString(3));
                    e = new EventoDTO(res.GetString(1), res.GetString(2), res.GetString(3));
                }


                return e;

            }
        }
    }
