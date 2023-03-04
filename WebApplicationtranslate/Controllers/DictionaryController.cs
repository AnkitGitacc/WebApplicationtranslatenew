using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationtranslate.Controllers
{
    public class DictionaryController : Controller
    {
        // GET: Dictionary
        private readonly string connectionString = "Server=DESKTOP-JUL9JEG\\SQLEXPRESS;Database=TranslationDB;Trusted_Connection=True;";

        [HttpGet]
        public ActionResult Translate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Translate(string english)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT Hungarian FROM Translationsnewdata WHERE English = '{english}'";
                var command = new SqlCommand(query, connection);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        string hungarian = reader.GetString(0);
                        return Json(new { Translation = hungarian });
                    }
                    else
                    {
                        return Json(new { Error = "Translation not found" },JsonRequestBehavior.AllowGet);
                    }
                }
                connection.Close();
            }
        }
    }
}
    
   