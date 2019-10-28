using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace snake_10.Model
{
    class Foods
    {
        public Foods()
        {
            FoodPositions = new List<CanvasPosition>();
        }
        public List<CanvasPosition> FoodPositions { get; set; }

        internal void Add(int randomRow, int randomColumn, UIElement paint)
        {
            FoodPositions.Add(new CanvasPosition(randomRow, randomColumn, paint));
        }


        /// <summary>
        /// egy étel törlése a Canvasról 
        /// </summary>
        /// <param name="rowPosition"></param>
        /// <param name="columnPosition"></param>
        /// <returns> a törölt étellel tér vissza</returns>
        internal CanvasPosition Remove(int rowPosition, int columnPosition)
        {
            var foodToDelete = FoodPositions.Single(x => x.RowPosition == rowPosition && x.ColumnPosition == columnPosition);
            return foodToDelete;
            
           
        }
    }
}
