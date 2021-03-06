﻿using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElDorado.Console.WritingCalendar
{
    public class GoogleSheet
    {
        private readonly string _spreadsheetId;

        private readonly SheetsService _service;

        public GoogleSheet(string spreadsheetId)
        {
            _spreadsheetId = spreadsheetId;
            _service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = GetSheetsUserCredential(),
                ApplicationName = "El Dorado",
            });
        }

        public virtual IList<IList<object>> GetCells(string range)
        {
            var request = _service.Spreadsheets.Values.Get(_spreadsheetId, range);
            return request.Execute().Values;
        }

        public virtual void UpdateSpreadsheet(string range, IList<IList<object>> cells)
        {
            var updateRequest = _service.Spreadsheets.Values.Update(new ValueRange() { Values = cells }, _spreadsheetId, range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            updateRequest.Execute();

        }

        private static UserCredential GetSheetsUserCredential()
        {
            // If modifying these scopes, delete your previously saved credentials
            // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
            UserCredential credential;

            using (var stream = new FileStream(@"CredFiles\google.cred", FileMode.Open, FileAccess.Read))
            {
                string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/sheets.googleapis.com-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, 
                    new string[] { SheetsService.Scope.Spreadsheets }, "user", CancellationToken.None, new FileDataStore(credPath, true)).Result;
            }

            return credential;
        }
    }
}
