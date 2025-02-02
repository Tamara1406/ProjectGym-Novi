using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class Education : AbsEntity
    {
        [Browsable(false)]
        public int EducationID { get; set; }
        public string Qualifications { get; set; }
        public override string ToString()
        {
            return Qualifications;
        }
        [Browsable(false)]
        public override string TableName => " Education ";

        public override string CheckAttribute(AbsEntity entity)
        {
            throw new NotImplementedException();
        }

        public override string CheckId(int key)
        {
            throw new NotImplementedException();
        }

        public override string JoinKeys()
        {
            return "";
        }

        public override List<AbsEntity> ReaderRead(SqlDataReader reader)
        {
            List<AbsEntity> educations = new List<AbsEntity>();

            while (reader.Read())
            {
                Education education = new Education
                {
                    EducationID = (int)reader[0],
                    Qualifications = reader[1].ToString(),
               };

                educations.Add(education);
            }

            return educations;
        }

        public override string Search(string criteria)
        {
            throw new NotImplementedException();
        }

        public override string ValuesToInsert(AbsEntity entity)
        {
            throw new NotImplementedException();
        }

        public override string ValuesToSet(AbsEntity entity)
        {
            throw new NotImplementedException();
        }

        public override string GetKey()
        {
            return EducationID + "";
        }
    }
}
