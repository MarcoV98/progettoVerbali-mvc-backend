using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using progettoVerbali_mvc_backend.Models;

namespace progettoVerbali_mvc_backend.Controllers
{
    public class StatisticheController : Controller
    {
        private readonly string _connectionString = "Data Source=DESKTOP-PD7VIV6\\SQLEXPRESS;Initial Catalog=progettoVerbali;Integrated Security=True; TrustServerCertificate=True";

        public IActionResult TotaleVerbali()
        {
            List<TotaleVerbaliVM> totaleVerbali = GetTotaleVerbaliFromDatabase();
            return View(totaleVerbali);
        }

        public IActionResult TotalePunti()
        {
            List<TotalePuntiVM> totalePunti = GetTotalePuntiFromDatabase();
            return View(totalePunti);
        }

        public IActionResult ViolazioniPunti()
        {
            List<ViolazioneVM> violazioniPunti = GetViolazioniPuntiFromDatabase();
            return View(violazioniPunti);
        }

        public IActionResult ViolazioniImporto()
        {
            List<ViolazioneVM> violazioniImporto = GetViolazioniImportoFromDatabase();
            return View(violazioniImporto);
        }

        private List<TotaleVerbaliVM> GetTotaleVerbaliFromDatabase()
        {
            List<TotaleVerbaliVM> totaleVerbali = new List<TotaleVerbaliVM>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "SELECT Anag.IDAnagrafica, Anag.Cognome, Anag.Nome, COUNT(Verb.IDVerbale) AS TotaleVerbali " +
                             "FROM Anagrafica Anag " +
                             "LEFT JOIN Verbali Verb ON Anag.IDAnagrafica = Verb.AnagraficaID " +
                             "GROUP BY Anag.IDAnagrafica, Anag.Cognome, Anag.Nome";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TotaleVerbaliVM totale = new TotaleVerbaliVM
                            {
                                IDAnagrafica = (int)reader["IDAnagrafica"],
                                Cognome = reader["Cognome"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                TotaleVerbali = (int)reader["TotaleVerbali"]
                            };
                            totaleVerbali.Add(totale);
                        }
                    }
                }
            }

            return totaleVerbali;
        }

        private List<TotalePuntiVM> GetTotalePuntiFromDatabase()
        {
            List<TotalePuntiVM> totalePunti = new List<TotalePuntiVM>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "SELECT Anag.IDAnagrafica, Anag.Cognome, Anag.Nome, SUM(Verb.DecurtamentoPunti) AS TotalePunti " +
                             "FROM Anagrafica Anag " +
                             "LEFT JOIN Verbali Verb ON Anag.IDAnagrafica = Verb.AnagraficaID " +
                             "GROUP BY Anag.IDAnagrafica, Anag.Cognome, Anag.Nome";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TotalePuntiVM totale = new TotalePuntiVM
                            {
                                IDAnagrafica = (int)reader["IDAnagrafica"],
                                Cognome = reader["Cognome"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                TotalePunti = (int)reader["TotalePunti"]
                            };
                            totalePunti.Add(totale);
                        }
                    }
                }
            }

            return totalePunti;
        }

        private List<ViolazioneVM> GetViolazioniPuntiFromDatabase()
        {
            List<ViolazioneVM> violazioniPunti = new List<ViolazioneVM>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "SELECT Verb.IDVerbale, Verb.DataViolazione, Verb.IndirizzoViolazione, " +
                             "Verb.Nominativo_Agente, Verb.DataTrascrizioneVerbale, Verb.Importo, " +
                             "Verb.DecurtamentoPunti, Tipo.Descrizione AS DescrizioneTipo " +
                             "FROM Verbali Verb " +
                             "INNER JOIN Anagrafica Anag ON Verb.AnagraficaID = Anag.IDAnagrafica " +
                             "INNER JOIN TipoViolazione Tipo ON Verb.TipoViolazioneID = Tipo.IDViolazione " +
                             "WHERE Verb.DecurtamentoPunti > 0";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ViolazioneVM violazione = new ViolazioneVM
                            {
                                IDVerbale = (int)reader["IDVerbale"],
                                DataViolazione = (DateTime)reader["DataViolazione"],
                                IndirizzoViolazione = reader["IndirizzoViolazione"].ToString(),
                                NominativoAgente = reader["Nominativo_Agente"].ToString(),
                                DataTrascrizioneVerbale = (DateTime)reader["DataTrascrizioneVerbale"],
                                Importo = (decimal)reader["Importo"],
                                DecurtamentoPunti = (int)reader["DecurtamentoPunti"],
                                DescrizioneTipoViolazione = reader["DescrizioneTipo"].ToString()
                            };
                            violazioniPunti.Add(violazione);
                        }
                    }
                }
            }

            return violazioniPunti;
        }

        private List<ViolazioneVM> GetViolazioniImportoFromDatabase()
        {
            List<ViolazioneVM> violazioniImporto = new List<ViolazioneVM>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "SELECT Verb.IDVerbale, Verb.DataViolazione, Verb.IndirizzoViolazione, " +
                             "Verb.Nominativo_Agente, Verb.DataTrascrizioneVerbale, Verb.Importo, " +
                             "Verb.DecurtamentoPunti, Tipo.Descrizione AS DescrizioneTipo " +
                             "FROM Verbali Verb " +
                             "INNER JOIN Anagrafica Anag ON Verb.AnagraficaID = Anag.IDAnagrafica " +
                             "INNER JOIN TipoViolazione Tipo ON Verb.TipoViolazioneID = Tipo.IDViolazione " +
                             "WHERE Verb.Importo > 400";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ViolazioneVM violazione = new ViolazioneVM
                            {
                                IDVerbale = (int)reader["IDVerbale"],
                                DataViolazione = (DateTime)reader["DataViolazione"],
                                IndirizzoViolazione = reader["IndirizzoViolazione"].ToString(),
                                NominativoAgente = reader["Nominativo_Agente"].ToString(),
                                DataTrascrizioneVerbale = (DateTime)reader["DataTrascrizioneVerbale"],
                                Importo = (decimal)reader["Importo"],
                                DecurtamentoPunti = (int)reader["DecurtamentoPunti"],
                                DescrizioneTipoViolazione = reader["DescrizioneTipo"].ToString()
                            };
                            violazioniImporto.Add(violazione);
                        }
                    }
                }
            }

            return violazioniImporto;
        }
    }
}
