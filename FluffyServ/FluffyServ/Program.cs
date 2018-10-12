using FluffyServ.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyServ
{
    class Program
    {
        static void Main(string[] args)
        {
            GameServer serveur = new GameServer();
            serveur.StartServ();

            Console.WriteLine("Waiting for a key to stop");
            Console.ReadKey();
        }

        
    }
}
