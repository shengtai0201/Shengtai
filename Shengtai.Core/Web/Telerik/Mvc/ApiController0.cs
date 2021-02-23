using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shengtai.Web.Telerik.Mvc
{
    public abstract class ApiReadController<TKey, TModel> : ControllerBase where TModel : class
    {
        private readonly IApiReadService<TKey, TModel> _service;
        protected ApiReadController(IApiReadService<TKey, TModel> service)
        {
            _service = service;
        }

        private async Task<IActionResult> GetAsync(DataSourceRequest request)
        {
            IDataSourceResponse<TModel> response = await _service.ReadAsync(request);
            return Ok(response);
        }

        private async Task<IActionResult> GetAsync(TKey key)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TModel model = await _service.ReadAsync(key);

            if (model == null)
                return this.NotFound();
            else
                return Ok(new DataSourceResponse<TModel> { DataCollection = new List<TModel> { model }, TotalRowCount = 1 });
        }

        protected virtual bool IsKeyNull(TKey key)
        {
            if (key == null)
                return true;

            return default(TKey).Equals(key);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] TKey key, [ModelBinder] DataSourceRequest request)
        {
            if (this.IsKeyNull(key))
                return await this.GetAsync(request);
            else
                return await this.GetAsync(key);
        }
    }

    public abstract class ApiUpdateController<TKey, TModel> : ApiReadController<TKey, TModel> where TModel : class
    {
        private readonly IApiUpdateService<TKey, TModel> _service;
        protected ApiUpdateController(IApiUpdateService<TKey, TModel> service) : base(service)
        {
            _service = service;
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromQuery]TKey key, [FromForm] TModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = new DataSourceResponse<TModel> { DataCollection = new List<TModel> { model }, TotalRowCount = 1 };
            bool? result = await _service.UpdateAsync(key, model, response);

            if (result == null)
                return NotFound(response);
            else
            {
                if (result.Value)
                    return this.Ok(response);
                else
                    return this.StatusCode(500, response);
            }
        }
    }

    public abstract class ApiDestroyController<TKey, TModel> : ApiReadController<TKey, TModel> where TModel : class
    {
        private readonly IApiDestroyService<TKey, TModel> _service;
        protected ApiDestroyController(IApiDestroyService<TKey, TModel> service) : base(service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostAsync([FromForm] TModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = new DataSourceResponse<TModel> { DataCollection = new List<TModel> { model }, TotalRowCount = 1 };
            bool result = await _service.CreateAsync(model, response);

            if (result)
                return Ok(response);
            else
                return this.StatusCode(500, response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] TKey key)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool? result = await _service.DestroyAsync(key);

            if (result == null)
                return this.NotFound();
            else
            {
                if (result.Value)
                    return Ok(new DataSourceResponse<TModel>());
                else
                    return this.StatusCode(500, new { key });
            }
        }
    }

    public abstract class ApiController<TKey, TModel> : ApiReadController<TKey, TModel> where TModel : class
    {
        private readonly IApiService<TKey, TModel> _service;
        protected ApiController(IApiService<TKey, TModel> service) : base(service)
        {
            _service = service;
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromQuery] TKey key, [FromForm] TModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = new DataSourceResponse<TModel> { DataCollection = new List<TModel> { model }, TotalRowCount = 1 };
            bool? result = await _service.UpdateAsync(key, model, response);

            if (result == null)
                return NotFound(response);
            else
            {
                if (result.Value)
                    return this.Ok(response);
                else
                    return this.StatusCode(500, response);
            }
        }

        [HttpPost]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostAsync([FromForm] TModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = new DataSourceResponse<TModel> { DataCollection = new List<TModel> { model }, TotalRowCount = 1 };
            bool result = await _service.CreateAsync(model, response);

            if (result)
                return Ok(response);
            else
                return this.StatusCode(500, response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] TKey key)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool? result = await _service.DestroyAsync(key);

            if (result == null)
                return this.NotFound();
            else
            {
                if (result.Value)
                    return Ok(new DataSourceResponse<TModel>());
                else
                    return this.StatusCode(500, new { key });
            }
        }
    }
}