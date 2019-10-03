using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
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
        private ArenaPosition CurrentPosition;
        private int RowCount;
        private int ColumnCount;
        

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
            RowCount = 20;
            ColumnCount = 20;
          
        }



        private void ItsTimeToDisplay(object sender, EventArgs e)
        {

            if (!isGameStarted)
            {
                return;
            }
            //megjegyezzük a fej pozicióját
            CurrentPosition = new ArenaPosition(Snake.HeadPosition.RowPosition, Snake.HeadPosition.ColumnPosition);

            //a fej új poziciója:
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

            //falnak ütközés detektálása:

            if (Snake.HeadPosition.RowPosition < 1 || Snake.HeadPosition.RowPosition > RowCount - 1
                || Snake.HeadPosition.ColumnPosition < 1 || Snake.HeadPosition.ColumnPosition > ColumnCount - 1)
            {
                EndOfGame();
                return;
            }

            //testtel ütközés detektálása:
            if (Snake.Body.Any(x => x.RowPosition == Snake.HeadPosition.RowPosition
                                 && x.ColumnPosition == Snake.HeadPosition.ColumnPosition))
            {
                EndOfGame();
                Console.WriteLine("saját farkába harapott");
            }

            ShowSnakeHead(Snake.HeadPosition.RowPosition, Snake.HeadPosition.ColumnPosition, IconEnum.Head);

            // a régi fejből test lett, belerakjuk a Body listába:
            Snake.Body.Add(new ArenaPosition(CurrentPosition.RowPosition, CurrentPosition.ColumnPosition));

            // töröljük a régi fejet, helytte test lesz:
            ShowSnakeHead(CurrentPosition.RowPosition, CurrentPosition.ColumnPosition, IconEnum.Body);

            if (Snake.Body.Count > Snake.Lenght)
            {
                var end = Snake.Body[0];
                ShowSnakeHead(end.RowPosition, end.ColumnPosition, IconEnum.Empty);
                Snake.Body.RemoveAt(0);
                
            }

        }

        private void EndOfGame()
        {
            pendulum.Stop();
            Console.WriteLine("End of Game");
        }

        private void ShowSnakeHead(int rowPosition, int columnPosition, IconEnum icon)
        {
            var cell = View.ArenaGrid.Children[rowPosition * 20 + columnPosition];
            var image = (FontAwesome.WPF.ImageAwesome)cell;
            switch (icon)
            {
                case IconEnum.Head:
                    image.Icon = FontAwesome.WPF.FontAwesomeIcon.CircleOutline;
                    break;
                case IconEnum.Body:
                    image.Icon = FontAwesome.WPF.FontAwesomeIcon.CircleOutline;
                    image.Foreground = Brushes.Gray;
                    break;
                case IconEnum.Empty:
                    image.Icon = FontAwesome.WPF.FontAwesomeIcon.SquareOutline;
                    image.Foreground = Brushes.Black;
                    break;
                
            }
            
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
            isGameStarted = true;
            
            
            
        }
    }
}
