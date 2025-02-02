using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain
{
        [Serializable]
    public class Client : AbsEntity
    {
        [Browsable(false)]
        public int ClientID { get; set; }
        [Browsable(false)]
        public string FirstName { get; set; }
        [Browsable(false)]
        public string LastName { get; set; }
        public string Name
        {
            get => FirstName + " " + LastName;
        }
        public Gender Gender { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public Group Group { get; set; }
        public override string ToString()
        {
            return Name;
        }

        [Browsable(false)]
        public override string TableName => " Client ";

        public override string CheckAttribute(AbsEntity entity)
        {
            throw new NotImplementedException();
        }

        public override string CheckId(int key)
        {
            return $" Client.ClientID = {key} ";
        }

        public override string JoinKeys()
        {
            return " join [Group] on [Group].GroupID = Client.[Group] " +
                " join Coach on Coach.CoachID = [Group].Coach " +
                " join Education on Education.EducationID = Coach.Education ";
        }

        public override List<AbsEntity> ReaderRead(SqlDataReader reader)
        {
            List<Client> clients = new List<Client>();

            while (reader.Read())
            {
                Education education = new Education
                {
                    EducationID = (int)reader[14],
                    Qualifications = reader[15].ToString(),
                };

                Coach coach = new Coach
                {
                    CoachID = (int)reader[10],
                    FirstName = reader[11].ToString(),
                    LastName = reader[12].ToString(),
                    Education = education,
                };

                Group group = new Group
                {
                    GroupID = (int)reader[7],
                    Coach = coach,
                    GroupName = reader[9].ToString(),
                };

                Client client = new Client
                {
                    ClientID = (int)reader[0],
                    FirstName = reader[1].ToString(),
                    LastName = reader[2].ToString(),
                    Gender = (Gender)reader[3],
                    Height = (int)reader[4],
                    Weight = (int)reader[5],
                    Group = group,
                };

                clients.Add(client);
            }

            return clients.ConvertAll(x => (AbsEntity)x);
        }

        public override string Search(string criteria)
        {
            return $" Client.[Group] = {criteria} ";
        }

        public override string ValuesToInsert(AbsEntity entity)
        {
            Client client = (Client)entity;

            return $" '{client.FirstName}', '{client.LastName}', '{(int)client.Gender}', '{client.Height}', '{client.Weight}', '{client.Group.GroupID}' ";

        }

        public override string ValuesToSet(AbsEntity entity)
        {
            Client client = (Client)entity;
            return $" FirstName = '{client.FirstName}', LastName = '{client.LastName}', Gender = '{(int)client.Gender}', Height = '{client.Height}', Weight = '{client.Weight}', [Group] = '{client.Group.GroupID}' ";
        }

        public override string GetKey()
        {
            return ClientID + "";
        }
    }
}
