using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Service;

namespace ServiceHoster
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("+             IPS Host Application            +");
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.ResetColor();

                Console.WriteLine("Start Loading Assemblies\n");

                var serviceHost = new ServiceHost(typeof(MainSvc));// or use WebServiceHost

                serviceHost.Open();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(serviceHost.BaseAddresses);
                Console.WriteLine("http://193.196.175.149:8733/Service/MainSvc/");
                Console.WriteLine(serviceHost.Description);
                Console.WriteLine(serviceHost.State);


                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------------------------------------------------------");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Alle Host Services gestartet...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {

                Console.Read();
            }
        }
    }
}
