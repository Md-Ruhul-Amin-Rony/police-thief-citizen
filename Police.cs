using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenPoliceAndThief
{
    class Police : Person
    {


        public static bool InPrison = false;
        public List<Inventory> RecoveredGoods = new List<Inventory>();
        public static List<Thief> ThiefsInPrizon = new List<Thief>();

        public Police(int x, int y, int xDirection, int yDirection, string type, int personID)
        {
            X = x;
            Y = y;
            XDirection = xDirection;
            YDirection = yDirection;
            Type = type;
            PersonID = personID;

        }







    }
}
