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
            db = new Database("Olympics");
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
            return db.UpdateDb($"INSERT INTO SportEvents (eventName, eventYear, eventLocation) " +
                               $"VALUES " +
                               $"('{((Event)entity).EventName.Replace("'", "''")}', " +
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
            if (instance == null)
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
            foreach (var row in rows)
            {
                Entity entity = new Event();
                entity.TypeSort(row);
                list.Add(entity);
            }
            return list;
        }
        public bool UpdateRecord(Entity entity)
        {
            return db.UpdateDb($"UPDATE SportEvents SET " +
                               $"eventName = '{((Event)entity).EventName.Replace("'", "''")}', " +
                               $"eventYear = {((Event)entity).EventYear}, " +
                               $"eventLocation = '{((Event)entity).EventLocation.Replace("'", "''")}' " +
                               $"WHERE id = {entity.Id};");
        }

        #endregion
    }
    
}
