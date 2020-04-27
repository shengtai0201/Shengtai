﻿using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace Shengtai.Web.Telerik.Http
{
    public class DataSourceRequestModelBinder : ModelBinder, IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException();

            if (bindingContext.ModelType != typeof(DataSourceRequest))
                return false;

            DataSourceRequest request = new DataSourceRequest
            {
                ServerPaging = this.GetServerPaging(bindingContext)
            };

            var logic = bindingContext.ValueProvider.GetValue("filter.logic");
            if (logic != null)
            {
                var filterInfoCollection = new ServerFilterInfo { Logic = this.ParseLogic(logic.AttemptedValue) };
                this.SetServerFiltering(bindingContext, filterInfoCollection, "filter.filters[{0}]", 0);

                if (filterInfoCollection.FilterCollection.Count > 0)
                    request.ServerFiltering = filterInfoCollection;
            }

            request.ServerSorting = this.SetServerSorting(bindingContext, null, 0);

            bindingContext.Model = request;

            return true;
        }

        private ServerPageInfo GetServerPaging(ModelBindingContext bindingContext)
        {
            var skip = bindingContext.ValueProvider.GetValue("skip");
            var take = bindingContext.ValueProvider.GetValue("take");
            if (skip != null && take != null)
            {
                var pageInfo = new ServerPageInfo
                {
                    Skip = Convert.ToInt32(skip.AttemptedValue),
                    Take = Convert.ToInt32(take.AttemptedValue)
                };

                var page = bindingContext.ValueProvider.GetValue("page");
                pageInfo.Page = string.IsNullOrEmpty(page.AttemptedValue) ? 1 : Convert.ToInt32(page.AttemptedValue);

                var pageSize = bindingContext.ValueProvider.GetValue("pageSize");
                pageInfo.PageSize = string.IsNullOrEmpty(pageSize.AttemptedValue) ? 1 : Convert.ToInt32(pageSize.AttemptedValue);

                return pageInfo;
            }

            return null;
        }

        private ServerFilterInfo SetServerFiltering(ModelBindingContext bindingContext, ServerFilterInfo filterInfoCollection, string format, int index)
        {
            string baseKey = string.Format(format, index++);
            var field = bindingContext.ValueProvider.GetValue(baseKey + ".field"); // 用以判別單一 filter
            var subLogic = bindingContext.ValueProvider.GetValue(baseKey + ".logic");  // 用以判別 filter 集合

            if (field != null)
            {
                var value = bindingContext.ValueProvider.GetValue(baseKey + ".value");
                var @operator = bindingContext.ValueProvider.GetValue(baseKey + ".operator");
                if (@operator != null)
                {
                    filterInfoCollection.FilterCollection.Add(new ServerFilterInfo
                    {
                        Operator = this.ParseOperator(@operator.AttemptedValue),
                        Field = field.AttemptedValue,
                        Value = value == null ? string.Empty : value.AttemptedValue
                    });

                    this.SetServerFiltering(bindingContext, filterInfoCollection, format, index);
                }
            }
            else if (subLogic != null)
            {
                var subFilterInfoCollection = new ServerFilterInfo { Logic = this.ParseLogic(subLogic.AttemptedValue) };
                filterInfoCollection.FilterCollection.Add(this.SetServerFiltering(bindingContext, subFilterInfoCollection, baseKey + ".filters[{0}]", 0));
            }

            return filterInfoCollection;
        }

        private ICollection<ServerSortInfo> SetServerSorting(ModelBindingContext bindingContext, ICollection<ServerSortInfo> sortInfoCollection, int index)
        {
            string baseKey = string.Format("sort[{0}]", index++);
            var field = bindingContext.ValueProvider.GetValue(baseKey + "[field]");
            var dir = bindingContext.ValueProvider.GetValue(baseKey + "[dir]");

            if (field != null)
            {
                if (sortInfoCollection == null)
                    sortInfoCollection = new List<ServerSortInfo>();

                sortInfoCollection.Add(new ServerSortInfo
                {
                    Field = field.AttemptedValue,
                    Dir = this.ParseDir(dir.AttemptedValue)
                });

                this.SetServerSorting(bindingContext, sortInfoCollection, index);
            }

            return sortInfoCollection;
        }
    }
}