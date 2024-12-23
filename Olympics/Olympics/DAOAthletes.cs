using MSSTU.DB.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics
{
    internal class DAOAthletes : IDAO
    {
        #region Singleton
        private IDatabase db;
        private DAOAthletes() 
        {
            db = new Database("Olympics");
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
            return db.UpdateDb($"INSERT INTO Athletes (athleteName, surname, dateOfBirth, country) " +
                               $"VALUES " +
                               $"('{((Athlete)entity).AthleteName.Replace("'","''")}', " +
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
            if (instance == null)
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
            foreach (var row in rows)
            {
                Entity entity = new Athlete();
                entity.TypeSort(row);

                list.Add(entity);

            }
                return new List<Entity>();
        }

        public bool UpdateRecord(Entity entity)
        {
            return db.UpdateDb($"UPDATE Athletes SET " +
                               $"athleteName = '{((Athlete)entity).AthleteName.Replace("'", "''")}', " +
                               $"surname = '{((Athlete)entity).Surname.Replace("'", "''")}', " +
                               $"dateOfBirth = '{((Athlete)entity).DateOfBirth.ToString("yyyy-MM-dd")}', " +
                               $"country = '{((Athlete)entity).Country.Replace("'", "''")}' " +
                               $"WHERE id = {entity.Id};");
        }

        #endregion
    }
}
