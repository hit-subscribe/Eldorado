﻿using ElDorado.Domain;
using ElDorado.Gui.ViewModels;
using ElDorado.Trello;
using ElDorado.Wordpress;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElDorado.Gui.Controllers
{
    public class BlogPostsController : Controller
    {
        private readonly BlogContext _blogContext;
        private readonly TrelloWritingCalendarService _trelloService;
        private readonly WordpressService _wordpressService;

        public string MapPath { get; set; }

        public BlogPostsController(BlogContext blogContext, TrelloWritingCalendarService trelloService, Wordpress.WordpressService wordpressService)
        {
            _wordpressService = wordpressService;
            _trelloService = trelloService;
            _blogContext = blogContext;
        }

        public ActionResult Index(int blogId = 0, int authorId = 0, DateTime? draftDate = null, bool includeAll = false)
        {
            var postsFromEvaluatedDatabaseQuery = _blogContext.BlogPosts.ToList(); //I like the readability from these filtering methods we're using, and the cost tradeoff here just doesn't matter

            var filteredPosts = postsFromEvaluatedDatabaseQuery.Where(bp => bp.BlogId.DefaultMatches(blogId) && bp.AuthorId.DefaultMatches(authorId) && bp.DraftDate.DefaultMatches(draftDate));
            var currentPosts = filteredPosts.Where(bp => includeAll || (!bp.IsHitSubscribeFinished && bp.IsApproved));
            var orderedPosts = currentPosts.OrderBy(p => p.DraftDate);

            return View(new BlogPostIndexViewModel(orderedPosts, _blogContext));
        }

        public ActionResult Create(int blogId = 0)
        {
            return View(new BlogPostEditViewModel(new BlogPost() { BlogId = blogId }, _blogContext));
        }

        [HttpPost]
        public ActionResult Create(BlogPost post, string createNew = null)
        {
            _blogContext.BlogPosts.Add(post);
            _blogContext.SaveChanges();
            _blogContext.UpdateBlogPostDependencies(post);

            InitializeTrelloService();
            _trelloService.AddCard(post);

            post.CalculateAuthorPay();

            SyncToWordpress(post);

            _blogContext.SaveChanges();

            return RedirectToAppropriatePage(post, createNew);
        }

        private ActionResult RedirectToAppropriatePage(BlogPost post, string createNew)
        {
            if (createNew == "Create and Add Another")
                return RedirectToAction("Create", new { blogId = post.BlogId });

            return RedirectToAction("Edit", new { postId = post.Id });
        }

        public ActionResult Edit(int postId)
        {
            return View(GetViewModelForId(postId));
        }


        [HttpPost]
        public ActionResult Edit(BlogPostEditViewModel blogPostViewModel)
        {
            _blogContext.BlogPosts.Attach(blogPostViewModel.Post);
            _blogContext.SetModified(blogPostViewModel.Post);
            _blogContext.UpdateBlogPostDependencies(blogPostViewModel.Post);
            blogPostViewModel.SetAuthorPay();
            _blogContext.SaveChanges();
            SyncToWordpress(blogPostViewModel.Post);
            
            InitializeTrelloService();
            _trelloService.EditCard(blogPostViewModel.Post);

            return RedirectToAction("Edit", new { postId = blogPostViewModel.Post.Id });
        }

        public ActionResult Delete(int postId)
        {
            var post = _blogContext.BlogPosts.First(p => p.Id == postId);
            string trelloId = post.TrelloId;

            _blogContext.BlogPosts.Remove(post);
            _blogContext.SaveChanges();

            InitializeTrelloService();
            _trelloService.DeleteCard(trelloId);

            _wordpressService.AuthorizeUser(MapPath ?? Server.MapPath(@"~/App_Data/wordpress.cred"));
            _wordpressService.DeleteFromWordpress(post);

            return RedirectToAction("Index");
        }

        private void SyncToWordpress(BlogPost post)
        {
            _wordpressService.AuthorizeUser(MapPath ?? Server.MapPath(@"~/App_Data/wordpress.cred"));
            _wordpressService.SyncToWordpress(post);
        }

        private BlogPostEditViewModel GetViewModelForId(int id)
        {
            var blogPost = _blogContext.BlogPosts.First(bp => bp.Id == id);
            return new BlogPostEditViewModel(blogPost, _blogContext);
        }

        private void InitializeTrelloService()
        {
            _trelloService.Initialize(MapPath ?? Server.MapPath(@"~/App_Data/trello.cred"));
        }
    }
}