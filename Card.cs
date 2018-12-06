using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno2
{
    public enum Color { None, Red, Green, Yellow, Blue, Wild }
    public enum Symbol { None, Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Draw2, Reverse, Skip, Wild, WildDraw4 }

    public struct Card
    {
        public Color Color { get; }
        public Symbol Symbol { get; }

        /// <summary>
        /// Constructor to make a new Card struct which requires a color and symbol
        /// </summary>
        /// <param name="color"></param>
        /// <param name="symbol"></param>
        public Card(Color color, Symbol symbol)
        {
            Color = color;
            Symbol = symbol;
        }

        //prints out a card value
        public override string ToString()
        {
            return string.Format("{0}", Symbol);
        }
    }
}
