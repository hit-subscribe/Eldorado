﻿using ElDorado.Console.Trello;
using ElDorado.Domain;
using ElDorado.Gui.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Telerik.JustMock;
using Telerik.JustMock.EntityFramework;
using Telerik.JustMock.Helpers;

namespace ElDorado.Gui.Tests.PostRefreshesControllerTests
{
    [TestClass]
    public class When_Performing_CRUD_Opeartions_On_Post_Refreshes_PostRefreshesController_Should
    {
        private const int BlogPostId = 1;

        private BlogPost Post = new BlogPost() { Id = BlogPostId };
        private PostRefresh Refresh = new PostRefresh() { Id = 11, BlogPostId = BlogPostId };
        private Author Author = new Author() { };

        private ITrelloSynchronizer<PostRefresh> Synchronizer = Mock.Create<ITrelloSynchronizer<PostRefresh>>();

        private BlogContext Context { get; } = EntityFrameworkMock.Create<BlogContext>();

        private PostRefreshesController Target { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            Context.PostRefreshes.Add(Refresh);
            Context.Authors.Add(Author);

            Target = new PostRefreshesController(Context, Synchronizer) { MapPath = "somepath" };
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_All_Refreshes_In_Index()
        {
            var originalRefreshCount = Context.PostRefreshes.Count();

            var additionalRefreshes = new List<PostRefresh>() { new PostRefresh() };
            Context.PostRefreshes.AddRange(additionalRefreshes);

            var refreshes = Target.Index().GetResult<IEnumerable<PostRefresh>>();

            refreshes.Count().ShouldBe(originalRefreshCount + additionalRefreshes.Count());
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Sort_Results_By_DraftDate()
        {
            const int earliestRefreshId = 12;
            Refresh.DraftDate = new DateTime(2019, 4, 1);
            Context.PostRefreshes.Add(new PostRefresh() { DraftDate = new DateTime(2019, 1, 1), Id = earliestRefreshId });

            var refreshes = Target.Index().GetResult<IEnumerable<PostRefresh>>();

            refreshes.First().Id.ShouldBe(earliestRefreshId);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_No_Results_When_BlogPostId_Is_Specified_But_Matches_Nothing()
        {
            var refreshes = Target.Index(BlogPostId + 1).GetResult<IEnumerable<PostRefresh>>();

            refreshes.ShouldBeEmpty();
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_A_Result_When_BlogPostId_Matches()
        {
            var refreshes = Target.Index(BlogPostId).GetResult<IEnumerable<PostRefresh>>();

            refreshes.ShouldNotBeEmpty();
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Create_A_Post_With_BlogId_Set()
        {
            var refresh = Target.Create(BlogPostId).GetResult<PostRefresh>();

            refresh.BlogPostId.ShouldBe(BlogPostId);
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Invoke_The_TrelloService_Initialize_On_Create()
        {
            Synchronizer.Arrange(ts => ts.Initialize(Arg.AnyString));

            Target.Create(Refresh);

            Synchronizer.Assert(ts => ts.Initialize(Arg.AnyString));
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Add_A_Trello_Card_On_Create()
        {
            Synchronizer.Arrange(rs => rs.CreateCardForEntity(Arg.IsAny<PostRefresh>()));

            Target.Create(Refresh);

            Synchronizer.Assert(rs => rs.CreateCardForEntity(Arg.IsAny<PostRefresh>()));
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Add_Authors_To_ViewBag_On_Create()
        {
            var result = Target.Create(BlogPostId);

            ((IEnumerable<Author>)result.ViewBag.Authors).ShouldNotBeEmpty();
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Add_Authors_To_ViewBag_On_Edit()
        {
            var result = Target.Edit(11);

            ((IEnumerable<Author>)result.ViewBag.Authors).ShouldNotBeEmpty();
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Add_Authors_To_The_ViewBag_On_Edit_Postback()
        {
            var result = Target.Edit(Refresh);

            ((IEnumerable<Author>)result.ViewBag.Authors).ShouldNotBeEmpty();
        }

    [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Invoke_The_TrelloService_Initialize_On_Edit()
        {
            Synchronizer.Arrange(ts => ts.Initialize(Arg.AnyString));

            Target.Edit(Refresh);

            Synchronizer.Assert(ts => ts.Initialize(Arg.AnyString));
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Edit_Trello_Card_On_Edit()
        {
            Synchronizer.Arrange(rs => rs.UpdateCardForEntity(Arg.IsAny<PostRefresh>()));

            Target.Edit(Refresh);

            Synchronizer.Assert(rs => rs.UpdateCardForEntity(Arg.IsAny<PostRefresh>()));
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Initialize_TrelloService_On_Delete()
        {
            Synchronizer.Arrange(ts => ts.Initialize(Arg.AnyString));

            Target.Delete(Refresh.Id);

            Synchronizer.Assert(ts => ts.Initialize(Arg.AnyString));
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Delete_In_Trello_Service_On_Delete()
        {
            const string trelloId = "asdf";
            Refresh.TrelloId = trelloId;

            Synchronizer.Arrange(ts => ts.DeleteCard(Arg.AnyString));

            Target.Delete(Refresh.Id);

            Synchronizer.Assert(ts => ts.DeleteCard(trelloId));
        }

        [TestMethod, Owner("ebd"), TestCategory("Proven"), TestCategory("Unit")]
        public void Return_A_Redirect_With_BlogPostId_Set_On_Delete()
        {
            var result = Target.Delete(Refresh.Id) as RedirectToRouteResult;
            
            result.RouteValues["blogPostId"].ShouldBe(BlogPostId);
        }
}
}
