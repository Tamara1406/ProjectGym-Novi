using CommunicationClasses;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using SystemOperations;
using SystemOperations.AppointmentSO;
using SystemOperations.Attendance;
using SystemOperations.ClientSO;
using SystemOperations.CoachSO;
using SystemOperations.EducationSO;
using SystemOperations.GroupSO;
using SystemOperations.UserSO;

namespace Server
{
    public class ServerController
    {
        private static ServerController instance;
        public static ServerController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServerController();
                }
                return instance;
            }
        }
        public ServerController()
        {
        }

        BaseSO operation;
        public bool Login(User user, BaseSO operation)
        {
            //operation = new GetAllUsersSO();
            this.operation = operation;
            this.operation.ExecuteOperation();
            List<User> users = operation.ResultList.ConvertAll(x => (User)x);

            foreach (User checkUser in users)
            {
                if (user.Username == checkUser.Username && user.Password == checkUser.Password)
                {
                    return true;
                }
            }
            return false;
        }

        public bool RegisterUser(User newUser, BaseSO operation)
        {
            //operation = new CreateAccountSO(newUser);
            if (newUser.Username == null || newUser.Username.Length == 0 ||
                newUser.Password == null || newUser.Password.Length == 0 ||
                newUser.FirstName == null || newUser.FirstName.Length == 0 ||
                newUser.LastName == null || newUser.LastName.Length == 0 ||
                newUser.Email == null || newUser.Email.Length == 0)
            {
                return false;
            }
            else
            {
                this.operation = operation;
                this.operation.ExecuteOperation();
                return true;
            }
        }

        public User GetUserByUsername(User user, BaseSO operation)
        {
            //operation = new GetUserByUsernameSO(user);
            this.operation = operation;
            this.operation.ExecuteOperation();
            return (User)operation.Result;
        }

        public bool UpdateUser(User userToUpdate, BaseSO operation)
        {

            //operation = new UpdateAccountSO(userToUpdate);
            if (userToUpdate.Username == null || userToUpdate.Username.Length == 0 ||
                userToUpdate.Password == null || userToUpdate.Password.Length == 0 ||
                userToUpdate.FirstName == null || userToUpdate.FirstName.Length == 0 ||
                userToUpdate.LastName == null || userToUpdate.LastName.Length == 0 ||
                userToUpdate.Email == null || userToUpdate.Email.Length == 0)
            {
                return false;
            }
            else
            {
                this.operation = operation;
                this.operation.ExecuteOperation();
                return true;
            }
        }

        public List<User> GetAllUsers(BaseSO operation)
        {
            //operation = new GetAllUsersSO();
            this.operation = operation;
            this.operation.ExecuteOperation();
            return operation.ResultList.ConvertAll(x => (User)x);
        }
        
        public List<Coach> GetAllCoaches(BaseSO operation)
        {
            //operation = new GetAllCoachesSO();
            this.operation = operation;
            this.operation.ExecuteOperation();
            List<Coach> coaches = operation.ResultList.ConvertAll(x => (Coach)x);
            return coaches;
        }

        public List<Coach> GetAllCoachByEducation(BaseSO operation)
        {
            //operation = new GetAllCoachesSO();
            this.operation = operation;
            this.operation.ExecuteOperation();
            List<Coach> coaches = operation.ResultList.ConvertAll(x => (Coach)x);
            return coaches;
        }

        public Coach GetCoach(BaseSO operation)
        {
            //operation = new GetCoachSO();
            this.operation = operation;
            this.operation.ExecuteOperation();
            
            return (Coach)operation.Result;
        }

        public List<Education> GetAllEducations(BaseSO operation)
        {
            //operation = new GetAllEducationsSO();
            this.operation = operation;
            this.operation.ExecuteOperation();
            List<Education> education = operation.ResultList.ConvertAll(x => (Education)x);
            return education;
        }

        public Coach DeleteCoach(Coach resToDelete, BaseSO operation)
        {
            if (operation.Result == null)
                throw new Exception("Ne postoji trener u bazi!");

            //operation = new DeleteCoachSO(resToDelete);
            this.operation = operation;
            this.operation.ExecuteOperation();
            return (Coach)operation.Result;
        }

        public List<Group> GetAllGroups(BaseSO operation)
        {
            //operation = new GetAllGroupsSO();
            this.operation = operation;
            this.operation.ExecuteOperation();
            List<Group> groups = operation.ResultList.ConvertAll(x => (Group)x);
            return groups;
        }

        public bool UpdateCoach(Coach resToUpdate, BaseSO operation)
        {
            //operation = new UpdateCoachSO(resToUpdate);
            if (resToUpdate.FirstName == null || resToUpdate.FirstName.Length == 0 ||
                resToUpdate.LastName == null || resToUpdate.LastName.Length == 0 ||
                resToUpdate.Education == null)
            {
                return false;
            }
            else
            {
                this.operation = operation;
                this.operation.ExecuteOperation();
                return true;
            }
        }

        public bool CreateCoach(Coach coach, BaseSO operation)
        {
            //operation = new CreateCoachSO(coach);
            if (coach.FirstName == null || coach.FirstName.Length == 0 ||
            coach.LastName == null || coach.LastName.Length == 0 ||
            coach.Education == null)
            {
                return false;
            }
            else
            {
                this.operation = operation;
                this.operation.ExecuteOperation();
                return true;
            }
        }
        
        public List<Client> GetAllClients(BaseSO operation)
        {
            //operation = new GetAllClientsSO();
            this.operation = operation;
            this.operation.ExecuteOperation();
            List<Client> clients = operation.ResultList.ConvertAll(x => (Client)x);
            return clients;
        }

        public List<Client> GetAllClientsByGroup(BaseSO operation)
        {
            //operation = new GetAllClientsByGroupSO();
            this.operation = operation;
            this.operation.ExecuteOperation();
            List<Client> clients = operation.ResultList.ConvertAll(x => (Client)x);
            return clients;
        }

        public Client GetClient(BaseSO operation)
        {
            //operation = new GetCoachSO();
            this.operation = operation;
            this.operation.ExecuteOperation();

            return (Client)operation.Result;
        }

        public Client DeleteClient(Client clientToDelete, BaseSO operation)
        {
            if (operation.Result == null)
                throw new Exception("Ne postoji trener u bazi!");

            //operation = new DeleteClientSO(clientToDelete);
            this.operation = operation;
            this.operation.ExecuteOperation();
            return (Client)operation.Result;
        }

        public bool UpdateClient (Client clientToUpdate, BaseSO operation)
        {
            //operation = new UpdateClientSO(clientToUpdate);
            if (clientToUpdate.FirstName == null || clientToUpdate.FirstName.Length == 0 ||
                clientToUpdate.LastName == null || clientToUpdate.LastName.Length == 0 ||
                clientToUpdate.Weight == 0 || clientToUpdate.Height == 0 ||
                clientToUpdate.Group == null
                )
            {
                return false;
            }
            else
            {
                this.operation = operation;
                this.operation.ExecuteOperation();
                return true;
            }
        }

        public bool CreateClient(Client clientToUpdate, BaseSO operation)
        {
            //operation = new CreateClientSO(client);
            if (clientToUpdate.FirstName == null || clientToUpdate.FirstName.Length == 0 ||
                clientToUpdate.LastName == null || clientToUpdate.LastName.Length == 0 ||
                clientToUpdate.Weight == 0 || clientToUpdate.Height == 0 ||
                clientToUpdate.Group == null
                )
            {
                return false;
            }
            else
            {
                this.operation = operation;
                this.operation.ExecuteOperation();
                return true;
            }
        }

        public bool CreateAppointment(Appointment appointment, BaseSO operation)
        {
            //operation = new CreateAppointmentSO(appointment);
            if (appointment.NumberOfAppointments == 0 ||
                appointment.Group == null
                )
            {
                return false;
            }
            else
            {
                this.operation = operation;
                this.operation.ExecuteOperation();
                return true;
            }
        }

        public List<Appointment> GetAllAppointments(BaseSO operation)
        {
            //operation = new GetAllAppointmentsSO();
            this.operation = operation;
            this.operation.ExecuteOperation();
            List<Appointment> appointments = operation.ResultList.ConvertAll(x => (Appointment)x);
            return appointments;
        }

        public Appointment DeleteAppointment(Appointment appointmentToDelete, BaseSO operation)
        {
            //operation = new DeleteAppointmentSO(appointmentToDelete);
            if (operation.Result == null)
                throw new Exception("Ne postoji termin u bazi!");

            this.operation = operation;
            this.operation.ExecuteOperation();
            return (Appointment)operation.Result;
        }

        public bool UpdateAppointment(Appointment appToUpdate, BaseSO operation)
        {
            //operation = new UpdateAppointmentSO(appToUpdate);
            if (appToUpdate.NumberOfAppointments == 0 ||
                appToUpdate.Group == null
               )
            {
                return false;
            }
            else
            {
                this.operation = operation;
                this.operation.ExecuteOperation();
                return true;
            }
        }

        public List<Attendance> GetAllAttendances(BaseSO operation)
        {
            //operation = new GetAllAttendanceSO();
            this.operation = operation;
            this.operation.ExecuteOperation();
            List<Attendance> attendances = operation.ResultList.ConvertAll(x => (Attendance)x);
            return attendances;
        }

        public bool CreateGroup(Group group, BaseSO operation)
        {
            //operation = new CreateGroupSO(group);
            if (group.GroupName == null || group.GroupName.Length == 0 ||
                group.Coach == null
                )
            {
                return false;
            }
            else
            {
                this.operation = operation;
                this.operation.ExecuteOperation();
                return true;
            }
        }

        public bool CreateAttendances(List<Attendance> attendances, BaseSO operation)
        {
            //operation = new CreateAttendancesSO(attendances);
            foreach(Attendance attendance in attendances)
            {
                if (attendance.Client == null ||
                attendance.Appointment == null
                )
                {
                    return false;
                }
            }
            this.operation = operation;
            this.operation.ExecuteOperation();
            return true;
        }

        public Attendance DeleteAttendance(Attendance attendanceToDelete, BaseSO operation)
        {
            //operation = new DeleteAttendanceSO(attendanceToDelete);
            if (operation.Result == null)
                throw new Exception("Ne postoji prisustvo u bazi!");

            this.operation = operation;
            this.operation.ExecuteOperation();
            return (Attendance)operation.Result;
        }

    }
}
