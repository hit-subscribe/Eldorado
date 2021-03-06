﻿using ElDorado.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElDorado.Console.Metrics
{
    public class BlogMetricsRecorder
    {
        private FeedlyInquisitor _feedlyInquisitor;
        private readonly AlexaDataInquisitor _alexaInquisitor;
        private readonly MozInquisitor _mozInquisitor;

        public virtual Func<DateTime> TimeRecordingStrategy { get; set; } = () => DateTime.Now;

        public BlogMetricsRecorder(FeedlyInquisitor feedlyInquisitor, AlexaDataInquisitor alexaInquisitor, MozInquisitor mozInquisitor)
        {
            _mozInquisitor = mozInquisitor;
            _alexaInquisitor = alexaInquisitor;
            _feedlyInquisitor = feedlyInquisitor;
        }

        public IEnumerable<BlogMetric> GenerateMetrics(IEnumerable<Blog> blogs)
        {
            foreach (var blog in blogs.Where(b => b.Url != null))
            {
                var mozStats = _mozInquisitor.GetMozStats(blog.Hostname);
                yield return new BlogMetric()
                {
                    BlogId = blog.Id,
                    FeedlySubscribers = string.IsNullOrEmpty(blog.FeedlyUrl) ? 0 : _feedlyInquisitor.GetSubscriberCount(blog.FeedlyUrl),
                    AlexaRanking = string.IsNullOrEmpty(blog.Url) ? 0 : _alexaInquisitor.GetGlobalRank(blog.Url),
                    DomainAuthority = mozStats.DomainAuthority,
                    LinkingRootDomains = mozStats.LinkingDomains,
                    Recorded = TimeRecordingStrategy()
                };
            }
        }
    }
}
