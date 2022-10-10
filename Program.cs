using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CitizenPoliceAndThief
{
   
    class Program
    {
       
        static void Main(string[] args)
        {
            int ArrestedThieves = 0;
            int ThiefRobbedCitizens = 0;
            int timeCounter = 0;
            Random rand = new Random();
            Console.SetWindowSize(100, 25);
            List<Person> persons = AddPersonToCity();
           


            foreach (var person in persons)
            {
                if (person.Type == "Citizen")
                {
                    ((Citizen)person).Assets.Add(new Inventory("Watch"));
                    ((Citizen)person).Assets.Add(new Inventory("Money"));
                    ((Citizen)person).Assets.Add(new Inventory("Keys"));
                    ((Citizen)person).Assets.Add(new Inventory("Mobile"));
                }
            }
            

            while (true)
            {
                
                foreach (var person in persons)
                {
                   

                    if (person.X < 0 || person.Y < 0||person.X>Console.WindowWidth || person.Y>Console.WindowHeight )
                    {
                        person.X=rand.Next(4,99);
                        person.Y= rand.Next(2,25);
                    }
                        if (person is Citizen citizen)
                        {
                            Console.SetCursorPosition(person.X, person.Y);
                            Console.Write("C");

                        }
                        if (person is Police police)
                        {
                            Console.SetCursorPosition(person.X, person.Y);
                            Console.Write("P");

                        }
                        if (person is Thief thief)
                        {
                            Console.SetCursorPosition(person.X, person.Y);
                            Console.Write("T");


                        }
            

                }


                Console.SetCursorPosition(0, 30);
                Console.WriteLine($"Number of robbed citizens: {ThiefRobbedCitizens}\nNumber of thieves in prison: {Police.ThiefsInPrizon.Count} ");
                Console.WriteLine($"timeCounter= {timeCounter}");
                
                foreach (var curentPerson in persons)
                {

                    foreach (var person in persons)
                    {

                        if (curentPerson.PersonID != person.PersonID)
                        {
                            if ((curentPerson.Type == "Citizen" && person.Type == "Thief") ||
                            (curentPerson.Type == "Thief" && person.Type == "Citizen") ||
                            (curentPerson.Type == "Thief" && person.Type == "Police") ||
                            (curentPerson.Type == "Police" && person.Type == "Thief"))
                            {

                                if (curentPerson.X == person.X && curentPerson.Y == person.Y)
                                {
                                    if (curentPerson.Type == "Thief" && person.Type == "Police")
                                    {
                                       
                                        Console.WriteLine("Police caught a Thief.");
                                        ((Police)person).RecoveredGoods = ((Thief)curentPerson).StolenGoods;
                                        ((Thief)curentPerson).StolenGoods.Clear();
                                        ArrestedThieves++;
                                        Police.ThiefsInPrizon.Add((Thief)curentPerson);
                                        //persons.Remove(((Thief)curentPerson));
                                        Police.InPrison = true;
                                        Thread.Sleep(1000);
                                        if (Police.InPrison == true)
                                        {
                                            timeCounter++;
                                        }

                                      





                                    }
                                   //else if (curentPerson.Type == "Police" && person.Type == "Thief")
                                   // {
                                   //     //Console.SetCursorPosition(0, 30);
                                   //     Console.WriteLine("Police caught a Thief.......2");
                                   //     ((Police)curentPerson).RecoveredGoods = ((Thief)person).StolenGoods;
                                   //     ((Thief)person).StolenGoods.Clear();
                                   //     Thread.Sleep(1000);
                                   // }
                                    if (curentPerson.Type == "Citizen" && person.Type == "Thief")
                                    {
                                       
                                        Console.WriteLine("Thief stole goods form Citizen.");  
                                        ((Thief)person).StolenGoods = ((Citizen)curentPerson).Assets;
                                        ThiefRobbedCitizens++;
                                        Thread.Sleep(1000);
                                        if (Police.InPrison == true)
                                        {
                                            timeCounter++;
                                           
                                        }
                                       

                                    }
                                   //else if (curentPerson.Type == "Thief" && person.Type == "Citizen")
                                   // {
                                   //     //Console.SetCursorPosition(0, 30);
                                   //     Console.WriteLine("Thief stole gods form Citizen.......4");
                                   //     ((Thief)curentPerson).StolenGoods = ((Citizen)person).Assets;
                                   //     Thread.Sleep(1000);
                                   // }

                                }


                            }

                            if (timeCounter >= 10 )
                            {
                                timeCounter = 0;
                                Console.WriteLine("One Theif is free:");
                                Police.ThiefsInPrizon.RemoveAt(0);
                                if (Police.ThiefsInPrizon.Count == 0)
                                {
                                    Police.InPrison = false;

                                }
                                

                            }
                        }
                    }
                }
                Thread.Sleep(1000);
                if (Police.InPrison == true)
                {
                    timeCounter++;

                }
                foreach (var person in persons)
                {
                    person.X += person.XDirection;
                    person.Y += person.YDirection;
                    
                }

                Console.Clear();
               
               




            }

        }

        public static List<Person> AddPersonToCity()
        {

            Random rand = new Random();
            List<Person> persons = new List<Person>
            {
             new Citizen (rand.Next(1, 100), rand.Next(1, 20),rand.Next(1,2),rand.Next(-1,2),"Citizen",1),
             new Citizen (rand.Next(1, 100), rand.Next(1, 20),rand.Next(1,2),rand.Next(-1,2),"Citizen",2),
             new Citizen(rand.Next(1, 100), rand.Next(1, 20),rand.Next(1,2),rand.Next(-1,2),"Citizen",3),
             new Citizen(rand.Next(1, 100), rand.Next(1, 20),rand.Next(1,2),rand.Next(-1,2),"Citizen",4),
             new Citizen(rand.Next(1, 100), rand.Next(1, 20),rand.Next(1,2),rand.Next(-1,2),"Citizen",5),
             new Citizen(rand.Next(1, 100), rand.Next(1, 20),rand.Next(-1,2),rand.Next(1,2),"Citizen",6),
             new Citizen(rand.Next(1, 90), rand.Next(1, 20),rand.Next(1,2),rand.Next(-1,2),"Citizen",7),
             new Thief(rand.Next(2, 90), rand.Next(2, 20),rand.Next(-1,2),rand.Next(1,2),"Thief",8),
             new Thief(rand.Next(2, 90), rand.Next(2, 20),rand.Next(1,2),rand.Next(-1,2),"Thief",9),
             new Thief(rand.Next(2, 90), rand.Next(2, 20),rand.Next(1,2),rand.Next(-1,2),"Thief",10),
             new Thief(rand.Next(2, 90), rand.Next(2, 20),rand.Next(-1,2),rand.Next(-1,2),"Thief",11),
             new Thief(rand.Next(2, 90), rand.Next(2, 20),rand.Next(-1,2),rand.Next(1,2),"Thief",12),
             new Thief(rand.Next(2, 90), rand.Next(2, 20),rand.Next(1,2),rand.Next(-1,2),"Thief",13),
             new Thief(rand.Next(2, 90), rand.Next(2, 20),rand.Next(-1,2),rand.Next(1,2),"Thief",14),
             new Police(rand.Next(4, 90), rand.Next(3, 25),rand.Next(-1,2),rand.Next(1,2),"Police",15),
             new Police(rand.Next(4, 90), rand.Next(3, 25),rand.Next(-1,2),rand.Next(1,2),"Police",16),
             new Police(rand.Next(4, 90), rand.Next(3, 25),rand.Next(1,2),rand.Next(-1,2),"Police",17),
             new Police(rand.Next(4, 100), rand.Next(3, 25),rand.Next(-1,2),rand.Next(1,2),"Police",18),
             new Police(rand.Next(4, 100), rand.Next(3, 25),rand.Next(1,2),rand.Next(-1,2),"Police",19),
             new Police(rand.Next(4, 100), rand.Next(3, 22),rand.Next(-1,2),rand.Next(1,2),"Police",20),
             new Police(rand.Next(4, 100), rand.Next(3, 21),rand.Next(1,2),rand.Next(1,2),"Police",21),
             new Police(rand.Next(4, 100), rand.Next(3, 25),rand.Next(-1,2),rand.Next(1,2),"Police",22),
             new Police(rand.Next(0, 90), rand.Next(3, 25),rand.Next(-1,2),rand.Next(1,2),"Police",23),
             new Thief(rand.Next(10, 100), rand.Next(2, 25),rand.Next(-1,2),rand.Next(-1,2),"Thief",24),
             new Thief(rand.Next(2, 99), rand.Next(2, 20),rand.Next(-1,2),rand.Next(-1,2),"Thief",25),
             new Thief(rand.Next(2, 100), rand.Next(2, 20),rand.Next(-1,2),rand.Next(-1,2),"Thief",26),
             new Thief(rand.Next(8, 90), rand.Next(2, 20),rand.Next(-1,2),rand.Next(-1,2),"Thief",27),
             new Thief(rand.Next(2, 100), rand.Next(2, 20),rand.Next(-1,2),rand.Next(-1,2),"Thief",28),
             new Thief(rand.Next(72, 90), rand.Next(2, 20),rand.Next(-1,2),rand.Next(-1,2),"Thief",29),
             new Police(rand.Next(4, 90), rand.Next(3, 20),rand.Next(-1,2),rand.Next(1,2),"Police",30),
             new Police(rand.Next(4, 97), rand.Next(3, 20),rand.Next(-1,2),rand.Next(1,2),"Police",31),
             new Police(rand.Next(44, 100), rand.Next(3, 20),rand.Next(-1,2),rand.Next(1,2),"Police",32),
             new Police(rand.Next(4, 90), rand.Next(3, 20),rand.Next(-1,2),rand.Next(1,2),"Police",33),
             new Police(rand.Next(14, 90), rand.Next(3, 20),rand.Next(-1,2),rand.Next(1,2),"Police",34),
             new Police(rand.Next(4, 90), rand.Next(3, 20),rand.Next(-1,2),rand.Next(1,2),"Police",35),
             new Police(rand.Next(50, 100), rand.Next(3, 20),rand.Next(-1,2),rand.Next(1,2),"Police",36),
             new Citizen (rand.Next(1, 90), rand.Next(1, 25),rand.Next(1,2),rand.Next(-1,2),"Citizen",37),
             new Citizen (rand.Next(1, 90), rand.Next(1, 25),rand.Next(1,2),rand.Next(-1,2),"Citizen",38),
             new Citizen (rand.Next(1, 90), rand.Next(1, 25),rand.Next(1,2),rand.Next(-1,2),"Citizen",39),
             new Citizen (rand.Next(1, 90), rand.Next(1, 25),rand.Next(1,2),rand.Next(-1,2),"Citizen",40),
             new Citizen (rand.Next(1, 100), rand.Next(1, 23),rand.Next(1,2),rand.Next(-1,2),"Citizen",41),
             new Citizen (rand.Next(1, 100), rand.Next(1, 23),rand.Next(-1,2),rand.Next(-1,2),"Citizen",42),
             new Citizen (rand.Next(1, 90), rand.Next(1, 22),rand.Next(1,2),rand.Next(-1,2),"Citizen",43),
             new Citizen (rand.Next(1, 100), rand.Next(1, 21),rand.Next(1,2),rand.Next(-1,2),"Citizen",44),
             new Citizen (rand.Next(1, 100), rand.Next(1, 22),rand.Next(1,2),rand.Next(-1,2),"Citizen",45),
             new Citizen (rand.Next(1, 100), rand.Next(1, 20),rand.Next(-1,2),rand.Next(-1,2),"Citizen",46),
             new Citizen (rand.Next(1, 90), rand.Next(1, 20),rand.Next(1,2),rand.Next(-1,2),"Citizen",47),
             new Citizen (rand.Next(1, 100), rand.Next(1, 20),rand.Next(1,2),rand.Next(-1,2),"Citizen",48),
             new Citizen (rand.Next(1, 90), rand.Next(1, 20),rand.Next(1,2),rand.Next(-1,2),"Citizen",49),
             new Citizen (rand.Next(1, 90), rand.Next(1, 20),rand.Next(-1,2),rand.Next(-1,2),"Citizen",50),
             new Citizen (rand.Next(1, 90), rand.Next(1, 20),rand.Next(1,2),rand.Next(-1,2),"Citizen",51),
             new Citizen (rand.Next(1, 100), rand.Next(1, 20),rand.Next(-1,2),rand.Next(-1,2),"Citizen",52),
             new Thief(rand.Next(0, 90), rand.Next(2, 25),rand.Next(1,2),rand.Next(-1,2),"Thief",53),
             new Police(rand.Next(4, 90), rand.Next(3, 25),rand.Next(-1,2),rand.Next(-1,2),"Police",54),
             new Thief(rand.Next(72, 100), rand.Next(9, 25),rand.Next(-1,2),rand.Next(1,2),"Thief",55),
             new Police(rand.Next(4, 90), rand.Next(3, 25),rand.Next(-1,2),rand.Next(1,2),"Police",56),
             new Thief(rand.Next(72, 90), rand.Next(11, 20),rand.Next(1,2),rand.Next(-1,2),"Thief",57),
             new Police(rand.Next(4, 90), rand.Next(0, 20),rand.Next(1,2),rand.Next(-1,2),"Police",58),
             new Thief(rand.Next(72, 100), rand.Next(4, 20),rand.Next(1,2),rand.Next(-1,2),"Thief",59),
             new Police(rand.Next(4, 100), rand.Next(9, 20),rand.Next(-1,2),rand.Next(1,2),"Police",60),
             new Thief(rand.Next(72, 90), rand.Next(7, 20),rand.Next(-1,2),rand.Next(1,2),"Thief",61),
             new Police(rand.Next(4, 90), rand.Next(6, 20),rand.Next(1,2),rand.Next(1,2),"Police",62),
             new Citizen(rand.Next(1, 100), rand.Next(1, 25),rand.Next(1,2),rand.Next(-1,2),"Citizen",63),
             new Citizen(rand.Next(1, 100), rand.Next(9, 25),rand.Next(1,2),rand.Next(-1,2),"Citizen",64),
             new Citizen(rand.Next(1, 100), rand.Next(1, 25),rand.Next(1,2),rand.Next(-1,2),"Citizen",65),
             new Citizen(rand.Next(1, 100), rand.Next(1, 22),rand.Next(1,2),rand.Next(-1,2),"Citizen",66),
             new Citizen(rand.Next(1, 100), rand.Next(1, 19),rand.Next(1,2),rand.Next(-1,2),"Citizen",67),
             new Citizen(rand.Next(1, 100), rand.Next(1, 23),rand.Next(1,2),rand.Next(-1,2),"Citizen",68),
             new Citizen(rand.Next(1, 100), rand.Next(1, 21),rand.Next(1,2),rand.Next(-1,2),"Citizen",69),
             new Citizen(rand.Next(1, 100), rand.Next(1, 26),rand.Next(1,2),rand.Next(-1,2),"Citizen",70)

             };
            return persons;
        }

    }

   


}
