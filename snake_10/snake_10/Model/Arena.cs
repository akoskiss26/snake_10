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
        internal void KeyDown(KeyEventArgs e)
        {
            // a játék kezdetéhet a nyégy nyilbillenytyű egyikének lenyomása kell
            switch (e.Key)
            {
                
                case Key.Left:
                case Key.Up:
                case Key.Right:
                case Key.Down:
                    Console.WriteLine(e.Key);
                    break;
                
                
            }

        }
    }
}
