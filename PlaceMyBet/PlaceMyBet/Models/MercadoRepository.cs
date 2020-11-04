using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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

        internal List<MercadoDTO> RetrieveList()
        {

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT * FROM Mercado";

            con.Open();
            MySqlDataReader res = command.ExecuteReader();

            MercadoDTO m = null;
            List<MercadoDTO> mercadoDTO = new List<MercadoDTO>();
            while (res.Read())
            {
                Debug.WriteLine("Recuparado: " + res.GetDouble(1) + res.GetDouble(2) + res.GetDouble(3));
                m = new MercadoDTO(res.GetDouble(1), res.GetDouble(2), res.GetDouble(3));
                mercadoDTO.Add(m);
            }

            return mercadoDTO;

        }

        internal MercadoDTO Retrieve(int Id)
        {

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT * FROM Mercado WHERE ID = @id";
            command.Parameters.AddWithValue("@id", Id);

            con.Open();
            MySqlDataReader res = command.ExecuteReader();

            MercadoDTO m = null;

            if (res.Read())
            {
               
                Debug.WriteLine("Recuparado: " + res.GetDouble(1) + res.GetDouble(2) + res.GetDouble(3));
                m = new MercadoDTO(res.GetDouble(1), res.GetDouble(2), res.GetDouble(3));
            }

            return m;

        }



        internal void UpdateDinero(ApuestaDTO apuesta)
        {

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();

            con.Open();
            Debug.WriteLine("tipo: " + apuesta.TipoApuesta + apuesta.IDMercado);
            if (apuesta.TipoApuesta == "Over")
            {
                command.CommandText = "UPDATE Mercado SET DineroOver = DineroOver + " + apuesta.DineroApostado + "  WHERE ID = " + apuesta.IDMercado + "; ";

            }
            else if (apuesta.TipoApuesta == "Under")
            {
                command.CommandText = "UPDATE Mercado SET DineroUnder = DineroUnder + " + apuesta.DineroApostado + "  WHERE ID = " + apuesta.IDMercado + "; ";
            }
            command.ExecuteNonQuery();

        }



        internal Mercado RetrieveDinero()
        {

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT * FROM Mercado";

            con.Open();
            MySqlDataReader res = command.ExecuteReader();

            Mercado Dinero = null;

            if (res.Read())
            {

                Dinero = new Mercado(res.GetInt32(0), res.GetDouble(1), res.GetDouble(2), res.GetDouble(3), res.GetDouble(4), res.GetDouble(5), res.GetDouble(6));
            }

            return Dinero;

        }

        internal void Calculos(ApuestaDTO apuesta, Mercado Dinero)
        {
            
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            double probabilidadOver = Dinero.DineroOver / (Dinero.DineroOver + Dinero.DineroUnder);
            double cuotaOver = 0;
            double over = 0.95;
            if (probabilidadOver != 0)
            {
                cuotaOver = 1 / probabilidadOver * 0.95;
                over = Math.Round((double)Convert.ToDouble(cuotaOver), 2);
            }

            double probabilidadUnder = Dinero.DineroUnder / (Dinero.DineroOver + Dinero.DineroUnder);
            double cuotaUnder = 0;
            double under = 0.95;
            if (probabilidadUnder != 0)
            {
                cuotaUnder = 1 / probabilidadUnder * 0.95;
                under = Math.Round((double)Convert.ToDouble(cuotaUnder), 2);
            }




            Debug.WriteLine("under: " + cuotaUnder + "dineroOver: " + Dinero.DineroOver + "dineroUnder: " + Dinero.DineroUnder + "probabilidad: " + probabilidadUnder);
            Debug.WriteLine("under: "+ under);
            con.Open();

                command.CommandText = "UPDATE Mercado SET CuotaUnder=" + under.ToString(CultureInfo.CreateSpecificCulture("us-US"))+ ", CuotaOver =" + over.ToString(CultureInfo.CreateSpecificCulture("us-US")) + " WHERE ID= " + Dinero.MercadoID + ";";
                Debug.WriteLine("Comando: " + command.CommandText);

            

            command.ExecuteNonQuery();






        }



    }
}