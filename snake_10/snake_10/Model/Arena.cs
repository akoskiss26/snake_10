using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
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
        private Random Random;
        private Foods Foods;
        private int Decrement;
        private int DecrementStep; // ennyivel csökken a taktjel periódusideje msec
        private int NumberOfEaten;
        private int Speed;


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
            //példányosítunk egy random generátort
            Random = new Random();
            //példányosítunk egy étel osztályt
            Foods = new Foods();

            

            isGameStarted = false;
            RowCount = 20;
            ColumnCount = 20;
            Decrement = 0;
            DecrementStep = 20;
            NumberOfEaten = 0;

        }

        private void PendulumStart(int numberOfEaten)
        {
            if (pendulum != null && pendulum.IsEnabled)
            {
                pendulum.Stop();
            }

            int speed = 500 - numberOfEaten * DecrementStep;
            pendulum = new DispatcherTimer(TimeSpan.FromMilliseconds(speed),DispatcherPriority.Normal, ItsTimeToDisplay, Dispatcher.CurrentDispatcher);
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

            if (Snake.HeadPosition.RowPosition < 0 || Snake.HeadPosition.RowPosition > RowCount - 1
                || Snake.HeadPosition.ColumnPosition < 0 || Snake.HeadPosition.ColumnPosition > ColumnCount - 1)
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


            //testtel ütközés 2. (levél PG-nek)
            //if (Snake.Body.Any(x => x == Snake.HeadPosition))
            //{
            //    EndOfGame();
            //    Console.WriteLine("saj fark har");
            //}




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

            //van-e evés esemény

            //ütközött-e a fej elemózsiával
            var food = Foods.FoodPositions[0];
            if (Snake.HeadPosition.RowPosition == food.RowPosition && Snake.HeadPosition.ColumnPosition == food.ColumnPosition )
            {
                Eating(food.RowPosition, food.ColumnPosition);
            }
            

        }



        private void Eating(int rowPosition, int columnPosition)
        {
            Console.WriteLine("evés van");
           // Foods.FoodPositions.RemoveAt(0);
            var foodToDelete = Foods.Remove(rowPosition, columnPosition);
            EraseFromCanvas(foodToDelete.Paint);
            Foods.FoodPositions.RemoveAt(0);
            GetFood();
            Snake.Lenght = Snake.Lenght + 1;
            NumberOfEaten = NumberOfEaten + 1;
            PendulumStart(NumberOfEaten);

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
                case IconEnum.Food:
                    image.Icon = FontAwesome.WPF.FontAwesomeIcon.Apple;
                    image.Foreground = Brushes.Red;
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
            PendulumStart(NumberOfEaten);



            //kezdő elemózsia helye:
            // Foods.FoodPositions.Add(new ArenaPosition(randomRow, randomColumn));
            //fenti helyett:
            // Foods.Add(randomRow, randomColumn);

            GetFood();

        }






        private void GetFood()
        {
            int randomRow = Random.Next(RowCount);
            int randomColumn = Random.Next(ColumnCount);
            while ((Snake.HeadPosition.RowPosition == randomRow && Snake.HeadPosition.ColumnPosition == randomColumn
                || Snake.Body.Any(x => x.RowPosition == randomRow && x.ColumnPosition == randomColumn)))
            {
                randomRow = Random.Next(RowCount);
                randomColumn = Random.Next(ColumnCount);
            }

            var paint = PaintOnCanvas(randomRow, randomColumn);

            Foods.Add(randomRow, randomColumn, paint);
            ShowSnakeHead(randomRow, randomColumn, IconEnum.Food);
        }


        /// <summary>
        /// törli a paraméterben megadott elemet a Canvasról
        /// </summary>
        /// <param name="paint"></param>
        private void EraseFromCanvas(UIElement paint)
        {
            View.ArenaCanvas.Children.Remove(paint);
        }


        /// <summary>
        /// rajzol egy elemet a Canvas-ra
        /// </summary>
        /// <param name="randomRow"></param>
        /// <param name="randomColumn"></param>
        /// <returns>a felrajzolt elem, amit aztán törlünk</returns>
        private UIElement PaintOnCanvas(int randomRow, int randomColumn)
        {
            var paint = new Ellipse();
            paint.Height = View.ArenaCanvas.ActualHeight / RowCount;
            paint.Width = View.ArenaCanvas.ActualWidth / ColumnCount;
            Canvas.SetTop(paint, randomRow * paint.Height);
            Canvas.SetLeft(paint, randomColumn * paint.Width);
            paint.Fill = Brushes.Red;
            View.ArenaCanvas.Children.Add(paint);
            return paint;
        }
    }
}
