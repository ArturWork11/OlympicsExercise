using MSSTU.DB.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics
{
    internal class DAOAthletes : IDAO
    {
        #region Singleton
        private Database db;
        private DAOAthletes() 
        {
            db = new Database("Olympics","ArtuzPc");
        }
        private static DAOAthletes instance = null;
        public static DAOAthletes GetInstance()
        {
            if (instance == null)
                instance = new DAOAthletes();
            return instance;
        }
        #endregion

        #region CRUD
        public bool CreateRecord(Entity entity)
        {

            return db.UpdateDb($"INSERT INTO Athletes (id, athleteName, surname, dateOfBirth, country) " +
                               $"VALUES " + 
                               $"( {((Athlete)entity).Id}, " +
                               $" '{((Athlete)entity).AthleteName.Replace("'","''")}', " +
                               $" '{((Athlete)entity).Surname.Replace("'","''")}', " +
                               $" '{((Athlete)entity).DateOfBirth.ToString("yyyy-MM-dd")}', " +
                               $" '{((Athlete)entity).Country.Replace("'","''")}');");
        }

        public bool DeleteRecord(int recordId)
        {
            return db.UpdateDb($"DELETE FROM Athletes WHERE id = {recordId};");
        }

        public Entity? FindRecord(int recordId)
        {
            var row = db.ReadOneDb($"SELECT * FROM Athletes WHERE id = {recordId};");
            if (row != null)
            {
                Entity entity = new Athlete();
                entity.TypeSort(row);

                return entity;
            }
            else
                return null;
        }

        public List<Entity> GetRecords()
        {
            List<Entity> list = new List<Entity>();
            var rows = db.ReadDb("SELECT * FROM Athletes;");
            if (rows.Count <= 1)
            {
                return null;
            }
            else
            {
                foreach (var row in rows)
                {
                    Entity entity = new Athlete();
                    entity.TypeSort(row);

                    list.Add(entity);

                }
                return list;
            }
        }

        public bool UpdateRecord(Entity entity)
        {
            return db.UpdateDb($"UPDATE Athletes SET " + 
                               $"id = {((Athlete)entity).Id}, " +
                               $"athleteName = '{((Athlete)entity).AthleteName.Replace("'", "''")}', " +
                               $"surname = '{((Athlete)entity).Surname.Replace("'", "''")}', " +
                               $"dateOfBirth = '{((Athlete)entity).DateOfBirth.ToString("yyyy-MM-dd")}', " +
                               $"country = '{((Athlete)entity).Country.Replace("'", "''")}' " +
                               $"WHERE id = {entity.Id};");
        }

        public void ImportAthletesFromFile(string filePath)
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
                            { "athleteName", values[1] },
                            { "surname", values[2] },
                            { "dateOfBirth", values[3] },
                            { "country", values[4] }
                    };

                    Entity entity = new Athlete();                  

                    ((Athlete)entity).FromDictionary(dictionary);

                    if (entity != null)
                    {
                        CreateRecord(entity);
                    }
                
                    else
                    {
                        Console.WriteLine("Failed to create entity from the provided data.");
                    }

                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"The file at path {filePath} was not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while importing athletes from file: {ex.Message}");
            }
        }

        #endregion
    }
}
