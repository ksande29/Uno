using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno2
{
    public class Deck
    {
        private Card[] deck;
        private int currentCardNum = 0;

        /// <summary>
        /// Creates a new deck of uno cards and shuffles them
        /// </summary>
        public Deck()
        {
            deck = new Card[]
            {
                //red number cards
                new Card(Color.Red, Symbol.Zero),
                new Card(Color.Red, Symbol.One),
                new Card(Color.Red, Symbol.One),
                new Card(Color.Red, Symbol.Two),
                new Card(Color.Red, Symbol.Two),
                new Card(Color.Red, Symbol.Three),
                new Card(Color.Red, Symbol.Three),
                new Card(Color.Red, Symbol.Four),
                new Card(Color.Red, Symbol.Four),
                new Card(Color.Red, Symbol.Five),
                new Card(Color.Red, Symbol.Five),
                new Card(Color.Red, Symbol.Six),
                new Card(Color.Red, Symbol.Six),
                new Card(Color.Red, Symbol.Seven),
                new Card(Color.Red, Symbol.Seven),
                new Card(Color.Red, Symbol.Eight),
                new Card(Color.Red, Symbol.Eight),
                new Card(Color.Red, Symbol.Nine),
                new Card(Color.Red, Symbol.Nine),
                //red special cards
                new Card(Color.Red, Symbol.Draw2),
                new Card(Color.Red, Symbol.Draw2),
                new Card(Color.Red, Symbol.Reverse),
                new Card(Color.Red, Symbol.Reverse),
                new Card(Color.Red, Symbol.Skip),
                new Card(Color.Red, Symbol.Skip),
                //green number cards
                new Card(Color.Green, Symbol.Zero),
                new Card(Color.Green, Symbol.One),
                new Card(Color.Green, Symbol.One),
                new Card(Color.Green, Symbol.Two),
                new Card(Color.Green, Symbol.Two),
                new Card(Color.Green, Symbol.Three),
                new Card(Color.Green, Symbol.Three),
                new Card(Color.Green, Symbol.Four),
                new Card(Color.Green, Symbol.Four),
                new Card(Color.Green, Symbol.Five),
                new Card(Color.Green, Symbol.Five),
                new Card(Color.Green, Symbol.Six),
                new Card(Color.Green, Symbol.Six),
                new Card(Color.Green, Symbol.Seven),
                new Card(Color.Green, Symbol.Seven),
                new Card(Color.Green, Symbol.Eight),
                new Card(Color.Green, Symbol.Eight),
                new Card(Color.Green, Symbol.Nine),
                new Card(Color.Green, Symbol.Nine),
                //green special cards
                new Card(Color.Green, Symbol.Draw2),
                new Card(Color.Green, Symbol.Draw2),
                new Card(Color.Green, Symbol.Reverse),
                new Card(Color.Green, Symbol.Reverse),
                new Card(Color.Green, Symbol.Skip),
                new Card(Color.Green, Symbol.Skip),
                //yellow number cards
                new Card(Color.Yellow, Symbol.Zero),
                new Card(Color.Yellow, Symbol.One),
                new Card(Color.Yellow, Symbol.One),
                new Card(Color.Yellow, Symbol.Two),
                new Card(Color.Yellow, Symbol.Two),
                new Card(Color.Yellow, Symbol.Three),
                new Card(Color.Yellow, Symbol.Three),
                new Card(Color.Yellow, Symbol.Four),
                new Card(Color.Yellow, Symbol.Four),
                new Card(Color.Yellow, Symbol.Five),
                new Card(Color.Yellow, Symbol.Five),
                new Card(Color.Yellow, Symbol.Six),
                new Card(Color.Yellow, Symbol.Six),
                new Card(Color.Yellow, Symbol.Seven),
                new Card(Color.Yellow, Symbol.Seven),
                new Card(Color.Yellow, Symbol.Eight),
                new Card(Color.Yellow, Symbol.Eight),
                new Card(Color.Yellow, Symbol.Nine),
                new Card(Color.Yellow, Symbol.Nine),
                //yellow special cards
                new Card(Color.Yellow, Symbol.Draw2),
                new Card(Color.Yellow, Symbol.Draw2),
                new Card(Color.Yellow, Symbol.Reverse),
                new Card(Color.Yellow, Symbol.Reverse),
                new Card(Color.Yellow, Symbol.Skip),
                new Card(Color.Yellow, Symbol.Skip),
                //blue number cards
                new Card(Color.Blue, Symbol.Zero),
                new Card(Color.Blue, Symbol.One),
                new Card(Color.Blue, Symbol.One),
                new Card(Color.Blue, Symbol.Two),
                new Card(Color.Blue, Symbol.Two),
                new Card(Color.Blue, Symbol.Three),
                new Card(Color.Blue, Symbol.Three),
                new Card(Color.Blue, Symbol.Four),
                new Card(Color.Blue, Symbol.Four),
                new Card(Color.Blue, Symbol.Five),
                new Card(Color.Blue, Symbol.Five),
                new Card(Color.Blue, Symbol.Six),
                new Card(Color.Blue, Symbol.Six),
                new Card(Color.Blue, Symbol.Seven),
                new Card(Color.Blue, Symbol.Seven),
                new Card(Color.Blue, Symbol.Eight),
                new Card(Color.Blue, Symbol.Eight),
                new Card(Color.Blue, Symbol.Nine),
                new Card(Color.Blue, Symbol.Nine),
                //blue special cards
                new Card(Color.Blue, Symbol.Draw2),
                new Card(Color.Blue, Symbol.Draw2),
                new Card(Color.Blue, Symbol.Reverse),
                new Card(Color.Blue, Symbol.Reverse),
                new Card(Color.Blue, Symbol.Skip),
                new Card(Color.Blue, Symbol.Skip),
                //wild cards
                new Card(Color.Wild, Symbol.Wild),
                new Card(Color.Wild, Symbol.Wild),
                new Card(Color.Wild, Symbol.Wild),
                new Card(Color.Wild, Symbol.Wild),
                new Card(Color.Wild, Symbol.WildDraw4),
                new Card(Color.Wild, Symbol.WildDraw4),
                new Card(Color.Wild, Symbol.WildDraw4),
                new Card(Color.Wild, Symbol.WildDraw4),
             };
            this.Shuffle();
        }

        /// <summary>
        /// Randomizes the Card order in the deck
        /// </summary>
        public void Shuffle()
        {
            Random rand = new Random();

            for (int i = 0; i < this.deck.Length; i++)
            {
                int randNum = rand.Next(0, this.deck.Length);
                Card temp = deck[i];
                deck[i] = deck[randNum];
                deck[randNum] = temp;
            }
            currentCardNum = 0;
        }

        /// <summary>
        /// Returns the top Card in the Deck
        /// </summary>
        /// <returns></returns>
        public Card Draw()
        {
            Card card = deck[currentCardNum];
            currentCardNum++;
            if (currentCardNum == deck.Length)
                currentCardNum = 0;
            return card;
        }

    }
}
