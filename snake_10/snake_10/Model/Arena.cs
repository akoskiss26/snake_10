using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace snake_10.Model
{
    /// <summary>
    /// Osztálydefiníció
    /// A játékmenet logikáját tartalmazza
    /// </summary>
    class Arena
    {
        private MainWindow View;

        /// <summary>
        /// konstruktorfgv egy paraméterrel
        /// </summary>
        /// <param name="view">az ablak ami létrehozta az Arena példányát</param>
        public Arena(MainWindow view)
        {
            this.View = view;
            View.GamePlayBorder.Visibility = System.Windows.Visibility.Visible;
        }

        internal void KeyDown(KeyEventArgs e)
        {
            // a játék kezdetéhet a négy nyilbillenytyű egyikének lenyomása kell
            switch (e.Key)
            {
                
                case Key.Left:
                case Key.Up:
                case Key.Right:
                case Key.Down:
                    View.GamePlayBorder.Visibility = System.Windows.Visibility.Hidden;
                    Console.WriteLine(e.Key);
                    break;

                    
                
                
            }

        }
    }
}
