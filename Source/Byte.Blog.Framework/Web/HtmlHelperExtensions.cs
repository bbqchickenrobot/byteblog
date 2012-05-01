using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Byte.Blog.Framework.Web
{
    public static class HtmlHelperExtensions
    {
        public static string IdFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression)
        {
            var htmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(
                ExpressionHelper.GetExpressionText(expression));

            return HtmlHelper.GenerateIdFromName(htmlFieldName);
        }

        //is present in mvc 4, but not mvc 3
        public static MvcHtmlString LabelFor<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            object htmlAttributes = null)
        {
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            string resolvedLabelText = 
                metadata.DisplayName ?? 
                metadata.PropertyName ?? 
                htmlFieldName.Split('.').Last();

            if (String.IsNullOrEmpty(resolvedLabelText))
            {
                return MvcHtmlString.Empty;
            }

            var tagBuilder = new TagBuilder("label");

            tagBuilder.Attributes.Add("for", TagBuilder.CreateSanitizedId(
                html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)));

            tagBuilder.SetInnerText(resolvedLabelText);

            var htmlAttributesDict = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            tagBuilder.MergeAttributes(htmlAttributesDict, replaceExisting: true);

            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }
    }
}
