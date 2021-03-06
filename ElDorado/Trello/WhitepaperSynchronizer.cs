﻿using ElDorado.Console;
using ElDorado.Console.Trello;
using ElDorado.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElDorado.Console.Trello
{
    public class WhitepaperSynchronizer : CalendarService, ITrelloSynchronizer<Whitepaper>
    {
        public WhitepaperSynchronizer(ICalendarBoard board = null)
        {
            Board = board ?? new CalendarBoard("jV76Ii6Z");
        }

        public void CreateCardForEntity(Whitepaper whitepaperToAdd)
        {
            string companyName = whitepaperToAdd.Blog.CompanyName;
            var cardMembers = new string[] { whitepaperToAdd.AuthorTrelloUserName, whitepaperToAdd.EditorTrelloUserName };
            
            var card = Board.AddPlannedPostCard(name: whitepaperToAdd.Title,
                                                dueDate: whitepaperToAdd.TargetOutlineDate.SafeToMidnightEastern(),
                                                trelloUserNames: cardMembers,
                                                companyName: companyName);

            whitepaperToAdd.TrelloId = card.Id;
        }

        public void UpdateCardForEntity(Whitepaper whitepaperToEdit)
        {
            var card = Board.AllCards.FirstOrDefault(c => c.Id == whitepaperToEdit?.TrelloId);
            if (card == null)
                return;

            card.Name = whitepaperToEdit.Title;
            UpdatePlannedPostPropertiesIfApplicable(whitepaperToEdit, card);
        }

        private void UpdatePlannedPostPropertiesIfApplicable(Whitepaper whitepaperToEdit, ITrelloCard card)
        {
            if (card.ListName == PlannedPostsListName)
            {
                card.DueDate = whitepaperToEdit.TargetOutlineDate.SafeToMidnightEastern();
                card.UpdateLabels(Board.GetLabelsForCompany(whitepaperToEdit.Blog.CompanyName));
                card.UpdateMembers(Board.GetMembersWithUserNames(whitepaperToEdit.AuthorTrelloUserName, whitepaperToEdit.EditorTrelloUserName));
            }
        }
    }
}
