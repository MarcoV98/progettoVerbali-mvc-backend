using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using progettoVerbali_mvc_backend.Models;
using System.Collections.Generic;

namespace progettoVerbali_mvc_backend.Controllers
{
    public class AnagraficaController : Controller
    {
        private readonly string _connectionString = "Data Source=DESKTOP-PD7VIV6\\SQLEXPRESS;Initial Catalog=progettoVerbali;Integrated Security=True; TrustServerCertificate=True";

        public IActionResult Index()
        {
            List<Anagrafica> anagrafiche = GetAnagraficheFromDatabase();
            return View(anagrafiche);
        }

        public IActionResult Details(int id)
        {
            Anagrafica anagrafica = GetAnagraficaFromDatabase(id);
            if (anagrafica == null)
            {
                return NotFound();
            }
            return View(anagrafica);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Anagrafica anagrafica)
        {
            if (ModelState.IsValid)
            {
                InsertAnagraficaIntoDatabase(anagrafica);
                return RedirectToAction(nameof(Index));
            }
            return View(anagrafica);
        }

        public IActionResult Edit(int id)
        {
            Anagrafica anagrafica = GetAnagraficaFromDatabase(id);
            if (anagrafica == null)
            {
                return NotFound();
            }
            return View(anagrafica);
        }

        [HttpPost]
        public IActionResult Edit(Anagrafica anagrafica)
        {
            if (ModelState.IsValid)
            {
                UpdateAnagraficaInDatabase(anagrafica);
                TempData["Message"] = "Anagrafica modificata con successo";
                return RedirectToAction(nameof(Index));
            }
            return View(anagrafica);
        }

        public IActionResult Delete(int id)
        {
            Anagrafica anagrafica = GetAnagraficaFromDatabase(id);
            if (anagrafica == null)
            {
                return NotFound();
            }
            return View(anagrafica);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            DeleteAnagraficaFromDatabase(id);
            TempData["Message"] = "Anagrafica eliminata con successo";
            return RedirectToAction(nameof(Index));
        }

        private List<Anagrafica> GetAnagraficheFromDatabase()
        {
            List<Anagrafica> anagrafiche = new List<Anagrafica>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Anagrafica";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Anagrafica anagrafica = new Anagrafica
                            {
                                IDAnagrafica = (int)reader["IDAnagrafica"],
                                Cognome = reader["Cognome"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                Indirizzo = reader["Indirizzo"].ToString(),
                                Città = reader["Città"].ToString(),
                                CAP = reader["CAP"].ToString(),
                                Cod_Fisc = reader["Cod_Fisc"].ToString()
                            };
                            anagrafiche.Add(anagrafica);
                        }
                    }
                }
            }

            return anagrafiche;
        }

        private Anagrafica GetAnagraficaFromDatabase(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Anagrafica WHERE IDAnagrafica = @ID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Anagrafica anagrafica = new Anagrafica
                            {
                                IDAnagrafica = (int)reader["IDAnagrafica"],
                                Cognome = reader["Cognome"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                Indirizzo = reader["Indirizzo"].ToString(),
                                Città = reader["Città"].ToString(),
                                CAP = reader["CAP"].ToString(),
                                Cod_Fisc = reader["Cod_Fisc"].ToString()
                            };
                            return anagrafica;
                        }
                    }
                }
            }

            return null;
        }

        private void InsertAnagraficaIntoDatabase(Anagrafica anagrafica)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO Anagrafica (Cognome, Nome, Indirizzo, Città, CAP, Cod_Fisc) " +
                             "VALUES (@Cognome, @Nome, @Indirizzo, @Città, @CAP, @Cod_Fisc)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Cognome", anagrafica.Cognome);
                    command.Parameters.AddWithValue("@Nome", anagrafica.Nome);
                    command.Parameters.AddWithValue("@Indirizzo", anagrafica.Indirizzo);
                    command.Parameters.AddWithValue("@Città", anagrafica.Città);
                    command.Parameters.AddWithValue("@CAP", anagrafica.CAP);
                    command.Parameters.AddWithValue("@Cod_Fisc", anagrafica.Cod_Fisc);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void UpdateAnagraficaInDatabase(Anagrafica anagrafica)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "UPDATE Anagrafica SET Cognome = @Cognome, Nome = @Nome, " +
                             "Indirizzo = @Indirizzo, Città = @Città, CAP = @CAP, Cod_Fisc = @Cod_Fisc " +
                             "WHERE IDAnagrafica = @ID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ID", anagrafica.IDAnagrafica);
                    command.Parameters.AddWithValue("@Cognome", anagrafica.Cognome);
                    command.Parameters.AddWithValue("@Nome", anagrafica.Nome);
                    command.Parameters.AddWithValue("@Indirizzo", anagrafica.Indirizzo);
                    command.Parameters.AddWithValue("@Città", anagrafica.Città);
                    command.Parameters.AddWithValue("@CAP", anagrafica.CAP);
                    command.Parameters.AddWithValue("@Cod_Fisc", anagrafica.Cod_Fisc);

                    command.ExecuteNonQuery();
                }
            }
        }

        private bool DeleteAnagraficaFromDatabase(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sql = "DELETE FROM Anagrafica WHERE IDAnagrafica = @ID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

