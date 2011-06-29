using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using System.Linq.Expressions;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace CollectionEditing.Infrastructure
{
    public static class JsonHtmlExtensions
    {
        public static MvcHtmlString Json<TModel, TObject>(this HtmlHelper<TModel> html, TObject obj)
        {
            return MvcHtmlString.Create(JsonConvert.SerializeObject(obj));
        }
    }
}

