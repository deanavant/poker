using System;
using System.Linq;
using System.Collections.Generic;

namespace cards{
    public class Player{
        public string name;
        public List<Card> hand;

        public Player(string name){
            this.name = name;
            hand = new List<Card>();
        }

        public Card draw(Deck deck){
            Card temp = deck.deal();
            hand.Add(temp);
            return temp;
        }

        public Card discard(int index){
            if((hand.Count == 0) || (index < 0) || (index > hand.Count - 1)){
                return null;
            }
            Card temp = hand[index];
            hand.RemoveAt(index);
            return temp;
        }

        public void discardAll(){
            while(hand.Count != 0){
                hand.RemoveAt(0);
            }
        }

        public void showHand(){
            int ind=1;
            Console.WriteLine("{0} has:",name);
            foreach(Card card in hand)
            {
                Console.WriteLine("{0}. {1} of {2}",ind,card.stringVal,card.suit);
                ind=ind+1;
            }
        }

        private bool isRoyalFlush(Dictionary<string, int> myScore){
            return isStraightFlush(myScore) && hand[4].val == 13;
        }

        private bool isStraightFlush(Dictionary<string, int> myScore){
            return isStraight() && isFlush(myScore);
        }

        private bool isFourOfAKind(Dictionary<string, int> myScore){
            return (myScore.ContainsKey("four"));
        }
        private bool isFullHouse(Dictionary<string, int> myScore){
            return (myScore.ContainsKey("three") && myScore.ContainsKey("two") );
        }

        private bool isFlush(Dictionary<string, int> myScore){
            foreach(string key in Deck.suitvals) {
                if(myScore.ContainsKey(key) && myScore[key] == 5) {
                    // Console.WriteLine("{0} flush", key);
                    return true; }
            }
            return false;
        }

        private bool isStraight(){
            if (hand[0].val == 1){
                if (hand[1].val == 10){ // look for high straight
                    return (hand[1].val == hand[2].val - 1 &&
                            hand[2].val == hand[3].val - 1 &&
                            hand[3].val == hand[4].val - 1);
                }
                return (hand[1].val == 2 &&
                        hand[2].val == hand[3].val - 1 &&
                        hand[3].val == hand[4].val - 1);
            }
            return (hand[0].val == hand[1].val - 1 &&
                        hand[1].val == hand[2].val - 1 &&
                        hand[2].val == hand[3].val - 1 &&
                        hand[3].val == hand[4].val - 1);
        }

        private bool isThreeOfAKind(Dictionary<string, int> myScore){
            return (myScore.ContainsKey("three"));
        }

        private bool isTwoPair(Dictionary<string, int> myScore){
            return (myScore.ContainsKey("two") && myScore["two"] == 2);
        }
        private bool isPair(Dictionary<string, int> myScore){
            return (myScore.ContainsKey("two"));      
        }

        public bool mySort(){
            if(hand.Count == 0) { return false; }
            hand = hand.OrderBy(x => x.val).ToList();
            return true;
        }

        public int score(){
            Dictionary<string, int> myScore = new Dictionary<string, int>();
            int sum = 0;
            foreach(Card card in hand){
                if(myScore.ContainsKey(card.suit)){
                    myScore[card.suit]++;
                } else{
                    myScore[card.suit] = 1;
                }
                if(myScore.ContainsKey(card.stringVal)){
                    myScore[card.stringVal]++;
                } else {
                    myScore[card.stringVal] = 1;
                }
            }

            foreach(string key in Deck.cardvals){
                if(myScore.ContainsKey(key) && myScore[key] == 4){
                    myScore["four"] = 1;
                }
                if(myScore.ContainsKey(key) && myScore[key] == 3){
                    myScore["three"] = 1;
                    // Console.WriteLine("three of a kind {0}", key);
                }
                if(myScore.ContainsKey(key) && myScore[key] == 2){
                    if(myScore.ContainsKey("two")){
                        myScore["two"]++;
                        if (key == "Ace"){
                            myScore["p2"] = 14;
                        } else if (key == "King"){
                            myScore["p2"] = 13;
                        } else if (key == "Queen"){
                            myScore["p2"] = 12;
                        } else if (key == "Jack"){
                            myScore["p2"] = 11;
                        } else {
                            myScore["p2"] = Int32.Parse(key);
                        }
                    } else {
                        myScore["two"] = 1;
                        if (key == "Ace"){
                            myScore["p1"] = 14;
                        } else if (key == "King"){
                            myScore["p1"] = 13;
                        } else if (key == "Queen"){
                            myScore["p1"] = 12;
                        } else if (key == "Jack"){
                            myScore["p1"] = 11;
                        } else {
                            myScore["p1"] = Int32.Parse(key);
                        }
                    }
                }
            }

            if (isRoyalFlush(myScore) ){
                Console.WriteLine("{0} has a Royal Flush!",name);
                return 9001; 
            }   
            if (isStraightFlush(myScore) ){
                Console.WriteLine("{0} has a Straight Flush!",name);
                return 8000 + hand[4].val;
            }    
            if (isFourOfAKind(myScore) ){
                Console.WriteLine("{0} has four {1}s",name,hand[3].stringVal);
                return 7000 + (hand[1].val * 10) + hand[4].val;
            }
            if (isFullHouse(myScore) ) {
                Console.WriteLine("{0} has a Full house {1}s and {2}s",name,hand[0].stringVal,hand[4].stringVal);
                return 6000 + (hand[2].val * 10);
            }  
            if (isFlush(myScore) ) {
                Console.WriteLine("{0} has a flush of {1} with {2} high",name,hand[0].suit,hand[4].stringVal);
                return 5000 + hand[0].val;
            }
            if (isStraight() ) {
                Console.WriteLine("{0} has a {1} high straight",name,hand[4].stringVal);
                return 4000 + hand[4].val;
            }
            if (isThreeOfAKind(myScore) ){
                Console.WriteLine("{0} has three {1}s",name,hand[2].stringVal);
                return 3000 + (hand[2].val * 50 );
            }
            if (isTwoPair(myScore) ) {
                Console.WriteLine("{0} has two pair {1}s and {2}s",name,hand[1].stringVal,hand[3].stringVal);
                if (myScore["p1"] > myScore["p2"] ){
                    return 2000 + myScore["p1"] * 50 + myScore["p2"] + 34;
                } else {
                    return 2000 + myScore["p2"] * 50 + myScore["p1"] + 34;
                }
            }
            if (isPair(myScore) ) {
                Console.WriteLine("{0} has a pair of {1}s",name,myScore["p1"]);
                return 1000 + myScore["p1"]*50 + hand[4].val;
            }
            Console.WriteLine("{0} has {1} high",name,hand[4].stringVal);
            return (hand[4].val + hand[3].val + hand[2].val + hand[1].val + hand[0].val);
        }
    }
}