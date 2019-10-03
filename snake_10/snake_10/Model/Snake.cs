using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_10.Model
{
    class Snake
    {
        public Snake(int rowPosition, int columnPosition)
        {
            HeadPosition = new ArenaPosition(rowPosition, columnPosition);
            HeadDirection = SnakeHeadDirerctionEnum.InPlace;
            Body = new List<ArenaPosition>();  //object reference null hiba ellen!
            Lenght = 6;
        }

        //hol van a kígyó feje
        public ArenaPosition HeadPosition { get; set; }

        //a kígyó teste
        public List<ArenaPosition> Body { get; set; }


        //merre megy
        public SnakeHeadDirerctionEnum HeadDirection { get; set; }

        //milyen hosszú
        public int Lenght { get; set; }
    }
}
