using CommunicationClasses;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ClientController
    {
        public ClientCommunication Communication { get; set; }

        private static ClientController instance;
        public static ClientController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ClientController();
                }
                return instance;
            }
        }
        public int LoginClient(User user)
        {
            Package request = new Package
            {
                Item = user,
                Operation = Operation.LoginClient
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();

            if (response.Operation == Operation.LoginOk)
            {
                return 1;
            }
            else if (response.Operation == Operation.AlreadyLogged)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        public User GetUserByUsername(User userToFind)
        {
            Package request = new Package
            {
                Operation = Operation.GetUserByUsername,
                Item = userToFind
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();

            return (User)response.Item;
        }

        public bool CreateAccount(User user)
        {
            Package request = new Package
            {
                Operation = Operation.RegisterUser,
                Item = user
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();

            if (response.Operation == Operation.RegisterOk)
            {
                return true;
            }
            return false;
        }

        public bool SaveAccount(User user)
        {
            ValidatorClient validator = new ValidatorClient();

            validator.CheckUserData(user);

            Package request = new Package
            {
                Item = user,
                Operation = Operation.UpdateUser
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();

            if (response.Operation == Operation.UserUpdateOk)
            {
                return true;
            }
            return false;
        }

        public virtual List<Coach> GetAllCoaches()
        {
            Package request = new Package
            {
                Operation = Operation.GetAllCoaches
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();
            List<Coach> coaches = response.ItemList.ConvertAll(x => (Coach)x);

            return coaches;
        }

        public Coach GetCoach(Coach coach)
        {
            Package request = new Package
            {
                Item = coach,
                Operation = Operation.GetCoach
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();
            return (Coach)response.Item;
        }

        public List<Coach> GetCoachSearchedByName(string searchStr)
        {
            List<Coach> coaches = this.GetAllCoaches();

            List<Coach> finalCoaches = new List<Coach>();

            searchStr = searchStr.ToLower();

            foreach (Coach coach in coaches)
            {
                string name = coach.Name.ToLower();

                if (name.Contains(searchStr))
                {
                    finalCoaches.Add(coach);
                }
            }

            return finalCoaches;
        }

        public List<Education> GetAllEducations()
        {
            Package request = new Package
            {
                Operation = Operation.GetAllEducations
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();

            List<Education> educations = response.ItemList.ConvertAll(x => (Education)x);

            return educations;
        }

        public List<Coach> GetAllCoachByEducation(Education education)
        {
            Package request = new Package
            {
                Operation = Operation.GetAllCoachByEducation,
                Item = education
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();

            List<Coach> coachesByEducation = response.ItemList.ConvertAll(x => (Coach)x);

            return coachesByEducation;
        }

        public Coach DeleteCoach(Coach coach)
        {
            Package request = new Package
            {
                Item = coach,
                Operation = Operation.DeleteCoach
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();
            return (Coach)response.Item;
        }

        public List<Group> GetAllGroups()
        {
            Package request = new Package
            {
                Operation = Operation.GetAllGroups
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();
            List<Group> group = response.ItemList.ConvertAll(x => (Group)x);

            return group;
        }

        public bool UpdateCoach(Coach coach)
        {
            Package package = new Package
            {
                Operation = Operation.UpdateCoach,
                Item = coach
            };

            Communication.SendRequest(package);

            Package response = Communication.RecieveResponse();

            if (response.Operation == Operation.UpdateCoachOk)
            {
                return true;
            }

            return false;
        }

        public bool CreateCoach(Coach coach)
        {
            Package request = new Package
            {
                Operation = Operation.AddCoach,
                Item = coach
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();

            if (response.Operation == Operation.AddCoachOk)
            {
                return true;
            }
            return false;
        }

        public virtual List<Domain.Client> GetAllClients()
        {
            Package request = new Package
            {
                Operation = Operation.GetAllClients
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();
            List<Domain.Client> clients = response.ItemList.ConvertAll(x => (Domain.Client)x);

            return clients;
        }

        public Domain.Client GetClient(Domain.Client client)
        {
            Package request = new Package
            {
                Item = client,
                Operation = Operation.GetClient
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();
            return (Domain.Client)response.Item;
        }

        public List<Domain.Client> GetClientSearchedByName(string searchStr)
        {
            List<Domain.Client> clients = this.GetAllClients();

            List<Domain.Client> finalClients = new List<Domain.Client>();

            searchStr = searchStr.ToLower();

            foreach (Domain.Client client in clients)
            {
                string name = client.Name.ToLower();

                if (name.Contains(searchStr))
                {
                    finalClients.Add(client);
                }
            }

            return finalClients;
        }

        public List<Domain.Client> GetAllClientsByGroup(Group group)
        {
            Package request = new Package
            {
                Operation = Operation.GetAllClientsByGroup,
                Item = group
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();

            List<Domain.Client> clientsByGroup = response.ItemList.ConvertAll(x => (Domain.Client)x);

            return clientsByGroup;
        }

        public Domain.Client DeleteClient(Domain.Client client)
        {
            Package request = new Package
            {
                Item = client,
                Operation = Operation.DeleteClient
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();
            return (Domain.Client)response.Item;
        }

        public bool UpdateClient(Domain.Client client)
        {
            Package package = new Package
            {
                Operation = Operation.UpdateClient,
                Item = client
            };

            Communication.SendRequest(package);

            Package response = Communication.RecieveResponse();

            if (response.Operation == Operation.UpdateClientOk)
            {
                return true;
            }

            return false;
        }

        public bool CreateClient(Domain.Client client)
        {
            Package request = new Package
            {
                Operation = Operation.AddClient,
                Item = client
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();

            if (response.Operation == Operation.AddClientOk)
            {
                return true;
            }
            return false;
        }

        public bool CreateAppointment(Appointment appointment)
        {
            
            Package request = new Package
            {
                Operation = Operation.AddAppointment,
                Item = appointment
        };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();

            if (response.Operation == Operation.AddAppointmentOk)
            {
                return true;
            }
            return false;
        }

        public List<Appointment> GetAllAppointments()
        {
            Package request = new Package
            {
                Operation = Operation.GetAllAppointments
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();
            List<Appointment> appointment = response.ItemList.ConvertAll(x => (Appointment)x);

            return appointment;
        }

        public List<Appointment> GetAllAppointmentsByGroup(Group group)
        {
            Package request = new Package
            {
                Operation = Operation.GetAllAppointments,
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();

            List<Appointment> appointments = response.ItemList.ConvertAll(x => (Appointment)x);

            List<Appointment> appointmentsByGroup = new List<Appointment>();

            foreach (Appointment appointment in appointments)
            {
                if (appointment.Group.GroupID == group.GroupID)
                {
                    appointmentsByGroup.Add(appointment);
                }
            }

            return appointmentsByGroup;
        }

        public Appointment DeleteAppointment(Appointment appointment)
        {
            Package request = new Package
            {
                Item = appointment,
                Operation = Operation.DeleteAppointment
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();
            return (Appointment)response.Item;
        }

        public List<Attendance> GetAllAttendances()
        {
            Package request = new Package
            {
                Operation = Operation.GetAllAttendances
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();
            List<Attendance> attendances = response.ItemList.ConvertAll(x => (Attendance)x);

            return attendances;
        }

        public bool CreateGroup(Group group)
        {
            Package request = new Package
            {
                Operation = Operation.AddGroup,
                Item = group
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();

            if (response.Operation == Operation.AddGroupOk)
            {
                return true;
            }
            return false;
        }

        public bool CreateAttendances(List<Attendance> attendances)
        {
            List<object> list = new List<object>();
            foreach (object attendance in attendances)
            {
                list.Add(attendance);
            }
            Package request = new Package
            {
                Operation = Operation.AddAttendances,
                ItemList = list
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();

            if (response.Operation == Operation.AddAttendancesOk)
            {
                return true;
            }
            return false;
        }

        public Attendance DeleteAttendance(Attendance attendance)
        {
            Package request = new Package
            {
                Item = attendance,
                Operation = Operation.DeleteAttendance
            };

            Communication.SendRequest(request);

            Package response = Communication.RecieveResponse();
            return (Attendance)response.Item;
        }


    }
}
