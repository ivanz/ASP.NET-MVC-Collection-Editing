using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using System.Linq.Expressions;
using System.Collections.Specialized;

namespace CollectionEditing.Infrastructure
{
    public static class CollectionEditingHtmlExtensions
    {
        public static IDisposable BeginCollectionItem<TModel>(this HtmlHelper<TModel> html, string collectionName)
        {
            string collectionIndexFieldName = String.Format("{0}.Index", collectionName);
            string itemIndex = GetCollectionItemIndex(collectionIndexFieldName);
            string collectionItemName = String.Format("{0}[{1}]", collectionName, itemIndex);

            TagBuilder indexField = new TagBuilder("input");
            indexField.MergeAttributes(new Dictionary<string, string>() {
                { "name", collectionIndexFieldName },
                { "value", itemIndex },
                { "type", "hidden" },
                { "autocomplete", "off" }
            });

            html.ViewContext.Writer.WriteLine(indexField.ToString(TagRenderMode.SelfClosing));
            return new CollectionItemNamePrefixScope(html.ViewData.TemplateInfo, collectionItemName);
        }

        private static string GetCollectionItemIndex(string collectionIndexFieldName)
        {
            Queue<string> previousIndices = (Queue<string>) HttpContext.Current.Items[collectionIndexFieldName];
            if (previousIndices == null) {
                HttpContext.Current.Items[collectionIndexFieldName] = previousIndices = new Queue<string>();

                string previousIndicesValues = HttpContext.Current.Request[collectionIndexFieldName];
                if (!String.IsNullOrWhiteSpace(previousIndicesValues)) {
                    foreach (string index in previousIndicesValues.Split(','))
                        previousIndices.Enqueue(index);
                }
            }

            return previousIndices.Count > 0 ? previousIndices.Dequeue() : Guid.NewGuid().ToString();
        }

        private class CollectionItemNamePrefixScope : IDisposable
        {
            private readonly TemplateInfo _templateInfo;
            private readonly string _previousPrefix;

            public CollectionItemNamePrefixScope(TemplateInfo templateInfo, string collectionItemName)
            {
                this._templateInfo = templateInfo;

                _previousPrefix = templateInfo.HtmlFieldPrefix;
                templateInfo.HtmlFieldPrefix = collectionItemName;
            }

            public void Dispose()
            {
                _templateInfo.HtmlFieldPrefix = _previousPrefix;
            }
        }
    }


}

