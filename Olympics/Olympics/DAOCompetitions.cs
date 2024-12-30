using MSSTU.DB.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics
{
    internal class DAOCompetitions : IDAO
    {
        #region Singleton
        private IDatabase db;
        private DAOCompetitions()
        {
            db = new Database("Olympics","ArtuzPc");
        }
        private static DAOCompetitions instance = null;
        public static DAOCompetitions GetInstance()
        {
            if (instance == null)
                instance = new DAOCompetitions();
            return instance;
        }
        #endregion

        #region CRUD
        public bool CreateRecord(Entity entity)
        {
            return db.UpdateDb($"INSERT INTO Competitions (id, competitionName, category, isIndoor, isTeamCompetition) " +
                               $"VALUES " +
                               $"( {((Competition)entity).Id}, " +
                               $" '{((Competition)entity).CompetitionName.Replace("'", "''")}', " +
                               $" '{((Competition)entity).Category.Replace("'", "''")}', " +
                               $"  {(((Competition)entity).IsIndoor ? 1 : 0)}, " +
                               $"  {(((Competition)entity).IsTeamCompetition ? 1: 0)});");
        }
        public bool DeleteRecord(int recordId)
        {
            return db.UpdateDb($"DELETE FROM Competitions WHERE id = {recordId};");
        }
        public Entity? FindRecord(int recordId)
        {
            var row = db.ReadOneDb($"SELECT * FROM Competitions WHERE id = {recordId};");
            if (row != null)
            {
                Entity entity = new Competition();
                entity.TypeSort(row);
                return entity;
            }
            else
                return null;
        }
        public List<Entity> GetRecords()
        {
            List<Entity> list = new List<Entity>();
            var rows = db.ReadDb("SELECT * FROM Competitions;");
            if (rows.Count <= 1)
            {
                return null;
            }
            else
            {
                foreach (var row in rows)
                {
                    Entity entity = new Competition();
                    entity.TypeSort(row);
                    list.Add(entity);
                }
                return list;
            }
        }
        public bool UpdateRecord(Entity entity)
        {
            return db.UpdateDb($"UPDATE Competitions SET " + 
                               $"id = {((Competition)entity).Id}, " +
                               $"competitionName = '{((Competition)entity).CompetitionName.Replace("'", "''")}', " +
                               $"category = '{((Competition)entity).Category.Replace("'", "''")}', " +
                               $"isIndoor =  {(((Competition)entity).IsIndoor ? 1 : 0)}, " +
                               $"isTeamCompetition = {(((Competition)entity).IsTeamCompetition ? 1 : 0)} " +
                               $"WHERE id = {entity.Id};");
        }

        public void ImportCompetitionsFromFile(string filePath)
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
                            { "competitionName", values[1] },
                            { "category", values[2] },
                            { "isIndoor", values[3] },
                            { "isTeamCompetition", values[4] }
                    };

                    Entity entity = new Competition();
                    ((Competition)entity).FromDictionary(dictionary);

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
                Console.WriteLine($"An error occurred while importing competitions from file: {ex.Message}");
            }
        }

        #endregion
    }

}
