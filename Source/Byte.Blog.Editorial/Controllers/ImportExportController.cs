using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Byte.Blog.Framework.Data;
using Raven.Client;

namespace Byte.Blog.Editorial.Controllers
{
    [Authorize]
    public class ImportExportController : RavenController
    {
        public ImportExportController(IDocumentStore store)
            : base(store)
        {
        }

        public ActionResult FromWordpress()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult ImportWordpressXml(HttpPostedFileBase wordpressXml)
        {
            if(wordpressXml == null || wordpressXml.ContentLength <= 0)
            {
                return this.Json(new { error = "No wordpress xml found in uploaded file!" });
            }

            var document = XDocument.Load(wordpressXml.InputStream);

            var wordpressImporter = new WordpressImporter(this.session);
            wordpressImporter.ImportFromXml(document);

            var statistics = wordpressImporter.GetStatistics();

            return this.Json(statistics);
        }
    }
}
