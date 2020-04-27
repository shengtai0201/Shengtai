using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Shengtai.Web.Telerik.Http
{
    public abstract class ApiController<TKey, TModel> : ApiController
        where TKey : IComparable<TKey>, IEquatable<TKey>//IComparable, IConvertible
        where TModel : ViewModel<TKey>
    {
        private readonly IApiService<TKey, TModel, IPrincipal> service;

        public ApiController(IApiService<TKey, TModel, IPrincipal> service)
        {
            service.CurrentUser = this.User ?? HttpContext.Current?.User;
            this.service = service;
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(TKey key)
        {
            bool? result = await this.service.DestroyAsync(key);

            if (result == null)
                return this.NotFound();
            else
            {
                if (result.Value)
                    return this.StatusCode(HttpStatusCode.NoContent);
                else
                    return this.InternalServerError();
            }
        }

        [HttpGet]
        public async Task<IDataSourceResponse<TModel>> Get([ModelBinder(typeof(DataSourceRequestModelBinder))]DataSourceRequest request)
        {
            return await this.service.ReadAsync(request);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post(TModel model)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            var response = new DataSourceResponse<TModel> { DataCollection = new List<TModel> { model }, TotalRowCount = 1 };
            bool result = await this.service.CreateAsync(model, response);

            if (result)
                return Request.CreateResponse<IDataSourceResponse<TModel>>(HttpStatusCode.Created, response);
            else
                return Request.CreateResponse<IDataSourceResponse<TModel>>(HttpStatusCode.InternalServerError, response);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Put(TKey key, TModel model)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            var response = new DataSourceResponse<TModel> { DataCollection = new List<TModel> { model }, TotalRowCount = 1 };
            bool? result = await this.service.UpdateAsync(key, model, response);

            if (result == null)
                return Request.CreateResponse<IDataSourceResponse<TModel>>(HttpStatusCode.NotFound, response);
            else
            {
                if (result.Value)
                    return Request.CreateResponse<IDataSourceResponse<TModel>>(HttpStatusCode.OK, response);
                else
                    return Request.CreateResponse<IDataSourceResponse<TModel>>(HttpStatusCode.InternalServerError, response);
            }
        }

        protected async override void Dispose(bool disposing)
        {
            if (disposing)
                await this.service.DisposeAsync();

            base.Dispose(disposing);
        }
    }
}