using MSSTU.DB.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics
{
    internal class DAOMedals : IDAO
    {

        #region Singleton
        private IDatabase db;
        private DAOMedals()
        {
            db = new Database("Olympics","ArtuzPc");
        }
        private static DAOMedals instance = null;
        public static DAOMedals GetInstance()
        {
            if (instance == null)
                instance = new DAOMedals();
            return instance;
        }
        #endregion

        #region CRUD
        public bool CreateRecord(Entity entity)
        {
            return db.UpdateDb($"INSERT INTO Medals (id, idAthlete, idCompetition, idEvent, medalTier) " +
                               $"VALUES " + 
                               $"( {((Medal)entity).Id}, " +
                               $"  {(((Medal)entity).Athlete != null ? ((Medal)entity).Athlete.Id : "null")}, " +
                               $"  {(((Medal)entity).Competition != null ? ((Medal)entity).Competition.Id : "null")}, " +
                               $"  {(((Medal)entity).Event != null ? ((Medal)entity).Event.Id : "null")}, " +
                               $" '{((Medal)entity).MedalTier}');");
        }
        public bool DeleteRecord(int recordId)
        {
            return db.UpdateDb($"DELETE FROM Medals WHERE id = {recordId};");
        }
        public Entity? FindRecord(int recordId)
        {
            var row = db.ReadOneDb($"SELECT * FROM Medals WHERE id = {recordId};");
            if (row != null)
            {
                Entity entity = new Medal();
                entity.TypeSort(row);
                return entity;
            }
            else
                return null;
        }
        public List<Entity> GetRecords()
        {
            List<Entity> list = new List<Entity>();
            var rows = db.ReadDb("SELECT * FROM Medals;");
            if (rows.Count <= 1)
            {
                return null;
            }
            else
            {
                foreach (var row in rows)
                {
                    Entity entity = new Medal();
                    entity.TypeSort(row);
                    list.Add(entity);
                }
                return list;
            }
        }
        public bool UpdateRecord(Entity entity)
        {
            return db.UpdateDb($"UPDATE Medals SET " +
                               $"id = {((Medal)entity).Id}, " +
                               $"idAthlete = {(((Medal)entity).Athlete != null ? ((Medal)entity).Athlete.Id : "null")}, " +
                               $"idCompetition = {(((Medal)entity).Competition != null ? ((Medal)entity).Competition.Id : "null")}, " +
                               $"idEvent = {(((Medal)entity).Event != null ? ((Medal)entity).Event.Id : "null")}, " +
                               $"medalTier = '{((Medal)entity).MedalTier}' " +
                               $"WHERE id = {entity.Id};");
        }

        public void ImportMedalsFromFile(string filePath)
        {
            try
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var values = line.Split(';');
                    if (values.Length < 5)
                    {
                        Console.WriteLine($"Skipping line due to insufficient data: {line}");
                        continue;
                    }
                    var dictionary = new Dictionary<string, string>
                    {
                            { "id", values[0] },
                            { "idAthlete", values[1] },
                            { "idCompetition", values[2] },
                            { "idEvent", values[3] },
                            { "medalTier", values[4] }

                    };

                    Entity entity = new Medal();

                    
                    ((Medal)entity).FromDictionary(dictionary);

                    if (entity != null)
                {


                    CreateRecord(entity);
                }
                else
                {
                    Console.WriteLine("Failed to create entity from the provided Data");
                }
            }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"The file at path {filePath} was not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while importing medals from file: {ex.Message}");
            }
        }
        #endregion

        #region Additional Methods

        public void AllMedalsOfAnAthlete(int athleteId)
        {
                    
            var rows = db.ReadDb($"SELECT m.id ,idAthlete, idCompetition, idEvent, medalTier,athleteName, surname,competitionName,eventName  FROM Medals m JOIN Athletes a ON m.idAthlete = a.Id  \r\n JOIN Competitions c ON m.idCompetition = c.Id \r\n  JOIN SportEvents e ON m.idEvent = e.id  \r\n WHERE a.Id = {athleteId} ORDER BY CASE  \r\n WHEN m.medalTier = 'Oro' THEN 1 \r\n WHEN m.medalTier = 'Argento' THEN 2\r\n WHEN m.medalTier = 'Bronzo' THEN 3\r\n END, e.eventYear;");
            if (rows.Count < 1)
            {
                Console.WriteLine("No medals found for the specified athlete.");
            }
            else 
            {

                foreach (var row in rows)
                {
                    Entity medal = new Medal();
                    medal.TypeSort(row);                   
                    Console.WriteLine(medal.ToString());
                }
            }            
        }

        public void CountOfEachTierOfMedalsWonByAnAthlete(int athleteId)
        {
            bool athlete = false;           
            var rows = db.ReadDb($"SELECT athleteName, Surname,medalTier, COUNT(*) as medalCount\r\n FROM Medals m join Athletes a\r\n ON m.idAthlete = a.id\r\n WHERE idAthlete = {athleteId} \r\n GROUP BY athleteName, surname, medalTier");
            if (rows.Count < 1)
            {
                Console.WriteLine("No medals found for the specified Athlete.");
            }
            else
            { 
                
                foreach (var row in rows)
                {
                    if (athlete == false)
                    {
                        Console.WriteLine($"\nMedals won by Athlete: {row["athletename"]} {row["surname"]} \n");
                        athlete = true;
                    }
                    Console.WriteLine($"Medal Tier: {row["medaltier"]} \tTotal: {row["medalcount"]}");
                }
            }
        }

        public void AthletesWhoWonMedalsForTheirCountry(string country)
        {
            var rows = db.ReadDb($"SELECT athleteName, Surname, country, count(*) as medalCount\r\nFROM Medals m join Athletes a\r\non m.idAthlete = a.id\r\nwhere country = '{country.ToLower()}'\r\ngroup by athleteName, surname, country;");
            if (rows.Count < 1)
            {
                Console.WriteLine("No Athlete won medals for the specified country.");
            }
            else
            {
                Console.WriteLine($"\nAthletes who won medals for {country}:\n");
                foreach (var row in rows)
                {
                    Console.WriteLine($"Athlete: {row["athletename"]} {row["surname"]} \tTotal Medals won: {row["medalcount"]}");
                }
            }
        }



        #endregion
    }
}
