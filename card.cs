namespace cards{

    public class Card{
        public string stringVal;
        public string suit;
        public int val;

        public Card(string sval,string suit,int value){
            stringVal = sval;
            this.suit = suit;
            val = value;
        }
    }
}