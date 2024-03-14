using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MySpotify.Models;

namespace MySpotify.TagHelpers
{
    public class PageLinkTagHelper : TagHelper
    {
        IUrlHelperFactory urlHelperFactory;

        //передаем ссылку на фабрику. которая будет создаать url хэлпер, с помощью которого мы создаим маршрут.
        ////по которому будет происходит переход при клике по одной из гиперссылок.  
        public PageLinkTagHelper(IUrlHelperFactory UrlHelperFactory)
        {
            urlHelperFactory = UrlHelperFactory;
        }


        //получаем url контаксте, чтобы сформировать  наши ссылки, 
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        
        //тут будут зранится данные для пагинации
        public MediaPaginationModel? PageModel { get; set; }

        public string PageAction { get; set; } = "";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
           if(PageModel == null) 
            {
                throw new Exception("Page model is'n set");
            }


            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "div";


            //набор ссылок будет представлять собой список ul
            TagBuilder tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");

            //формируем 3 ссылки - текущая, предыдущая и следующая
            TagBuilder currentItem = CreateTag(urlHelper, PageModel.PageNumber);

            //ссылка на предыдущую страницу 
            if(PageModel.HasPreviousPage)
            {
                TagBuilder prevItem = CreateTag(urlHelper, PageModel.PageNumber - 1);
                tag.InnerHtml.AppendHtml(prevItem);
            }
            tag.InnerHtml.AppendHtml(currentItem);
            //создаем ссылку на следующую страницу , если она есть
            if(PageModel.HasNextPage)
            {
                TagBuilder prevItem = CreateTag(urlHelper, PageModel.PageNumber + 1);
                tag.InnerHtml.AppendHtml(prevItem);
            }
            output.Content.AppendHtml(tag);

        }




        public TagBuilder CreateTag(IUrlHelper urlHelper , int pageNumber = 1)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a"); //должен быть дочерним по отношению к li
          
            if(pageNumber == PageModel?.PageNumber)
            {
                item.AddCssClass("active");
            }
            else
            {
                link.Attributes["href"] = urlHelper.Action(PageAction, new { page = pageNumber });
            }
            item.AddCssClass("page-item");
            link.AddCssClass("page-link");
            link.InnerHtml.Append(pageNumber.ToString());
            item.InnerHtml.AppendHtml(link);
            return item;

        }


    }
}
