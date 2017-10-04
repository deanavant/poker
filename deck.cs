using System;
using System.Collections.Generic;

namespace cards{
    public class Deck{
        public static string[] cardvals = {"Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King"};
        public static string[] suitvals = {"Clubs", "Diamonds", "Hearts", "Spades"};
        List<Card> cards;
        public Deck(){
            reset();
        }

        public void reset(){
            cards = new List<Card>();
            for(int i = 0;i < suitvals.Length;i++){
                for(int j = 0;j < cardvals.Length;j++){
                    cards.Add(new Card(cardvals[j],suitvals[i],j+ 1));
                }
            }
        }

        public Card deal(){
            Card acard = cards[0];
            cards.RemoveAt(0);
            return acard;
        }

        public void shuffle(){
            Random rand = new Random();
            for(int i = cards.Count - 1;i > 0;i--){
                int index = rand.Next(0,i);
                Card temp = cards[i];
                cards[i] = cards[index];
                cards[index] = temp;
            }
        }

        public void show(){
            foreach(Card c in this.cards){
                Console.WriteLine("{0} of {1}, value: {2}",c.stringVal,c.suit,c.val);
            }
        }
    }
}