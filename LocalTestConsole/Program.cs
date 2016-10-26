using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayerTest;
using DataLayer;

namespace LocalTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //var ent = Data.getEntities();
            //using (ent)
            //{
            //    var x = ent.ParkplatzSets.Select(parkplatz => parkplatz.Name).ToList();
            //}

            var entModel = DataModel.getEntities();
            using (entModel)
            {
                var x = entModel.ParkplatzSets.Select(parkplatz => parkplatz.Name).ToList();
            }
        }
    }
}
