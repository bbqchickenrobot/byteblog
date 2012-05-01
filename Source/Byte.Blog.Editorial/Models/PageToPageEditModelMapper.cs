using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Byte.Blog.Content;
using Raven.Client;

namespace Byte.Blog.Editorial.Models
{
    public class PageToPageEditModelMapper
    {
        private readonly IDocumentSession session;

        public PageToPageEditModelMapper(IDocumentSession session)
        {
            this.session = session;
        }

        public PageEditModel Map(Page page)
        {
            var pageEditModel = Mapper.Map<PageEditModel>(page);

            pageEditModel.PossibleHtmlColors = this.GetPossibleHtmlColors(page.HtmlColor);

            return pageEditModel;
        }

        private IEnumerable<SelectListItem> GetPossibleHtmlColors(string currentHexColor)
        {
            return HtmlColor.AllColors.Select(color =>
                new SelectListItem
                {
                    Text = color.FriendlyName,
                    Value = color.HexColor,
                    Selected = color.HexColor == currentHexColor ? true : false
                });
        }
    }
}