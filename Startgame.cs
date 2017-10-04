using System;
using System.Collections.Generic;

namespace cards{

    public class Startgame
    {
        public List<Player> players=new List<Player>();
        public void  start()
        {
            
            Console.WriteLine("****WELCOME TO PLAY POKER-5 CARD DRAW****");
            Console.WriteLine("How many players would like to play the game(2-4) ?");
            int numOfPlayers = Convert.ToInt32(Console.ReadLine());
            for(int i=1;i<=numOfPlayers;i++)
            {
            Console.WriteLine("Please enter the name of Player"+i+":");
            string name=Console.ReadLine();
            players.Add(new Player(name));
            }
        }

    }
}