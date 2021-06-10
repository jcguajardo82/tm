using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServicesManagement.Web.Models;

namespace ServicesManagement.Web
{
    public class PostalCodeRepository
    {
        private decimal _latitude;
        private decimal _longitude;
        private decimal _radius = 30000;
        private string _Url = "https://maps.googleapis.com/maps/api/place/autocomplete/json?input={0}&types=establishment&location={1},{2}&radius={3}&strictbounds&key=AIzaSyBbzO0IdrX-vkGcX5_hXc13QhbaUeBAsII";
        private string _UrlRegions = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={0},{1}&radius={2}&type=(regions)&keyword=*&key=AIzaSyAe_ToUGyd3KLvDe2cC3FS8xBNmEQeofoE";
        private string _UrlRegionsAlt = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={0},{1}&radius={2}&type=(regions)&keyword=*&key=AIzaSyAe_ToUGyd3KLvDe2cC3FS8xBNmEQeofoE&pagetoken={3}";
        private string _UrlDetails = "https://maps.googleapis.com/maps/api/place/details/json?place_id={0}&fields=address_component&key=AIzaSyBbzO0IdrX-vkGcX5_hXc13QhbaUeBAsII";
        private List<string> PlaceIdList = new List<string>();
        private List<int> PostalCodeList = new List<int>();
        public IList<int> GetPostalCode(decimal latitude, decimal longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
            //GetEstablisments();
            GetRegions();
            GetPostalCodes();
            return PostalCodeList;
        }

        private void GetRegions()
        {
            HttpClient httpClient = new HttpClient();

            var result = httpClient.GetAsync(string.Format(_UrlRegions, _latitude, _longitude, _radius)).Result;
            Task<Stream> streamTask = result.Content.ReadAsStreamAsync();
            Stream stream = streamTask.Result;
            StreamReader sr = new StreamReader(stream);
            Prediction PredictionList = JsonConvert.DeserializeObject<Prediction>(sr.ReadToEnd());
            sr.Close();
            stream.Close();

            if (PredictionList.status == "OK")
            {
                foreach (var s in PredictionList.results)
                {
                    if (!PlaceIdList.Contains(s.place_id))
                    {
                        PlaceIdList.Add(s.place_id);
                    }
                }
            }
            while (!string.IsNullOrEmpty(PredictionList.next_page_token))
            {
                var result2 = httpClient.GetAsync(string.Format(_UrlRegionsAlt, _latitude, _longitude, _radius, PredictionList.next_page_token)).Result;
                streamTask = result2.Content.ReadAsStreamAsync();
                stream = streamTask.Result;
                sr = new StreamReader(stream);
                PredictionList = JsonConvert.DeserializeObject<Prediction>(sr.ReadToEnd());
                sr.Close();
                stream.Close();

                if (PredictionList.status == "OK")
                {
                    foreach (var s in PredictionList.results)
                    {
                        if (!PlaceIdList.Contains(s.place_id))
                        {
                            PlaceIdList.Add(s.place_id);
                        }
                    }
                }
            }
        }

        private void GetEstablisments()
        {
            HttpClient httpClient = new HttpClient();

            for (char letter = 'A'; letter <= 'Z'; letter++)
            {
                var result = httpClient.GetAsync(string.Format(_Url, letter, _latitude, _longitude, _radius)).Result;
                Task<Stream> streamTask = result.Content.ReadAsStreamAsync();
                Stream stream = streamTask.Result;
                StreamReader sr = new StreamReader(stream);
                //var soapResponse = XDocument.Load(sr);
                //Console.WriteLine(soapResponse.ToString());
                Prediction PredictionList = JsonConvert.DeserializeObject<Prediction>(sr.ReadToEnd());
                sr.Close();
                stream.Close();

                if (PredictionList.status == "OK")
                {
                    foreach (var s in PredictionList.results)
                    {
                        if (!PlaceIdList.Contains(s.place_id))
                        {
                            PlaceIdList.Add(s.place_id);
                        }
                    }
                }
            }
        }

        private void GetPostalCodes()
        {
            HttpClient httpClient = new HttpClient();

            foreach (string placeId in PlaceIdList)
            {
                var result = httpClient.GetAsync(string.Format(_UrlDetails, placeId)).Result;
                Task<Stream> streamTask = result.Content.ReadAsStreamAsync();
                Stream stream = streamTask.Result;
                StreamReader sr = new StreamReader(stream);
                //var soapResponse = XDocument.Load(sr);
                //Console.WriteLine(soapResponse.ToString());
                string response = sr.ReadToEnd();
                JObject o = JObject.Parse(response);
                sr.Close();
                stream.Close();
                var srchItem = o.SelectToken("result.address_components").ToList().Where(x => x["types"][0].ToString() == "postal_code").FirstOrDefault();
                if (srchItem != null)
                {
                    var postal_code = srchItem.SelectToken("long_name").ToString();
                    int ipostal_code;
                    if (int.TryParse(postal_code, out ipostal_code))
                    {
                        if (!string.IsNullOrEmpty(postal_code) && !PostalCodeList.Contains(Convert.ToInt32(postal_code)))
                        {
                            PostalCodeList.Add(Convert.ToInt32(postal_code));
                        }
                    }
                }
            }
        }
    }
}