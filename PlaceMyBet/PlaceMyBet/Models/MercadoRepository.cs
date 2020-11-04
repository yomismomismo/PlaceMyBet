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

                Debug.WriteLine("Recuparado: " + res.GetDouble(1) + res.GetDouble(2) + res.GetDouble(3));
                m = new Mercado(res.GetInt16(0), res.GetDouble(1), res.GetDouble(2), res.GetDouble(3), res.GetDouble(4), res.GetDouble(5), res.GetDouble(6));
                mercado.Add(m);
            }

            return mercado;

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

        internal List<ApuestaMercado> GetApuestasMercado(double tipoMercado, string email)
        {

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT m.OverUnder, m.ID_Evento , a.TipoApuesta, a.Cuota, a.DineroApostado, a.Email_Usuario FROM mercado m join apuesta a ON a.ID_Mercado = m.ID WHERE OverUnder = @tipo && Email_usuario = @email ";
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@tipo", tipoMercado);

            con.Open();
            MySqlDataReader res = command.ExecuteReader();

            ApuestaMercado apuestasUser = null;
            List<ApuestaMercado> mercadoApuesta = new List<ApuestaMercado>();

            while (res.Read())
            {


                apuestasUser = new ApuestaMercado(res.GetInt32(1), res.GetString(2), res.GetDouble(3), res.GetDouble(4));
                mercadoApuesta.Add(apuestasUser);
            }

            return mercadoApuesta;

        }

        internal Mercado getMercado(int id, double tipo)
        {

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT * FROM Mercado where ID_Evento= @id && OverUnder=@tipo";
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@tipo", tipo);

            con.Open();
            MySqlDataReader res = command.ExecuteReader();

            Mercado m = null;

            if (res.Read())
            {

                Debug.WriteLine("Recuparado: " + res.GetDouble(1) + res.GetDouble(2) + res.GetDouble(3));
                m = new Mercado(res.GetInt32(0), res.GetDouble(1), res.GetDouble(2), res.GetDouble(3), res.GetDouble(4), res.GetDouble(5), res.GetDouble(6));
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



        internal MercadoDinero RetrieveDinero()
        {

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT * FROM Mercado";

            con.Open();
            MySqlDataReader res = command.ExecuteReader();

            MercadoDinero Dinero = null;

            if (res.Read())
            {
                Debug.WriteLine("dinero recuperado " + res.GetInt32(0) + res.GetDouble(4) + res.GetDouble(5));
                Dinero = new MercadoDinero(res.GetInt32(0), res.GetDouble(4), res.GetDouble(5));
            }

            return Dinero;

        }

        internal void Calculos(MercadoDinero Dinero)
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