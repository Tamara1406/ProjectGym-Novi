using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class Coach : AbsEntity
    {
        [Browsable(false)]
        public int CoachID { get; set; }
        [Browsable(false)]
        public string FirstName { get; set; }
        [Browsable(false)]
        public string LastName { get; set; }

        public string Name
        {
            get => FirstName + " " + LastName;
        }
        public Education Education { get; set; }

        public override string ToString()
        {
            return Name;
        }

        [Browsable(false)]
        public override string TableName => " Coach ";

        public override string CheckAttribute(AbsEntity entity)
        {
            throw new NotImplementedException();
        }

        public override string CheckId(int key)
        {
            return $" Coach.CoachID = {key} ";
        }

        public override string JoinKeys()
        {
            return " join Education on Education.EducationID = Coach.Education ";
        }

        public override List<AbsEntity> ReaderRead(SqlDataReader reader)
        {
            List<Coach> coaches = new List<Coach>();

            while (reader.Read())
            {

                Education education = new Education
                {
                    EducationID = (int)reader[4],
                    Qualifications = reader[5].ToString(),
                };


                Coach coach = new Coach
                {
                    CoachID = (int)reader[0],
                    FirstName = reader[1].ToString(),
                    LastName = reader[2].ToString(),
                    Education = education,
                };

                coaches.Add(coach);
            }

            return coaches.ConvertAll(x => (AbsEntity)x);
        }

        public override string Search(string criteria)
        {
            return $" Coach.Education = {criteria} ";
        }

        public override string ValuesToInsert(AbsEntity entity)
        {
            Coach coach = (Coach)entity;

            return $" '{coach.FirstName}', '{coach.LastName}', '{coach.Education.EducationID}' ";

        }

        public override string ValuesToSet(AbsEntity entity)
        {
            Coach coach = (Coach)entity;
            return $" FirstName = '{coach.FirstName}', LastName = '{coach.LastName}', Education = '{coach.Education.EducationID}'";
        }

        public override string GetKey()
        {
            return CoachID + "";
        }
    }
}
