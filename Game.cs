using System;

namespace game {

    public class Game {

        public Game() {
            firstPlayer = MakeNewPlayer();
            secondPlayer = MakeNewPlayer();

            var battle = new Battle(firstPlayer, secondPlayer);
            battle.Start();
        }

        private Player MakeNewPlayer() {
            Console.Write("Enter your nickname: ");
            string _nickname = Console.ReadLine();

            Player player = new Player(_nickname);
            player.MakeNewArmy();

            return player;
        }
        
        private Player firstPlayer;
        private Player secondPlayer;
    }

}