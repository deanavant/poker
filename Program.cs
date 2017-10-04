using System;
using System.Collections.Generic;

namespace cards
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Startgame mygame=new Startgame();
            mygame.start();

            Deck myDeck = new Deck();
            myDeck.shuffle();

            Console.WriteLine("Distributing cards to all players...");
            foreach(Player element in mygame.players)
            {
                for(int i=0;i<5;i++)
                {
                    element.draw(myDeck);
                }
                element.mySort();
            }

            Console.WriteLine("Do you want to start the game ?");
            Console.WriteLine("Yes or No");
            string choice = Console.ReadLine();
            if(choice=="Yes"||choice=="y"||choice=="yes")
            {   int ind=0;
                foreach(Player element in mygame.players)
                {   
                    Console.Clear();
                    ind =ind+1;
                    Console.WriteLine("Is Player"+ind+" ready to play?");
                    Console.WriteLine("Y or N");
                    string opt=Console.ReadLine();
                    if(opt=="N" || opt=="n")
                    {
                        while(opt != "y")
                        {
                            Console.WriteLine("Is Player"+ind+" ready now?");
                            opt=Console.ReadLine();
                        }
                    }
                    if(opt=="Y" || opt=="y")
                    {
                        Console.Clear();
                        Boolean draw=true;
                        Console.WriteLine("Player "+ element.name +" your cards are:");
                        element.showHand();
                        int numOfDiscards=0;
                        while(draw)
                        {   int val=0;
                            if(numOfDiscards==5)
                            {
                                element.showHand();
                                Console.WriteLine("Your hand is empty");
                                val=2;
                            }
                            else
                            {
                                Console.WriteLine("Do you want to discard a card or stay?");
                                Console.WriteLine("Press 1 to discard and 2 to stay");
                                val = Convert.ToInt32(Console.ReadLine());
                            }
                            if(val==1)
                            {   
                                Console.WriteLine("Which Card do you want to discard? Enter the index");
                                int val1 = Convert.ToInt32(Console.ReadLine());
                                numOfDiscards = numOfDiscards+1;
                                element.discard(val1-1);
                                element.showHand();
                            }    
                            else if(val==2)
                            {   
                                if(numOfDiscards>0)
                                {   
                                    
                                    Console.WriteLine("Drawing new cards...");
                                    System.Threading.Thread.Sleep(600);
                                    Console.WriteLine("Your New Hand is :");
                                    for(int i=0;i<numOfDiscards;i++)
                                    {
                                        element.draw(myDeck);
                                    }
                                    element.mySort();
                                    element.showHand();
                                    System.Threading.Thread.Sleep(4000);
                                }
                                draw=false;
                            }
                        }
                    }
                
                }
                string topPlayer = "";
                int topScore = 0;
                Console.Clear();
                foreach(Player p in mygame.players){
                    int x = p.score();
                    p.showHand();
                    Console.WriteLine("**************");
                    if (x > topScore){
                        topScore = x;
                        topPlayer = p.name;
                    }
                }
                Console.WriteLine("{0} wins with {1} points!",topPlayer,topScore);
                
            }
            else if(choice=="No"||choice=="no")
            {
                Console.WriteLine("Are you sure you want to Exit the Game ?");
            }
        }
    }
}
