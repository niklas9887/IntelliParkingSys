using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private string url = "http://193.196.175.149:8733/Design_Time_Addresses/Service/MainSvc/";
        [TestMethod]
        public void TestGetParkplatzNames()
        {

            var client = new RestClient(url);
            var request = new RestRequest("GetParkplatzNames", Method.GET);
            //request.AddHeader()

            IRestResponse response = client.Execute(request);
            var content = response.Content;



        }

        [TestMethod]
        public void TestGetParkplatzsByEtage()
        {
            var client = new RestClient(url);
            var request = new RestRequest("GetParkplatzsByEtage/{etage}", Method.GET);
            request.AddHeader("name", "value");
            request.AddUrlSegment("etage", "1");
            
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            
        }

        [TestMethod]
        public void TestGetParkplatzarts()
        {
            var client = new RestClient(url);
            var request = new RestRequest("GetParkplatzarts", Method.GET);
            
            IRestResponse response = client.Execute(request);
            var content = response.Content;

        }
    }
    
}

