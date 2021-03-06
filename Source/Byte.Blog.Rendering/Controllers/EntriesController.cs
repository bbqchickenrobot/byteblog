﻿using System.Web.Mvc;
using Byte.Blog.Framework.Data;
using Byte.Blog.Rendering.Models;
using Raven.Client;

namespace Byte.Blog.Rendering.Controllers
{
    public class EntriesController : RavenController
    {
        public EntriesController(IDocumentStore documentStore)
            : base(documentStore)
        {
        }

        public ActionResult ViewEntry(string pageSlug, string entrySlug)
        {
            var pageFactory = new PageFactory(this.session);
            var page = pageFactory.CreateFromSlug(pageSlug);
            if (page == null)
            {
                return new HttpNotFoundResult("No such page.");
            }

            var entryFactory = new EntryFactory(this.session);
            var entry = entryFactory.CreateFromSlug(entrySlug);
            if (entry == null)
            {
                return new HttpNotFoundResult("No such entry.");
            }

            var entryToEntryViewModelMapper = new EntryToEntryViewModelMapper(this.session, this.Url);
            var entryViewModel = entryToEntryViewModelMapper.Map(entry);

            entryViewModel.PageSlug = pageSlug;

            return this.View(entryViewModel);
        }
    }
}
