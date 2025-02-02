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
    public class Group : AbsEntity
    {
        [Browsable(false)]
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public Coach Coach { get; set; }

        public override string ToString()
        {
            return GroupName;
        }

        public override string TableName => " [Group] ";

        public override string CheckAttribute(AbsEntity entity)
        {
            throw new NotImplementedException();
        }

        public override string CheckId(int key)
        {
            return $" [Group].GroupID = {key} ";
        }

        public override string JoinKeys()
        {
            return " join Coach on Coach.CoachID = [Group].Coach " +
                    " join Education on Education.EducationID = Coach.Education ";
        }

        public override List<AbsEntity> ReaderRead(SqlDataReader reader)
        {
            List<Group> groups = new List<Group>();

            while (reader.Read())
            {

                Education education = new Education
                {
                    EducationID = (int)reader[7],
                    Qualifications = reader[8].ToString(),
                };

                Coach coach = new Coach
                {
                    CoachID = (int)reader[3],
                    FirstName = reader[4].ToString(),
                    LastName = reader[5].ToString(),
                    Education = education,
                };

                Group group = new Group
                {
                    GroupID = (int)reader[0],
                    Coach = coach,
                    GroupName = (string)reader[2],
                };

                groups.Add(group);
            }

            return groups.ConvertAll(x => (AbsEntity)x);
        }

        public override string Search(string criteria)
        {
            throw new NotImplementedException();
        }

        public override string ValuesToInsert(AbsEntity entity)
        {
            Group group = (Group)entity;

            return $" '{group.Coach.CoachID}', '{group.GroupName}' ";

        }

        public override string ValuesToSet(AbsEntity entity)
        {
            Group group = (Group)entity;
            return $" Coach = '{group.Coach.CoachID}', GroupName = '{group.GroupName}' ";

        }

        public override string GetKey()
        {
            return GroupID + "";
        }
    }
}
