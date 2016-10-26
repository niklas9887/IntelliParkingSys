using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DataLayer;
using Service.DataContracts;

namespace Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MainSvc" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class MainSvc : IMainSvc
    {
        public void DoWork()
        {
            //Debug.WriteLine(this.GetType() + " ctor invoked");
        }

        public CsResultParkplatz GetParkplatzsByEtage(string etage)
        {
            try
            {
                var etageI = Convert.ToInt32(etage);

                var entModel = DataModel.getEntities();
                var res = new CsResultParkplatz { flag = 1 };

                var resPP = new List<CsParkplatz>();

                var entModelEtage = entModel.ParkplatzSets.Where(pp => pp.Etage == etageI);
                foreach (var ppEnt in entModelEtage)
                {
                    var pp = CreateCsParkplatz(ppEnt);

                    foreach (var resEnt in ppEnt.ReservierungSets)//Checkt ab ob Reservierung am heutigen Tage vorhanden ist
                    {
                        
                        if (resEnt.Datum == DateTime.Today && pp.Zustand == "Leer")//KEVin erklären, falls pp bestezt is abert reserivert kommt besetzt zurück nur wenn parkplatz frei is wird gecheckt ob reservierung heute vorhanden ist
                        {
                            pp.Zustand = "Reserviert";
                            break;
                        }
                    }

                    resPP.Add(pp);
                }
                res.Parkplatzs = resPP;

                if (res.Parkplatzs.Count == 0)
                    throw new Exception("Keine Parkplätze in dieser Etage");
                return res;
            }
            catch (Exception ex)
            {
                var res = new CsResultParkplatz
                {
                    flag = 0,
                    exception = ex.Message
                };

                return res;
            }
        }

        private static CsParkplatz CreateCsParkplatz(ParkplatzSet ppEnt)
        {
            var parkplatz = new CsParkplatz();
            parkplatz.Id = ppEnt.Id;
            parkplatz.Name = ppEnt.Name;
            parkplatz.Etage = ppEnt.Etage;
            parkplatz.Zustand = ppEnt.Zustand;
            parkplatz.KameraParkplatzs = null;
            parkplatz.Parkplatzart_Id = ppEnt.Parkplatzart_Id;

            var art = new CsParkplatzart
            {
                Id = ppEnt.ParkplatzartSet.Id,
                Art = ppEnt.ParkplatzartSet.Art
            };
            parkplatz.Parkplatzart = art;
            return parkplatz;
        }

        public CsResultParkplatzart GetParkplatzarts()
        {
            try
            {
                var entModel = DataModel.getEntities();
                var res = new CsResultParkplatzart { flag = 1 };
                var resPA = new List<CsParkplatzart>();

                foreach (var paEnt in entModel.ParkplatzartSets)
                {
                    var PA = new CsParkplatzart
                    {
                        Id = paEnt.Id,
                        Art = paEnt.Art
                    };
                    resPA.Add(PA);
                }
                res.Parkplatzarts = resPA;
                return res;
            }
            catch (Exception ex)
            {
                var res = new CsResultParkplatzart
                {
                    flag = 0,
                    exception = ex.Message
                    
                };
                return res;
            }
        }

        public CsResultParkplatz ReserveParkplatz(string datum, string schild, string art, string user)
        {
            //Debug.WriteLine(this.GetType() + " ReserveParkplatz(...) invoked");
            try
            {
                //var date = DateTime.ParseExact(datum, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture);
                var date = DateTime.ParseExact(datum, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                var artI = Convert.ToInt32(art);
                var entModel = DataModel.getEntities();
                var res = new CsResultParkplatz();
                using (entModel)
                {

                    var ppsEnt = entModel.ParkplatzSets.Where(pp => pp.Parkplatzart_Id == artI);
                    foreach (var ppEnt in ppsEnt)
                    {
                        if(date==DateTime.Today&&ppEnt.Zustand!="Leer")
                            continue;


                        if (ppEnt.ReservierungSets.Count == 0)//Gibt keine Reservierungen für diesen Parkplatz
                        {
                            Reserve(entModel, ppEnt, date, schild, user);
                            var pp = CreateCsParkplatz(ppEnt);
                            res.Parkplatzs = new List<CsParkplatz> { pp };
                            res.flag = 1;
                            break;

                        }
                        bool timable = true;
                        foreach (var reEnt in ppEnt.ReservierungSets)//Check obs eine Reservierung gibt
                        {
                            if (reEnt.Datum == date)
                                timable = false;
                        }

                        if (timable)
                        {
                            Reserve(entModel, ppEnt, date, schild, user);

                            var pp = CreateCsParkplatz(ppEnt);
                            res.Parkplatzs = new List<CsParkplatz> { pp };
                            res.flag = 1;
                            break;
                        }
                    }

                    entModel.SaveChanges();
                    return res;
                }

            }
            catch (Exception ex)
            {
                var res = new CsResultParkplatz
                {
                    flag = 0,
                    exception = ex.Message
                };
                return res;
            }

        }

        private void Reserve(IPSEntities entModel, ParkplatzSet ppEnt, DateTime date, string schild, string user)
        {
            var pp = entModel.ParkplatzSets.SingleOrDefault(b => b.Id == ppEnt.Id);
            ReservierungSet reEnt = new ReservierungSet();
            reEnt.Datum = date;
            reEnt.Nummernschild = schild;
            reEnt.ParkplatzSet = pp;
            reEnt.User = user;

            entModel.ReservierungSets.Add(reEnt);


        }

        public CsResultReservation GetReservationsById(string id)
        {
            try
            {
                var idI = Convert.ToInt32(id);
                var entModel = DataModel.getEntities();
                using (entModel)
                {
                    var resres = new CsResultReservation { flag = 1 };
                    var resLst = new List<CsReservierung>();

                    foreach (var resEnt in entModel.ReservierungSets.Where(r => r.Parkplatz_Id == idI))
                    {
                        var res = new CsReservierung();
                        res.Parkplatz_Id = resEnt.Parkplatz_Id;
                        res.Datum = resEnt.Datum.ToShortDateString();
                        res.Id = resEnt.Id;
                        res.Nummernschild = resEnt.Nummernschild;
                        res.Parkplatz = CreateCsParkplatz(resEnt.ParkplatzSet);
                        res.User = resEnt.User;
                        resLst.Add(res);

                    }
                    resres.Reservierungs = resLst;
                    return resres;
                }
            }
            catch (Exception ex)
            {

                var res = new CsResultReservation
                {
                    flag = 0,
                    exception = ex.Message
                };
                
                return res;
            }

        }

        public void SetParkplatzStatus(string id, string status)
        {
            var entModel = DataModel.getEntities();
            using (entModel)
            {
               
                    //TODO: Logik überlegen wie ablauf stattfinden soll
                    //Reserviert auch Status oder nur normale Status
                    var idI = Convert.ToInt32(id);
                    var entPP = entModel.ParkplatzSets.First(pp => pp.Id == idI);
                    entPP.Zustand = status;
                    entModel.SaveChanges();

                
            }

        }

        public CsResultParkplatz GetParkplatzStatus(string id)
        {
            var idI = Convert.ToInt32(id);
            var entModel = DataModel.getEntities();
            using (entModel)
            {
                try
                {
                    var resPP = new CsResultParkplatz();
                    resPP.Parkplatzs = new List<CsParkplatz>();
                    resPP.flag = 1;
                    foreach (var ppEnt in entModel.ParkplatzSets.Where(pp=>pp.Id==idI))
                    {
                        var pp = CreateCsParkplatz(ppEnt);

                        foreach (var resEnt in ppEnt.ReservierungSets)//Checkt ab ob Reservierung am heutigen Tage vorhanden ist
                        {
                            if (resEnt.Datum == DateTime.Today && pp.Zustand == "Leer")//KEVin erklären, falls pp bestezt is abert reserivert kommt besetzt zurück nur wenn parkplatz frei is wird gecheckt ob reservierung heute vorhanden ist
                            {
                                pp.Zustand = "Reserviert";
                                break;
                            }
                        }

                        //In Result-Objekt packen
                        resPP.Parkplatzs.Add(pp);

                    }
                    return resPP;

                }
                catch (Exception ex)
                {
                    var resPP = new CsResultParkplatz();
                    resPP.flag = 0;
                    resPP.exception = ex.Message;
                    return resPP;
                }
            }
        }

        public CsResultParkplatz GetParkplatzStati()
        {
            var entModel = DataModel.getEntities();
            using (entModel)
            {
                try
                {
                    var resPP = new CsResultParkplatz();
                    resPP.Parkplatzs = new List<CsParkplatz>();
                    resPP.flag = 1;
                    foreach (var ppEnt  in entModel.ParkplatzSets)
                    {
                        var pp = CreateCsParkplatz(ppEnt);

                        foreach (var resEnt in ppEnt.ReservierungSets)//Checkt ab ob Reservierung am heutigen Tage vorhanden ist
                        {
                            if (resEnt.Datum == DateTime.Today && pp.Zustand=="Leer")//KEVin erklären, falls pp bestezt is abert reserivert kommt besetzt zurück nur wenn parkplatz frei is wird gecheckt ob reservierung heute vorhanden ist
                            {
                                pp.Zustand = "Reserviert";
                                break;
                            }
                        }

                        //In Result-Objekt packen
                        resPP.Parkplatzs.Add(pp);

                    }
                    return resPP;

                }
                catch (Exception ex)
                {
                    var resPP = new CsResultParkplatz();
                    resPP.flag = 0;
                    resPP.exception = ex.Message;
                    return resPP;
                }
            }
        }

       
        public CsResultEtage GetEtages()
        {
            var entModel = DataModel.getEntities();
            using (entModel)
            {
                try
                {
                    var res = new CsResultEtage();
                    res.flag = 1;
                    var lst = new List<int>();
                    foreach (var pp in entModel.ParkplatzSets)
                    {
                        lst.Add(pp.Etage);
                    }
                    res.Etages = lst;
                    return res;
                }
                catch (Exception ex)
                {
                    var res= new CsResultEtage();
                    res.flag = 0;
                    res.exception = ex.Message;
                    return res;
                }
            }
        }

        #region TestMethods

        public List<string> GetParkplatzNames()
        {
            var entModel = DataModel.getEntities();
            using (entModel)
            {
                var entPP = entModel.ParkplatzSets.Select(parkplatz => parkplatz.Name).ToList();
                return entPP;
            }
        }

        public void AddParkplatz(string Nummer, string Name, string Etage)
        {
            var entModel = DataModel.getEntities();
            using (entModel)
            {
                try
                {
                    var iNummer = Convert.ToInt32(Nummer);
                    var iEtage = Convert.ToInt32(Etage);
                    
                    int et = Convert.ToInt32(Etage);


                    var art = entModel.ParkplatzartSets.SingleOrDefault(b => b.Id == 1);
                    ParkplatzSet parkplatz = new ParkplatzSet { Nummer = iNummer, Name = Name, Etage = iEtage, ParkplatzartSet = art };
                    entModel.ParkplatzSets.Add(parkplatz);
                    entModel.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
        }

        public void DeleteAllReservations()
        {
            var entModel = DataModel.getEntities();
            using (entModel)
            {
                
                    entModel.ReservierungSets = null;
                    entModel.SaveChanges();
                
            }
        }

        #endregion TestMethods
    }
}
