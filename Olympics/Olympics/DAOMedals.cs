using MSSTU.DB.Utility;
using System;
using System.Collections.Generic;
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
            db = new Database("Olympics");
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
            return db.UpdateDb($"INSERT INTO Medals (idAthlete, idCompetition, idEvent, medalTier) " +
                               $"VALUES " +
                               $"( {(((Medal)entity).Athlete != null ? ((Medal)entity).Athlete.Id : "null")}, " +
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
            if (instance == null)
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
            foreach (var row in rows)
            {
                Entity entity = new Medal();
                entity.TypeSort(row);
                list.Add(entity);
            }
            return list;
        }
        public bool UpdateRecord(Entity entity)
        {
            return db.UpdateDb($"UPDATE Medals SET " +
                               $"idAthlete = {(((Medal)entity).Athlete != null ? ((Medal)entity).Athlete.Id : "null")}, " +
                               $"idCompetition = {(((Medal)entity).Competition != null ? ((Medal)entity).Competition.Id : "null")}, " +
                               $"idEvent = {(((Medal)entity).Event != null ? ((Medal)entity).Event.Id : "null")}, " +
                               $"medalTier = '{((Medal)entity).MedalTier}' " +
                               $"WHERE id = {entity.Id};");
        }

        #endregion
    }

}
