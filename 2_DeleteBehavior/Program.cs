namespace _2_DeleteBehavior
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EntityDatabase obj = new EntityDatabase();
            obj.SaveChanges();


            Team team = new Team { Name="Team1"};///ініціалізатор об'єкту, ініт по властивостям (get;set;)
        }
    }
}
