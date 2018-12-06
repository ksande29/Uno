using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno2
{
    public class GameV2
    {
        private Deck deck;
        private Card lastDiscardCard;
        private Player[] players;
        private int currentTurnIndex = 0;

        private Card emptyCard = new Card(Color.None, Symbol.None);

        private int marginTop = 1;
        private int marginLeft = 10;

        // - - main game methods - - - - - - - - - 
        /// <summary>
        /// Plays an uno game
        /// </summary>
        public void playGame()
        {
            SetUpGame();
            while (!HasWin())
            {
                TakeTurn(); 
                if (HasUno())
                    SayUno();
                if (HasWin())
                {
                    WinGame();
                }
                else
                {
                    ToggleTurn();
                    PrintTurn();
                }
            }
        }

        /// <summary>
        /// Sets up a new uno game by creating a Deck, the players, and picking the first discard
        /// </summary>
        public void SetUpGame()
        {
            deck = new Deck();
            players = new Player[] { new Player(deck, 1), new Player(deck, 2), new Player(deck, 3), new Player(deck, 4) };
            lastDiscardCard = deck.Draw();
        }
        /// <summary>
        /// Allows a player to take a turn
        /// </summary>
        public void TakeTurn()
        {
            Console.Clear();
            PrintTitle();
            PrintDiscard();
            PrintTurn();

            Player player = players[currentTurnIndex];
            //print user's cards
            if (IsHumanPlayer())
                PrintPlayerDeck(player);

            //get index of card to discard
            int cardPlayedIndex = -1;
            if (IsHumanPlayer())
            {
                cardPlayedIndex = UserSelectCardNum();
            }
            else
            {
                System.Threading.Thread.Sleep(3000);
                cardPlayedIndex = ComputerSelectCardNum();
            }

            //get card from index
            Card cardPlayed = emptyCard;
            if (cardPlayedIndex == -1)
                cardPlayed = emptyCard;
            else
                cardPlayed = player.playerHand[cardPlayedIndex];

            //discard or draw a card
            if (!cardPlayed.Equals(emptyCard))
            {
                lastDiscardCard = cardPlayed;   //add card to discard pile
                PrintCardPlayed(cardPlayed);
                SpecialCards(cardPlayed);   //do special action
                player.playerHand[cardPlayedIndex] = emptyCard;     //remove card from player's card
            }
            else
            {
                //PrintDiscard();
                player.playerHand[player.CurrantValue] = player.DrawCard(deck); //draw a card  
                Console.CursorTop = marginTop + 7;
                Console.CursorLeft = marginLeft;
                Console.WriteLine("Player {0} drew 1 card", player.playerID);
                System.Threading.Thread.Sleep(3000);
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Checks if the current player is the human player or an ai
        /// </summary>
        /// <returns></returns>
        public bool IsHumanPlayer()
        {
            return players[currentTurnIndex].playerID == 1;
        }
        /// <summary>
        /// Allows the user to select a card to play
        /// </summary>
        /// <returns></returns>
        public int UserSelectCardNum()
        {
            Player player = players[currentTurnIndex];
            int CardPlayedIndex = -1;

            //get user input
            Console.CursorLeft = marginLeft;
            Console.Write("Select a card - enter a number or type 'draw': ");
            string input = Console.ReadLine();
            Console.WriteLine();
            if (int.TryParse(input, out CardPlayedIndex)
                && CardPlayedIndex > 0
                && CardPlayedIndex < player.playerHand.Length)
                CardPlayedIndex = int.Parse(input) - 1;    //-1 to get index of array
            if (input == "draw")
                CardPlayedIndex = -1;
            //TODO check that input is a valid move
            return CardPlayedIndex;
        }
        /// <summary>
        /// Selects which card the ai will play
        /// </summary>
        /// <returns></returns>
        public int ComputerSelectCardNum()
        {
            Player player = players[currentTurnIndex];
            int cardPlayedNum = -1;

            for (int i = 0; i < player.playerHand.Length; i++)
            {
                if (!player.playerHand[i].Equals(emptyCard))
                {
                    if ((player.playerHand[i].Color == lastDiscardCard.Color
                           || player.playerHand[i].Symbol == lastDiscardCard.Symbol
                           || player.playerHand[i].Symbol == Symbol.Wild
                           || player.playerHand[i].Symbol == Symbol.WildDraw4)
                           )
                    {
                        cardPlayedNum = i;
                        break;
                    }
                }
            }
            return cardPlayedNum;
        }
        /// <summary>
        /// Checks if a player has won the game
        /// </summary>
        /// <returns></returns>
        public bool HasWin()
        {
            return (RemainingCards() == 0);
        }
        /// <summary>
        /// Checks if a player has uno
        /// </summary>
        /// <returns></returns>
        public bool HasUno()
        {
            return (RemainingCards() == 1);
        }
        /// <summary>
        /// Checks how many cards are left in a player's hand
        /// </summary>
        /// <returns></returns>
        public int RemainingCards()
        {
            Player player = players[currentTurnIndex];
            int remainingCards = 0;
            for (int i = 0; i < player.playerHand.Length; i++)
            {
                if (!player.playerHand[i].Equals(emptyCard))
                    remainingCards++;
            }
            return remainingCards;
        }
        /// <summary>
        /// Allows a player to say uno
        /// </summary>
        public void SayUno()
        {
            Player player = players[currentTurnIndex];
            if (IsHumanPlayer())
            {
                Console.CursorLeft = marginLeft;
                Console.CursorTop = marginTop + 12;
                Console.Write("You are down to one card, say 'uno': ");
                string input = Console.ReadLine();
                if (input != "uno")
                {
                    Console.CursorLeft = marginLeft;
                    Console.CursorTop = marginTop + 10;
                    Console.WriteLine("You didn't say 'uno'!");
                    for (int i = 0; i < 2; i++)
                    {
                        player.playerHand[player.CurrantValue] = player.DrawCard(deck);
                    }
                }
            }
            else
            {
                Console.CursorLeft = marginLeft;
                Console.CursorTop = marginTop + 10;
                Console.WriteLine("uno! Player {0} has one card left!", player.playerID);
                Console.CursorLeft = marginLeft;
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Ends the game and displays who won
        /// </summary>
        public void WinGame()
        {
            Console.CursorLeft = marginLeft;
            Console.CursorTop = marginTop + 7;
            Console.WriteLine("Player {0} Wins!         ", currentTurnIndex + 1);
        }
        /// <summary>
        /// Changes who's turn it is
        /// </summary>
        public void ToggleTurn()
        {
            currentTurnIndex++;
            if (currentTurnIndex == players.Length)
                currentTurnIndex = 0;
        }

        // - - - - print methods - - - - -
        /// <summary>
        /// Prints the welcome message
        /// </summary>
        public void PrintTitle()
        {
            Console.CursorTop = marginTop;
            Console.CursorLeft = marginLeft;
            Console.WriteLine("Welcome to Uno!");
            Console.WriteLine();
        }
        /// <summary>
        /// Prints the last discarded card
        /// </summary>
        public void PrintDiscard()
        {
            Console.CursorTop = marginTop + 5;
            Console.CursorLeft = marginLeft;

            Console.Write("Last Discard: ");
            SetCardColor(lastDiscardCard.Color);
            Console.WriteLine("{0}          ", lastDiscardCard);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        /// <summary>
        /// Prints which card was 
        /// </summary>
        /// <param name="cardPlayed"></param>
        public void PrintCardPlayed(Card cardPlayed)
        {
            Console.CursorTop = marginTop + 7;
            Console.CursorLeft = marginLeft;
            Console.Write("Player {0} played ", players[currentTurnIndex].playerID);
            SetCardColor(cardPlayed.Color);
            Console.WriteLine("{0}",cardPlayed.Symbol);
            Console.ForegroundColor = ConsoleColor.Gray;
            System.Threading.Thread.Sleep(3000);
        }
        /// <summary>
        /// Prints who's turn it is
        /// </summary>
        public void PrintTurn()
        {
            Console.CursorTop = marginTop + 2;
            Console.CursorLeft = marginLeft;

            if (IsHumanPlayer())
            {
                Console.WriteLine("Your Turn Player 1");
                Console.CursorLeft = marginLeft;
                Console.WriteLine("You have {0} cards left", RemainingCards());
            }
            else
            {
                Console.WriteLine("Player {0}'s Turn", players[currentTurnIndex].playerID);
                Console.CursorLeft = marginLeft;
                Console.WriteLine("Player {0} has {1} cards left", players[currentTurnIndex].playerID, RemainingCards());
            }
            Console.WriteLine();
        }
        /// <summary>
        /// Prints the cards in the player's hand
        /// </summary>
        /// <param name="player"></param>
        public void PrintPlayerDeck(Player player)
        {
            Console.CursorTop = marginTop + 15;
            Console.CursorLeft = marginLeft;

            Console.WriteLine("Your cards: ");
            for (int i = 0; i < player.playerHand.Length; i++)
            {
                if (!player.playerHand[i].Equals(emptyCard))
                {
                    Console.CursorLeft = marginLeft;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("card number {0}: ", i + 1);
                    SetCardColor(player.playerHand[i].Color);
                    Console.WriteLine("{0}  ", player.playerHand[i]);
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }
        /// <summary>
        /// Sets the console color to the card color
        /// </summary>
        /// <param name="color"></param>
        public void SetCardColor(Color color)
        {
            if (color == Color.Red)
                Console.ForegroundColor = ConsoleColor.Red;
            if (color == Color.Green)
                Console.ForegroundColor = ConsoleColor.Green;
            if (color == Color.Yellow)
                Console.ForegroundColor = ConsoleColor.Yellow;
            if (color == Color.Blue)
                Console.ForegroundColor = ConsoleColor.Blue;
            if (color == Color.Wild)
                Console.ForegroundColor = ConsoleColor.White;
        }

        // - - - - - Methods for card actions - - - - - - 
        /// <summary>
        /// Checks if the card played has a special value and calls the corresponding method
        /// </summary>
        /// <param name="cardPlayed"></param>
        public void SpecialCards(Card cardPlayed)
        {
            if (cardPlayed.Symbol == Symbol.Wild)
                Wild();
            if (cardPlayed.Symbol == Symbol.WildDraw4)
                WildDraw4();
            if (cardPlayed.Symbol == Symbol.Draw2)
                Draw2();
            if (cardPlayed.Symbol == Symbol.Reverse)
                Reverse();
            if (cardPlayed.Symbol == Symbol.Skip)
                Skip();
        }
        /// <summary>
        /// Allows the user to select what color and number they want the card to turn into
        /// </summary>
        public void Wild()
        {
            Player player = players[currentTurnIndex];
            if (IsHumanPlayer())
            {
                //set the value
                Console.CursorLeft = marginLeft;
                Console.CursorTop = marginTop + 8;
                Console.Write(" What color would you like? ('red', 'green', 'yellow', 'blue'): ");
                string input = Console.ReadLine();
                Color color;
                if (input == "red")
                    color = Color.Red;
                else if (input == "green")
                    color = Color.Green;
                else if (input == "yellow")
                    color = Color.Yellow;
                else if (input == "blue")
                    color = Color.Blue;
                else
                {
                    Console.WriteLine("entry not valid red selected as default");
                    color = Color.Red;
                }

                Console.CursorLeft = marginLeft;
                Console.CursorTop = marginTop + 9;
                Console.WriteLine(" What card number would you like? ('one', 'two', 'three', 'four', 'five',): ");
                Console.Write("\t\t'six', 'seven', 'eight', 'nine'): ");
                input = Console.ReadLine();
                Symbol symbol;
                if (input == "one")
                    symbol = Symbol.One;
                else if (input == "two")
                    symbol = Symbol.Two;
                else if (input == "three")
                    symbol = Symbol.Three;
                else if (input == "four")
                    symbol = Symbol.Four;
                else if (input == "five")
                    symbol = Symbol.Five;
                else if (input == "six")
                    symbol = Symbol.Six;
                else if (input == "seven")
                    symbol = Symbol.Seven;
                else if (input == "eight")
                    symbol = Symbol.Eight;
                else if (input == "nine")
                    symbol = Symbol.Nine;
                else
                {
                    Console.WriteLine("Value not recognized. Default value of 0 assigned");
                    symbol = Symbol.One;
                }

                lastDiscardCard = new Card(color, symbol);
            }
            else
                lastDiscardCard = new Card(Color.Red, Symbol.One);
            Console.CursorLeft = marginLeft;
            Console.Write("Wild turned into: ");
            SetCardColor(lastDiscardCard.Color);
            Console.WriteLine(lastDiscardCard);
            System.Threading.Thread.Sleep(3000);
            Console.ForegroundColor = ConsoleColor.Gray;
            PrintDiscard();
        }
        /// <summary>
        /// Allows the user to select what color and number they want the card to turn into and causes the 
        /// next player to draw 4 cards
        /// </summary>
        public void WildDraw4()
        {
            Player player;
            if (currentTurnIndex < players.Length - 1)
                player = players[currentTurnIndex + 1];
            else
                player = players[0];
            for (int i = 0; i < 4; i++)
            {
                player.playerHand[player.CurrantValue] = player.DrawCard(deck);
            }
            Wild();
            Console.CursorLeft = marginLeft;
            Console.CursorTop = marginTop + 13; 
            Console.WriteLine("Player {0} drew 4 cards", player.playerID);
            System.Threading.Thread.Sleep(3000);
            Console.CursorLeft = marginLeft;
            Console.WriteLine();
        }
        /// <summary>
        /// Causes the next player to draw 2 cards from the deck
        /// </summary>
        public void Draw2()
        {
            Player player;
            if (currentTurnIndex < players.Length - 1)
                player = players[currentTurnIndex + 1];
            else
                player = players[0];
            for (int i = 0; i < 2; i++)
            {
                player.playerHand[player.CurrantValue] = player.DrawCard(deck);
            }
            Console.CursorLeft = marginLeft;
            Console.CursorTop = marginTop + 13;
            Console.WriteLine("Player {0} drew 2 cards", player.playerID);
            System.Threading.Thread.Sleep(3000);
            Console.CursorLeft = marginLeft;
            Console.WriteLine();
        }
        /// <summary>
        /// Switches the direction of the turn
        /// </summary>
        public void Reverse()
        {
            Array.Reverse(players);
            if (currentTurnIndex == 0)
                currentTurnIndex = 3;
            else if (currentTurnIndex == 1)
                currentTurnIndex = 2;
            else if (currentTurnIndex == 2)
                currentTurnIndex = 1;
            else //current == 3
                currentTurnIndex = 0;
        }
        /// <summary>
        /// Skips the next player
        /// </summary>
        public void Skip()
        {
            if (currentTurnIndex == players.Length - 1)
                currentTurnIndex = 0;
            else
                currentTurnIndex++;
        }
    }
}

