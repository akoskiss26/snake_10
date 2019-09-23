using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace snake_10.Model
{
    /// <summary>
    /// Osztálydefiníció
    /// A játékmenet logikáját tartalmazza
    /// </summary>
    class Arena
    {
        private MainWindow View;
        private Snake Snake;
        private DispatcherTimer pendulum;
        private bool isGameStarted;

        /// <summary>
        /// konstruktorfgv egy paraméterrel
        /// </summary>
        /// <param name="view">az ablak ami létrehozta az Arena példányát</param>
        public Arena(MainWindow view)
        {
            this.View = view;
            View.GamePlayBorder.Visibility = System.Windows.Visibility.Visible;

            //példányosítunk egy snake-et
            Snake = new Snake(10, 10);

            pendulum = new DispatcherTimer(TimeSpan.FromMilliseconds(300), DispatcherPriority.Normal, ItsTimeToDisplay, Dispatcher.CurrentDispatcher);

            isGameStarted = false;
        }

        private void ItsTimeToDisplay(object sender, EventArgs e)
        {
            //irányítjuk a kígyó fejét:



            switch (Snake.HeadDirection)
            {
                case SnakeHeadDirerctionEnum.Up:
                    Snake.HeadPosition.RowPosition = Snake.HeadPosition.RowPosition - 1;
                    break;
                case SnakeHeadDirerctionEnum.Down:
                    Snake.HeadPosition.RowPosition = Snake.HeadPosition.RowPosition + 1;
                    break;
                case SnakeHeadDirerctionEnum.Left:
                    Snake.HeadPosition.ColumnPosition = Snake.HeadPosition.ColumnPosition - 1;
                    break;
                case SnakeHeadDirerctionEnum.Right:
                    Snake.HeadPosition.ColumnPosition = Snake.HeadPosition.ColumnPosition + 1;
                    break;
                case SnakeHeadDirerctionEnum.InPlace:
                    break;
                
            }


            var cell = View.ArenaGrid.Children[Snake.HeadPosition.RowPosition*20 + Snake.HeadPosition.ColumnPosition];
            var image = (FontAwesome.WPF.ImageAwesome)cell;
            image.Icon = FontAwesome.WPF.FontAwesomeIcon.CircleOutline;
        }

        internal void KeyDown(KeyEventArgs e)
        {
            // a játék kezdetéhet a négy nyilbillenytyű egyikének lenyomása kell
            if (!isGameStarted)
            {
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

            switch (e.Key)
            {
                case Key.Left:
                    Snake.HeadDirection = SnakeHeadDirerctionEnum.Left;
                    break;
                case Key.Up:
                    Snake.HeadDirection = SnakeHeadDirerctionEnum.Up;
                    break;
                case Key.Right:
                    Snake.HeadDirection = SnakeHeadDirerctionEnum.Right;
                    break;
                case Key.Down:
                    Snake.HeadDirection = SnakeHeadDirerctionEnum.Down;
                    break;
            }

        }

        private void StartOfGame()
        {
            View.GamePlayBorder.Visibility = System.Windows.Visibility.Hidden;
            
            
            
        }
    }
}
