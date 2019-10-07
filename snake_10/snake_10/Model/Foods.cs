using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_10.Model
{
    class Foods
    {
        public Foods()
        {
            FoodPositions = new List<ArenaPosition>();
        }
        public List<ArenaPosition> FoodPositions { get; set; }

        internal void Add(int randomRow, int randomColumn)
        {
            FoodPositions.Add(new ArenaPosition(randomRow, randomColumn));
        }
    }
}
