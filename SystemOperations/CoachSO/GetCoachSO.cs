using Domain;

namespace SystemOperations.CoachSO
{
    public class GetCoachSO : BaseSO
    {
        Coach coach;
        public GetCoachSO(Coach coach)
        {
            this.coach = coach;
        }
        protected override void ExecuteConcreteOperation()
        {
            Result = repository.Get(coach, coach.CoachID);
        }
    }
}
