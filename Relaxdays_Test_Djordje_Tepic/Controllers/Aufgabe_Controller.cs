using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

namespace Relaxdays_Test_Djordje_Tepic.Controllers
{
    [Route("api/api/v1/createDatabase")]
    [ApiController]
    public class Aufgabe_Controller : ControllerBase
    {
        [HttpPost]
        public DatenbankReturn Main(string Datenbankname)
        {
            string b = GetPassword();
            var liste = DateiEinlesen();

            Datenbank ret = new Datenbank()
            {
                dbName = $"{Datenbankname}",
                dbUser = $"task{Datenbankname}mysql",
                dbPasswort = $"{b}"
            };
            liste.Add(ret);
            string jsonString = JsonSerializer.Serialize(liste);
            System.IO.File.WriteAllText("Datei.json", jsonString);



            DatenbankReturn kreatur = new DatenbankReturn()
            {
                dbPasswort = $"{b}",
                dbUser = $"task{Datenbankname}mysql"
            };
            return kreatur;

        }

        private string GetPassword()
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var Charsarr = new char[32];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }

            var resultString = new String(Charsarr);

            return resultString;

        }
        
        public static List<Datenbank> DateiEinlesen()     //Auswahl 5; object vor DateiEinlesen?
        {
            List<Datenbank> ob = new List<Datenbank>();                          // object ob = null;?

            try
            {

                string JsonContent = System.IO.File.ReadAllText("Datei.json");   //Datei Einlesen system.IO...

                //Json-Datei einlesen
                return JsonSerializer.Deserialize<List<Datenbank>>(JsonContent); // generische Klasse --> mehere Typen, mit den Klammern welcher Typ

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return ob;
        }
    }





}