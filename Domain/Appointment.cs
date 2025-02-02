using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain
{
    [Serializable]
    public class Appointment : AbsEntity
    {
        [Browsable(false)]
        public int AppointmentID { get; set; }
        public DateTime Time { get; set; }
        public int NumberOfAppointments { get; set; }
        public Group Group { get; set; }
        public override string ToString()
        {
            return Time + "h";
        }
        [Browsable(false)]
        public override string TableName => " Appointment ";

        public override string CheckAttribute(AbsEntity entity)
        {
            throw new NotImplementedException();
        }

        public override string CheckId(int key)
        {
            return $" Appointment.AppointmentID = {key} ";
        }

        public override string JoinKeys()
        {
            return "join [Group] on [Group].GroupID = [Appointment].[Group] " +
                " join Coach on Coach.CoachID = [Group].Coach " +
                " join Education on Education.EducationID = Coach.Education";
        }

        public override List<AbsEntity> ReaderRead(SqlDataReader reader)
        {
            List<Appointment> appointments = new List<Appointment>();

            while (reader.Read())
            {

                Education education = new Education
                {
                    EducationID = (int)reader[11],
                    Qualifications = reader[12].ToString(),
                };

                Coach coach = new Coach
                {
                    CoachID = (int)reader[7],
                    FirstName = reader[8].ToString(),
                    LastName = reader[9].ToString(),
                    Education = education,
                };

                Group group = new Group
                {
                    GroupID = (int)reader[4],
                    Coach = coach,
                    GroupName = (string)reader[6],
                };


                Appointment appointment = new Appointment
                {
                    AppointmentID = (int)reader[0],
                    NumberOfAppointments = (int)reader[1],
                    Group = group,
                    Time = (DateTime)reader[3],
                };

                appointments.Add(appointment);
            }

            return appointments.ConvertAll(x => (AbsEntity)x);
        }

        public override string Search(string criteria)
        {
            throw new NotImplementedException();
        }

        public override string ValuesToInsert(AbsEntity entity)
        {
            Appointment appointment = (Appointment)entity;

            return $" '{appointment.NumberOfAppointments}', '{appointment.Group.GroupID}', '{appointment.Time.ToString("yyyy-MM-dd")}' ";

        }

        public override string ValuesToSet(AbsEntity entity)
        {
            Appointment appointment = (Appointment)entity;
            return $" [Time] = '{appointment.Time}', NumberOfAppointments = '{appointment.NumberOfAppointments}', [Group] = '{appointment.Group.GroupID}' ";

        }

        public override string GetKey()
        {
            return AppointmentID + "";
        }
    }
}
