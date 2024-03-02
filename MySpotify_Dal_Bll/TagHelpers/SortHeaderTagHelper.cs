using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MySpotify.Models;

namespace MySpotify.TagHelpers
{
    public class SortHeaderTagHelper : TagHelper
    {
        public SortState Property { get; set; } //текущее свойство. для которого создается тег

        public SortState Current { get; set; } // активное свойство, выбранное для сортировки

        public string? Action { get; set; } // действие контроллеоа, для которого создается ссылка

        public bool Up { get; set; } //сортировка по возрастанию или убыванию


        [ViewContext]
        public ViewContext ViewContext { get; set; } = null!;

        IUrlHelperFactory urlHelperFactory;
        public SortHeaderTagHelper(IUrlHelperFactory HelperFactory)
        {
            urlHelperFactory = HelperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            output.TagName = "a";
            string? url = urlHelper.Action(Action, new { sortOrder = Property });
            output.Attributes.SetAttribute("href", url);
            //если текущее свойство имеет значение CurrentSort
            if (Current == Property)
            {
                TagBuilder tag = new TagBuilder("i");
                tag.AddCssClass("glyphicon");

                if (Up == true)  // сорт по возрастанию
                {
                    tag.AddCssClass("glyphicon-chevron-up");
                }
                else // убывание
                {
                    tag.AddCssClass("glyphicon-chevron-down");
                }
                output.PreContent.AppendHtml(tag);

            }

        }
    }
}
