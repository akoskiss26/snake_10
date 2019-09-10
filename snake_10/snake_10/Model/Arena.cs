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
                    StartOfGame();
                    Console.WriteLine(e.Key);
                    break;

                
                
            }

        }

        private void StartOfGame()
        {
            View.GamePlayBorder.Visibility = System.Windows.Visibility.Hidden;
            var cell = View.ArenaGrid.Children[1];
            var image = (FontAwesome.WPF.ImageAwesome)cell;
            image.Icon = FontAwesome.WPF.FontAwesomeIcon.CircleOutline;
            
            
        }
    }
}
