using FinControlCore6.ViewModels;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace FinControlCore6.TagHelpers
{
    public class TableTagHelper:TagHelper
    {
        public IndexViewModel IndexViewModel { get; set; } = null;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "table";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("class", "display");
        }
        
        //void buildHead(TagHelperOutput output)
        //{
        //    StringBuilder stringBuilder = new StringBuilder();
        //    stringBuilder.
        //    string headContent = "";
        //    headContent

        //}
    }
}
