using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Service.DataContracts;

namespace Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMainSvc" in both code and config file together.
    [ServiceContract]
    public interface IMainSvc
    {
        [OperationContract]
        void DoWork();

        /// <summary>
        /// Gibt alle Parkplätze in Json zurück. Nur Name
        /// TESTMETODE
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        List<string> GetParkplatzNames();

        /// <summary>
        /// Gibt alle Parkplätze in Json zurück
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetParkplatzsByEtage/{etage}")]
        CsResultParkplatz GetParkplatzsByEtage(string etage);

        /// <summary>
        /// Gibt alle Parkplatzarten zurück
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        CsResultParkplatzart GetParkplatzarts();

        /// <summary>
        /// Gibt alle Parkplatzarten zurück
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "ReserveParkplatz/{datum}.{schild}.{art}.{user}")]
        CsResultParkplatz ReserveParkplatz(string datum, string schild, string art, string user);

        /// <summary>
        /// Gibt alle Parkplatzarten zurück
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetReservationsById/{id}")]
        CsResultReservation GetReservationsById(string id);

        /// <summary>
        /// Gibt alle Parkplatzarten zurück
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "SetParkplatzStatus/{id}.{status}")]
        void SetParkplatzStatus(string id, string status);

        /// <summary>
        /// Gibt alle Parkplatzarten zurück
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetParkplatzStatus/{id}")]
        CsResultParkplatz GetParkplatzStatus(string id);

        /// <summary>
        /// Gibt alle Parkplatzarten zurück
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetParkplatzStati")]
        CsResultParkplatz GetParkplatzStati();

        /// <summary>
        /// Gibt alle Parkplatzarten zurück
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetEtages")]
        CsResultEtage GetEtages();

        #region TestMethods

        /// <summary>
        /// Parkplatz werden in Datenbank geschrieben
        /// </summary>
        /// <param name="Nummer"></param>
        /// <param name="Name"></param>
        /// <param name="Etage"></param>
        [OperationContract]
        [WebGet(UriTemplate = "AddParkplatz/{Nummer}.{Name}.{Etage}")]
        void AddParkplatz(string Nummer, string Name, string Etage);

        /// <summary>
        /// </summary>
        
        [OperationContract]
        [WebGet(UriTemplate = "DeleteAllReservations")]
        void DeleteAllReservations();

        #endregion Testmethods
    }
}
