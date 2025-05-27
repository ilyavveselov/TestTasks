using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace Task2.RazorHelpers
{
    public static class PizzaHelper
    {
        public static IHtmlContent RenderSelectors<T>
            (
            this IHtmlHelper htmlHelper,
            List<T> referenceArray,
            List<T> selectorData,
            T selectedSelector
            )
        {
            var resultHtml = new StringBuilder();
            foreach (var item in referenceArray)
            {
                if (selectorData.Contains(item))
                {
                    var activeClass = item.Equals(selectedSelector) ? "active" : "";
                    resultHtml.Append($"<span class=\"size {activeClass}\">{item}</span>");
                }
                else
                {
                    resultHtml.Append($"<span class=\"size disable\">{item}</span>");
                }
            }
            return new HtmlString(resultHtml.ToString());
        }
    }
}