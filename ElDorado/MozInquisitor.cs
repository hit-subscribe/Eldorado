﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ElDorado
{
    public class MozInquisitor
    {
        private readonly Dictionary<string, string> _credentials;
        private readonly SimpleWebClient _simpleWebClient;

        public DateTime CurrentTime { get; set; } = DateTime.Now;

        public MozInquisitor(IEnumerable<string> fileLines, SimpleWebClient simpleWebClient)
        {
            _simpleWebClient = simpleWebClient;
            _credentials = fileLines.ToDictionary(line => line.Split(':')[0], line => line.Split(':')[1]);
        }

        public decimal GetDomainAuthority(string baseUrl)
        {
            try
            {
                var authenticatedUrl = BuildUrl(baseUrl);
                var rawText = _simpleWebClient.GetRawText(authenticatedUrl);

                dynamic mozRow = JsonConvert.DeserializeObject(rawText);
                return Math.Round((decimal)mozRow.pda, 2);
            }
            catch
            {
                return 0;
            }
        }

        public string BuildUrl(string baseUrl)
        {
            var accessId = _credentials["ID"];
            var secret = _credentials["Secret"];
            var expiration = CurrentTime.AddMinutes(5).ToUnixTime();

            var bytesToHash = Encoding.ASCII.GetBytes($"{accessId}\n{expiration}");
            var signatureCode = GetSignatureCode(bytesToHash, secret);

            return $"http://lsapi.seomoz.com/linkscape/url-metrics/{baseUrl}?Cols=288230376151711743&AccessID={accessId}&Expires={expiration}&Signature={signatureCode}";
        }

        private static String GetSignatureCode(byte[] bytesToHash, string secret)
        {
            using (HMACSHA1 hashCalculator = new HMACSHA1(Encoding.ASCII.GetBytes(secret)))
            {
                var hash = hashCalculator.ComputeHash(bytesToHash);
                var hashedBytes = Convert.ToBase64String(hash);

                return HttpUtility.UrlEncode(hashedBytes);
            }
        }
    }
}
