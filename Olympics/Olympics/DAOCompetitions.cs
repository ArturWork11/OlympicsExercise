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
            db = new Database("Olympics");
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
            return db.UpdateDb($"INSERT INTO Competitions (competitionName, category, isIndoor, isTeamCompetitions) " +
                               $"VALUES " +
                               $"('{((Competition)entity).CompetitionName.Replace("'", "''")}', " +
                               $"('{((Competition)entity).Category.Replace("'", "''")}', " +
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
            if (instance == null)
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
            foreach (var row in rows)
            {
                Entity entity = new Competition();
                entity.TypeSort(row);
                list.Add(entity);
            }
            return list;
        }
        public bool UpdateRecord(Entity entity)
        {
            return db.UpdateDb($"UPDATE Competitions SET " +
                               $"competitionName = '{((Competition)entity).CompetitionName.Replace("'", "''")}', " +
                               $"category = '{((Competition)entity).Category.Replace("'", "''")}', " +
                               $"isIndoor =  {(((Competition)entity).IsIndoor ? 1 : 0)}, " +
                               $"isTeamCompetitions = {(((Competition)entity).IsTeamCompetition ? 1 : 0)} " +
                               $"WHERE id = {entity.Id};");
        }

        #endregion
    }

}
