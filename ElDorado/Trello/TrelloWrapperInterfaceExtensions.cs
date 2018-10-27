﻿using ElDorado.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElDorado.Trello
{
    //I'm creating this instead of adding instance methods to the Trello API classes because those are untestable.  Want to keep logic out of there.
    public static class TrelloWrapperInterfaceExtensions
    {
        public static void BuildDescriptionFromBlogPost(this ITrelloCard target, BlogPost post)
        {
            var ghostwrittenText = post.IsGhostwritten ? "Ghostwritten" : string.Empty;
            var doublePostText = post.IsDoublePost ? "Double Post" : string.Empty;

            var specialConsiderationText = GenerateCommaSeparatedList(ghostwrittenText, doublePostText);

            target.Description = SeparateIntoLines($"{Bold("Mission")}: {post.Mission}",
                                                   $"{Bold("Keyword")}: {post.Keyword}", 
                                                   $"{Bold("Persona")}: {post.Persona}", 
                                                   $"{Bold("Post Notes")}: {post.PostNotes}", 
                                                   $"{Bold("Client Notes")}: {post.ClientNotes}",
                                                   $"{Bold("Special Considerations")}: {specialConsiderationText}");
        }

        private static string SeparateIntoLines(params string[] args)
        {
            return string.Join(Environment.NewLine, args);
        }

        private static string GenerateCommaSeparatedList(params string[] args)
        {
            return string.Join(", ", args.Where(a => !string.IsNullOrEmpty(a)));
        }

        private static string Bold(string textToBold)
        {
            return $"**{textToBold}**";
        }
    }
}