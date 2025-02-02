using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public abstract class AbsEntity
    {
        public abstract string TableName { get; }
        public abstract string CheckId(int key);
        public abstract string GetKey();
        public abstract string ValuesToInsert(AbsEntity entity);
        public abstract string ValuesToSet(AbsEntity entity);
        public abstract string JoinKeys();
        public abstract string Search(string criteria);
        public abstract List<AbsEntity> ReaderRead(SqlDataReader reader);
        public abstract string CheckAttribute(AbsEntity entity);
    }
}
