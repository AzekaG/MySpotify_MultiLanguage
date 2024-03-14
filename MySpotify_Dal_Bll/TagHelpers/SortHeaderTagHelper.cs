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
		public SortState Property { get; set; } //текущее свойство
		public SortState Current { get; set; }	/// <summary>
		/// значение активного свойства
		/// </summary>
		public string? Action {  get; set; }//действие контроллера
		public bool Up {  get; set; }   //по возрастанию или убыванию

		//Тег хелпер не знает в какой вьюшке мы будем его использовать , 
		//Сюда инжектируется ссылка на вьюшку , на которой применяется тег- хелпер
		[ViewContext]
		public ViewContext ViewContext { get; set; } = null!;

		IUrlHelperFactory UrlHelperFactory;
		public SortHeaderTagHelper(IUrlHelperFactory helperFactory)
		{
			UrlHelperFactory = helperFactory;
		}

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			//получаем хэлпер который создает маршрут , но нужно передать контекст (ViewContext)
			IUrlHelper urlHelper = UrlHelperFactory.GetUrlHelper(ViewContext);
			output.TagName = "a";
			string? url = urlHelper.Action(Action, new { sortOrder = Property }); //сортОрдер - параметр, который в адреснйо строке будет идти после ?
	
		
			output.Attributes.SetAttribute("href", url);

			//если текущее свойство имеет значение CurrentSort
			//где совпадение прозошло - устанавливаем галочку вверх или вниз. 
			if(Current == Property)
			{
				TagBuilder tag = new TagBuilder("i");
				tag.AddCssClass("glyphicon");

				if (Up == true)
					tag.AddCssClass("glyphicon-chevron-up");
				else
					tag.AddCssClass("glyphicon-chevron-down");
				output.PreContent.AppendHtml(tag);
			}

		}



	}
}
