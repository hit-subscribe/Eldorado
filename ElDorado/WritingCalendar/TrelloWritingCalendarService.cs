﻿using ElDorado.Domain;
using Manatee.Trello;
using Manatee.Trello.ManateeJson;
using Manatee.Trello.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElDorado.WritingCalendar
{
    public class TrelloWritingCalendarService
    {
        public const string PlannedPostTrelloListName = "Planned Posts";
        public const string TrelloBoardId = "AhqnpUJD";


        private TrelloAuthorization Auth => TrelloAuthorization.Default;

        private Lazy<Board> LazyWritingCalendar = new Lazy<Board>(() => new Board(TrelloBoardId));

        private Board WritingCalendar => LazyWritingCalendar.Value;
        

        private CardCollection PlannedPostCards => WritingCalendar.Lists.First(l => l.Name == PlannedPostTrelloListName).Cards;
        private IList<Card> BoardCards => WritingCalendar.Cards.ToList();

        public void Initialize(CredentialStore credentialStore)
        {
            var serializer = new ManateeSerializer();
            TrelloConfiguration.Serializer = serializer;
            TrelloConfiguration.Deserializer = serializer;
            TrelloConfiguration.JsonFactory = new ManateeFactory();
            TrelloConfiguration.RestClientProvider = new WebApiClientProvider();

            Auth.AppKey = credentialStore["TrelloAppKey"];
            Auth.UserToken = credentialStore["TrelloUserToken"];
        }
        public virtual void AddCard(BlogPost postToAdd)
        {
            var erik = WritingCalendar.Members.Where(m => m.UserName == "erikdietrich");
            var members = WritingCalendar.Members.Where(m => !string.IsNullOrEmpty(postToAdd.Author) && m.FullName.Contains(postToAdd.Author)).Union(erik);

            var clientLabels = WritingCalendar.Labels.Where(l => l.Name == postToAdd?.Blog?.CompanyName);

            PlannedPostCards.Add(name: postToAdd.AuthorTitle, dueDate: postToAdd.DraftDate.Value.AddHours(12), members: members, labels: clientLabels);
        }

        public virtual bool DoesCardExist(string blogPostTitle)
        {
            return BoardCards.ToList().Any(c => c.Name.LooselyMatches(blogPostTitle));
        }

    }
}
