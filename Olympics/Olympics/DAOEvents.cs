using MSSTU.DB.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics
{
    internal class DAOEvents : IDAO
    {
        #region Singleton
        private IDatabase db;
        private DAOEvents()
        {
            db = new Database("Olympics","ArtuzPc");
        }
        private static DAOEvents instance = null;
        public static DAOEvents GetInstance()
        {
            if (instance == null)
                instance = new DAOEvents();
            return instance;
        }
        #endregion

        #region CRUD
        public bool CreateRecord(Entity entity)
        {
            return db.UpdateDb($"INSERT INTO SportEvents (id, eventName, eventYear, eventLocation) " +
                               $"VALUES " +
                               $"( {((Event)entity).Id}, " +
                               $" '{((Event)entity).EventName.Replace("'", "''")}', " +
                               $"  {((Event)entity).EventYear}, " +
                               $" '{((Event)entity).EventLocation.Replace("'","''")}');");
        }
        public bool DeleteRecord(int recordId)
        {
            return db.UpdateDb($"DELETE FROM SportEvents WHERE id = {recordId};");
        }
        public Entity? FindRecord(int recordId)
        {
            var row = db.ReadOneDb($"SELECT * FROM SportEvents WHERE id = {recordId};");
            if (row != null)
            {
                Entity entity = new Event();
                entity.TypeSort(row);
                return entity;
            }
            else
                return null;
        }
        public List<Entity> GetRecords()
        {
            List<Entity> list = new List<Entity>();
            var rows = db.ReadDb("SELECT * FROM SportEvents;");
            if (rows.Count <= 1)
            {
                return null;
            }
            else
            {
                foreach (var row in rows)
                {
                    Entity entity = new Event();
                    entity.TypeSort(row);
                    list.Add(entity);
                }
                return list;
            }
        }
        public bool UpdateRecord(Entity entity)
        {
            return db.UpdateDb($"UPDATE SportEvents SET " +
                               $"id = {((Event)entity).Id}, " +
                               $"eventName = '{((Event)entity).EventName.Replace("'", "''")}', " +
                               $"eventYear = {((Event)entity).EventYear}, " +
                               $"eventLocation = '{((Event)entity).EventLocation.Replace("'", "''")}' " +
                               $"WHERE id = {entity.Id};");
        }

        public void ImportEventsFromFile(string filePath)
        {
            try
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var values = line.Split(';');
                    if (values.Length < 4)
                    {
                        Console.WriteLine($"Skipping line due to insufficient data: {line}");
                        continue;
                    }
                    var dictionary = new Dictionary<string, string>
                    {
                            { "id", values[0] },
                            { "eventName", values[1] },
                            { "eventYear", values[2] },
                            { "eventLocation", values[3] }
                    };

                    Entity entity = new Event();
                    ((Event)entity).FromDictionary(dictionary);

                    if(entity != null)
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
                Console.WriteLine($"An error occurred while importing events from file: {ex.Message}");
            }
        }

        #endregion
    }

}
