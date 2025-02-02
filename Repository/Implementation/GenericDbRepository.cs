using Domain;
using Repository.DBConnection;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class GenericDbRepository : IDbRepository<AbsEntity>
    {
        public void Close()
        {
            DbConnectionFactory.Instance.GetDbConnection().Close();
        }
        public void Commit()
        {
            DbConnectionFactory.Instance.GetDbConnection().Commit();
        }
        public void Rollback()
        {
            DbConnectionFactory.Instance.GetDbConnection().Rollback();
        }

        public AbsEntity Add(AbsEntity entity)
        {
            SqlCommand cmd = new SqlCommand();

            cmd = DbConnectionFactory.Instance.GetDbConnection().CreateCommand($"insert into {entity.TableName} values ({entity.ValuesToInsert(entity)})");

            if (cmd.ExecuteNonQuery() == 0)
            {
                return null;
            }
            return entity;
        }

        public AbsEntity Get(AbsEntity entity, int key)
        {
            AbsEntity result;
            SqlCommand cmd = DbConnectionFactory.Instance.GetDbConnection().CreateCommand($"select * from {entity.TableName} {entity.JoinKeys()} where {entity.CheckId(key)}" );
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                List<AbsEntity> entities = new List<AbsEntity>();
                entities = entity.ReaderRead(reader);
                if (entities.Count == 0)
                {
                    reader.Close();
                    return null;
                }
                result = entities[0];
                reader.Close();
                return result;
            }
        }

        public List<AbsEntity> GetAll(AbsEntity entity)
        {
            SqlCommand cmd = new SqlCommand();
            List<AbsEntity> result;

            cmd = DbConnectionFactory.Instance.GetDbConnection().CreateCommand($"select * from {entity.TableName} " + entity.JoinKeys());
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                result = entity.ReaderRead(reader);
                return result;
            }
        }

        public List<AbsEntity> Search(AbsEntity entity, string criteria)
        {
            SqlCommand cmd = new SqlCommand();
            List<AbsEntity> result;

            cmd = DbConnectionFactory.Instance.GetDbConnection().CreateCommand($"select * from {entity.TableName} " + entity.JoinKeys() + " where " + entity.Search(criteria));
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                result = entity.ReaderRead(reader);
                return result;
            }
        }

        public AbsEntity Update(AbsEntity entity, int key)
        {
            SqlCommand cmd;
            cmd = DbConnectionFactory.Instance.GetDbConnection().CreateCommand($"update {entity.TableName} set {entity.ValuesToSet(entity)} where {entity.CheckId(key)};");
            if (cmd.ExecuteNonQuery() == 0)
            {
                return null;
            }
            return entity;
        }

        public AbsEntity Delete(AbsEntity entity, int key)
        {
            SqlCommand cmd;
            cmd = DbConnectionFactory.Instance.GetDbConnection().CreateCommand($"delete from {entity.TableName} where {entity.CheckId(key)};");
            if (cmd.ExecuteNonQuery() == 0)
            {
                return null;
            }
            return entity;
        }
    }
}
