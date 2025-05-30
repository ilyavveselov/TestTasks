using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using Task3.Controllers;
using Task3.Models;
using Task3.Repositories;

namespace Task3.RazorHelpers
{
    public static class PizzaHelper
    {
        public static List<int> GetAvailableSizes() => new List<int> { 30, 40, 60 };
        public static List<string> GetAvailableTypes() => new List<string> { "Традиционное", "Толстое" };
        public static (int activeSize, string activeType) GetActiveSelectors(this IHtmlHelper html, PizzaModel model)
        {
            var availableSizes = GetAvailableSizes();
            var availableTypes = GetAvailableTypes();

            var activeSize = model.Sizes.Contains(availableSizes[0])
                ? availableSizes[0]
                : model.Sizes.FirstOrDefault(size => availableSizes.Contains(size));

            var activeType = model.Types?.Contains(availableTypes[0]) == true
                ? availableTypes[0]
                : model.Types?.FirstOrDefault(type => availableTypes.Contains(type));

            return (activeSize, activeType);
        }

        public static IHtmlContent RenderSelectors<T>
        (
            this IHtmlHelper htmlHelper,
            List<T> referenceArray,
            List<T> selectorData,
            T selectedSelector,
            bool isSizeSelector
        )
        {
            var resultHtml = new StringBuilder();
            foreach (var item in referenceArray)
            {
                if (selectorData.Contains(item))
                {
                    var activeClass = item.Equals(selectedSelector) ? "active" : "";
                    resultHtml.Append($"<span class=\"size {activeClass}\">{item} {(isSizeSelector ? "см" : "")}</span>");
                }
                else
                {
                    resultHtml.Append($"<span class=\"size disable\">{item} {(isSizeSelector ? "см" : "")}</span>");
                }
            }
            return new HtmlString(resultHtml.ToString());
        }
    }
}