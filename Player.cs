using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno2
{
    public class Player
    {
        public Card[] playerHand;
        public int CurrantValue { get; set; }
        public int playerID { get; }

        /// <summary>
        /// Constructor method to make a new Player object which requires a Deck and a player ID as an int
        /// </summary>
        /// <param name="deck"></param>
        /// <param name="id"></param>
        public Player(Deck deck, int id)
        {
            playerHand = new Card[50];
            for (int i = 0; i < 7; i++)
            {
                playerHand[i] = DrawCard(deck);
            }
            CurrantValue = 7;

            playerID = id;
        }

        /// <summary>
        /// Returns the current card from the player's hand of cards
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        public Card DrawCard(Deck deck)
        {
            Card card = deck.Draw();
            CurrantValue++;
            return card;
        }
    }
}
