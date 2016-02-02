using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using vindinium.Infrastructure.Bots;

namespace vindinium
{
    class Client
    {
        /**
         * Launch client.
         * @param args args[0] Private key
         * @param args args[1] [training|arena]
         * @param args args[2] number of turns
         * @param args args[3] HTTP URL of Vindinium server (optional)
         */
        static void Main(string[] args)
        {
            string serverUrl = args.Length == 4 ? args[3] : "http://vindinium.org";

            //create the server stuff, when not in training mode, it doesnt matter
            //what you use as the number of turns
			Server server = new Server(args[0], args[1] != "arena", uint.Parse(args[2]), serverUrl, args[4]);

            //create the random bot, replace this with your own bot
			//var bot = new RandomBot(Server);
			server.CreateGame();


			if (server.Errored == false)
			{
				//opens up a webpage so you can view the game, doing it async so we dont time out
				new Thread(delegate()
				{
					System.Diagnostics.Process.Start(server.ViewUrl);
				}).Start();
			}


			//var bot = new RandomBot(Server);
			var bot = new HorseBandit(server);

            //now kick it all off by running the bot.
            bot.Run();

            Console.Out.WriteLine("done");

            Console.Read();
        }
    }
}
