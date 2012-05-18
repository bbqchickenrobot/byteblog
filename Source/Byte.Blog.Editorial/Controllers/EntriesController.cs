using System.Web.Mvc;
using Byte.Blog.Content;
using Byte.Blog.Editorial.Models;
using Byte.Blog.Framework;
using Byte.Blog.Framework.Data;
using Microsoft.Web.Mvc;
using Raven.Client;

namespace Byte.Blog.Editorial.Controllers
{
    [Authorize]
    public class EntriesController : RavenController
    {
        public EntriesController(IDocumentStore documentStore)
            : base(documentStore)
        {
        }

        public ActionResult New()
        {
            var entry = new Entry();

            var mapper = new EntryToEntryEditModelMapper(this.session);
            var entryEditModel = mapper.Map(entry);

            return this.View("Edit", entryEditModel);
        }

        public ActionResult Edit(string entryId)
        {
            entryId = entryId.EnsurePrefix(Entry.IdPrefix);

            var entry = this.session.Load<Entry>(entryId);
            if(entry == null)
            {
                this.ModelState.AddModelError("No Such Entry", "There was no entry found matching the id.");

                return this.View(new EntryEditModel());
            }

            var mapper = new EntryToEntryEditModelMapper(this.session);
            var entryEditModel = mapper.Map(entry);

            return this.View(entryEditModel);
        }

        [AjaxOnly]
        public ActionResult Save(EntryEditModel editModel)
        {
            if(!this.ModelState.IsValid)
            {
                return this.Json(new { error = true });
            }

            var isNewEntry = IsNewEntry(editModel);

            Entry entry;

            if (isNewEntry)
            {
                entry = new Entry();
            }
            else
            {
                entry = this.session.Load<Entry>(editModel.Id);
                if (entry == null)
                {
                    return this.Json(new { error = true });
                }
            }

            this.MapEntry(entry, editModel);

            if (isNewEntry)
            {
                this.session.Store(entry);
            }

            this.session.SaveChanges();

            return this.Json(entry);
        }

        private static bool IsNewEntry(EntryEditModel editModel)
        {
            if (string.IsNullOrEmpty(editModel.Id))
            {
                return true;
            }

            return false;
        }

        private void MapEntry(Entry entry, EntryEditModel editModel)
        {
            var mapper = new EntryEditModelToEntryMapper(this.session);
            mapper.Map(entry, editModel);
        }

        //TODO: 'REST' 'DELETE' ?
        [HttpPost]
        public ActionResult Delete(string entryId)
        {
            entryId = entryId.EnsurePrefix(Entry.IdPrefix);

            var entry = this.session.Load<Entry>(entryId);
            entry.Deleted = true;

            this.session.SaveChanges();

            return this.Json(new { });
        }
    }
}
