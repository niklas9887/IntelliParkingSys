using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var ent = new IPSEntities();
            using (ent)
            {
                var x = ent.ParkplatzSets.Select(parkplatz => parkplatz.Name).ToList();
            }
        }
        
    }
    public class Data
    {
        public static IPSEntities getEntities()
        {
            return new IPSEntities();
        }
    }
}
