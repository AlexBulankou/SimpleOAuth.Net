// Simple OAuth .Net
// (c) 2017-2020 Alex Bulankou
// Simple OAuth .Net may be freely distributed under the MIT license.

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using Xunit;

namespace SimpleOAuth.Tests
{
    public class YelpIntegrationTest
    {
        /// <summary>
        /// Verifies SimpleOAuth works with Yelp 
        /// </summary>
        [Fact]
        public void VerifyYelpIntegration ()
        {
            double swLat = 47.00;
            double swLon = -122.00;
            double neLat = 48.00;
            double neLon = -121.00;

            // These are creds generated specifically for unit tests. Don't use them for anything else.
            // BTW, Yelp search endpoint is being discontinued on 6/30/2018: https://engineeringblog.yelp.com/2017/06/upcoming-deprecation-of-yelp-api-v2.html
            var token = new Tokens ()
            {
                ConsumerKey = "Mvd9Yjr87auXdAYk-HnDLg",
                ConsumerSecret = "ZsyUxYDztrXlv_gjSf57kObrMvc",
                AccessToken = "k8ymPXf5D8-famDn-KELeHOkWnqq48aH",
                AccessTokenSecret = "YCVia5loNuPqCBxxxgG_1MprIfs"
            };

            string yelpSearchPath = "http://api.yelp.com/v2/search/";
            var uriBuilder = new UriBuilder (yelpSearchPath);
            uriBuilder.Query = string.Format ("category_filer={0}&bounds={1}&offset={2}", "italian", string.Format ("{0},{1}|{2},{3}", swLat, swLon, neLat, neLon), "0");

            var request = WebRequest.Create (uriBuilder.ToString ());
            request.Method = "GET";

            request.SignRequest (token).WithEncryption (EncryptionMethod.HMACSHA1).InHeader ();

            HttpWebResponse response = (HttpWebResponse) request.GetResponseAsync ().Result;
            var stream = new StreamReader (response.GetResponseStream (), Encoding.UTF8);
            var data = stream.ReadToEnd ();

            Assert.True (data.IndexOf ("Pie For the People NW") > -1);

        }
    }
}