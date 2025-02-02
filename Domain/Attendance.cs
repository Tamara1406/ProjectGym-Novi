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
    public class Attendance : AbsEntity
    {
        public Client Client { get; set; }
        public Appointment Appointment { get; set; }
        public bool IsAttend { get; set; }
        [Browsable(false)]
        public override string TableName => " Attendance ";

        public override string CheckAttribute(AbsEntity entity)
        {
            throw new NotImplementedException();
        }

        public override string CheckId(int key)
        {
            return $" Attendance.Client = {key} ";
        }

        public override string GetKey()
        {
            throw new NotImplementedException();
        }

        public override string JoinKeys()
        {
            return " join Client on Client.ClientID = Attendance.Client " +
                " join [Group] on [Group].GroupID = Client.[Group] " +
                " join Coach on Coach.CoachID = [Group].Coach " +
                " join Education on Education.EducationID = Coach.Education " +
            " join Appointment on Appointment.AppointmentID = Attendance.Appointment ";
        }

        public override List<AbsEntity> ReaderRead(SqlDataReader reader)
        {
            List<Attendance> attendances = new List<Attendance>();

            while (reader.Read())
            {
                Education education2 = new Education
                {
                    EducationID = (int)reader[17],
                    Qualifications = reader[18].ToString(),
                };

                Coach coach2 = new Coach
                {
                    CoachID = (int)reader[13],
                    FirstName = reader[14].ToString(),
                    LastName = reader[15].ToString(),
                    Education = education2,
                };

                Group group2 = new Group
                {
                    GroupID = (int)reader[10],
                    Coach = coach2,
                    GroupName = reader[12].ToString(),
                };

                Appointment appointment = new Appointment
                {
                    AppointmentID = (int)reader[19],
                    NumberOfAppointments = (int)reader[20],
                    Group = group2,
                    Time = (DateTime)reader[22],
                };

                Education education = new Education
                {
                    EducationID = (int)reader[17],
                    Qualifications = reader[18].ToString(),
                };

                Coach coach = new Coach
                {
                    CoachID = (int)reader[13],
                    FirstName = reader[14].ToString(),
                    LastName = reader[15].ToString(),
                    Education = education,
                };

                Group group = new Group
                {
                    GroupID = (int)reader[10],
                    Coach = coach,
                    GroupName = reader[12].ToString(),
                };

                Client client = new Client
                {
                    ClientID = (int)reader[3],
                    FirstName = reader[4].ToString(),
                    LastName = reader[5].ToString(),
                    Gender = (Gender)reader[6],
                    Height = (int)reader[7],
                    Weight = (int)reader[8],
                    Group = group
                };

                Attendance attendance = new Attendance
                {
                    Client = client,
                    Appointment = appointment,
                    IsAttend = (bool)reader[2],
                };

                attendances.Add(attendance);
            }

            return attendances.ConvertAll(x => (AbsEntity)x);
        }

        public override string Search(string criteria)
        {
            throw new NotImplementedException();
        }

        public override string ValuesToInsert(AbsEntity entity)
        {
            Attendance attendance = (Attendance)entity;

            return $" '{attendance.Client.ClientID}', '{attendance.Appointment.AppointmentID}', '{attendance.IsAttend}' ";

        }

        public override string ValuesToSet(AbsEntity entity)
        {
            throw new NotImplementedException();
        }

        
    }
}
