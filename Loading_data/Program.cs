using Microsoft.EntityFrameworkCore;

namespace Loading_data
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ////Як заповнити таблиці данними
          //  GenerateData();


            /////////Як зчитувати данні 
            ///
            ///////////////////////////////////////////////////////////////
            //  Eager loading жадная загрузка
            

            //using (EntityDatabase db = new EntityDatabase())
            //{
            //    Team? team = db.Teams
            //        .Include(t => t.Players)
            //            .Include(p => p.Stadium).FirstOrDefault();
                    

            //    if (team != null)
            //    {
            //        Console.WriteLine($"Team: {team.Name}  Stadium: {team.Stadium.Name}");
            //        foreach (var p in team.Players)
            //            Console.WriteLine($"Player: {p.Name}    Country: {p.Country?.Name}    ");
            //    }
            //}

            //using (EntityDatabase db = new EntityDatabase())
            //{
            //    Team? team = db.Teams
            //        .Include(t => t.Players)
            //            .ThenInclude(p => p.Country)
            //        .FirstOrDefault();

            //    if (team != null)
            //    {
            //        Console.WriteLine($"Team: {team.Name}");
            //        foreach (var p in team.Players)
            //            Console.WriteLine($"Player: {p.Name}    Country: {p.Country?.Name}");
            //    }
            //}

            using (EntityDatabase db = new EntityDatabase())
            {
                Team? team = db.Teams
                    .Include(t => t.Players).ThenInclude(p => p.Country).ThenInclude(c => c.Capital)
                        .Include(p => p.Stadium)
                    .FirstOrDefault();

                if (team != null)
                {
                    Console.WriteLine($"Team: {team.Name}  Stadium: {team.Stadium.Name}");
                    foreach (var p in team.Players)
                        Console.WriteLine($"Player: {p.Name,-30}  Country: {p.Country?.Name,-20}  City: {p.Country.Capital.Name,-20}   ");
                }
            }


            ////////////////////////////////////////////////////////////
            ////// Explicite loading (явная загрузка)

            //using (EntityDatabase db = new EntityDatabase())
            //{
            //    var team = db.Teams.FirstOrDefault();
            //    db.Players.Where(p => p.Team.Id == team.Id).Load();

            //    Console.WriteLine($"Team: {team?.Name}");

            //    foreach (var p in team.Players)
            //        Console.WriteLine($"Player: {p.Name}");
            //}

            //using (EntityDatabase db = new EntityDatabase())
            //{
            //    var team = db.Teams.FirstOrDefault();
            //    db.Players.Where(p => p.Team.Id == team.Id).Load();

            //    Console.WriteLine($"Team: {team?.Name}");

            //    foreach (var p in team.Players)
            //        Console.WriteLine($"Player: {p.Name}");
            //}

            //using (EntityDatabase db = new EntityDatabase())
            //{
            //    var team = db.Teams.FirstOrDefault();
            //    db.Entry(team).Collection(t => t.Players).Load();

            //    Console.WriteLine($"Team: {team?.Name}");

            //    foreach (var p in team.Players)
            //        Console.WriteLine($"Player: {p.Name}");
            //}



            //using (EntityDatabase db = new EntityDatabase())
            //{
            //    var player = db.Players.FirstOrDefault();
            //    db.Entry(player).Reference(t => t.Team).Load();

            //    Console.WriteLine($"Team: {player.Team.Name}");


            //    Console.WriteLine($"Player: {p.Name}");
            //}


            Console.ReadLine();
        }


        /// <summary>
        /// //Заповнюємо базу  данними
        /// </summary>
        public static void GenerateData()
        {
            using (EntityDatabase db = new EntityDatabase())
            {
                var listItem = new List<Team>
                {
                    new Team(){Name= "Аланта Хукс", Stadium =new Stadium(){ Name="Филипс-Арена (Атланта б США)"} },
                    new Team(){Name= "Бостон Селикс", Stadium =new Stadium(){ Name="Ти-Ди Гарден (Бостон, США)"} },
                    new Team(){Name= "Бруклин Нетс", Stadium =new Stadium(){ Name="Барклайс-центр (Нью-Йорк, США)"} }



                };

                db.Teams.AddRange(listItem);
                db.SaveChanges();

                var listCountry = new List<Country>()
                {
                    new Country() {Name="США",Capital=new City(){Name="Вашингтон"}},
                    new Country() {Name="Германия",Capital=new City(){Name="Берлин"}},
                    new Country() {Name="Македония",Capital=new City(){Name="Скопье"}}
                };


                db.Countries.AddRange(listCountry);
                db.SaveChanges();


                var listPlayer = new List<Player>()
                {
                    new Player(){Name="Джон Дженкинс",TeamId=1, CountryId=1},
                     new Player(){Name="Кайл Корвер",TeamId=1, CountryId=1},
                     new Player(){Name="Демарре Кэрролл",TeamId=1, CountryId=1},
                     new Player(){Name="Кулвин Мак",TeamId=1, CountryId=1},
                     new Player(){Name="Джефф Тиг",TeamId=1, CountryId=1},
                      new Player(){Name="Лу Тайрон Вильямс",TeamId=1, CountryId=1},
                      new Player(){Name="Дженнис Шрейдер",TeamId=1, CountryId=2},
                      new Player(){Name="Перро Антич",TeamId=1, CountryId=3},
                      new Player(){Name="Майк Мускала",TeamId=1, CountryId=1},
                      new Player(){Name="Дексер Питмэн",TeamId=1, CountryId=2},

                };

                db.Players.AddRange(listPlayer);
                db.SaveChanges();


            }




        }
    }

}
