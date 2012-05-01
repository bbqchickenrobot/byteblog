﻿using System.Linq;
using System.Web.Mvc;
using Byte.Blog.Content;
using Byte.Blog.Framework.Data;
using Byte.Blog.Rendering.Models;
using Raven.Client;

namespace Byte.Blog.Rendering.Controllers
{
    public class NavigationController : RavenController
    {
        public NavigationController(IDocumentStore documentStore)
            : base(documentStore)
        {
        }

        public ActionResult Menu()
        {
            var pages = this.session.Query<Page>()
                .Where(p => p.Deleted == false)
                .ToList();

            pages.Insert(0, Page.HomePage);

            var mapper = new PageToPageViewModelMapper();
            var pageViewModels = pages.Select(mapper.Map);

            return this.View(pageViewModels);
        }
    }
}