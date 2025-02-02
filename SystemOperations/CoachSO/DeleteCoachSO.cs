using Domain;

namespace SystemOperations.CoachSO
{
    public class DeleteCoachSO : BaseSO
    {
        Coach coach;
        public DeleteCoachSO(Coach coach)
        {
            this.coach = coach;
        }
        protected override void ExecuteConcreteOperation()
        {
            Result = repository.Delete(coach, coach.CoachID);
        }
    }
}
