using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupermarketCheckout.Core.Entities;
using SupermarketCheckout.Core.Interfaces;

namespace SupermarketCheckout.WebAplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkuController : ControllerBase
    {

        private readonly IAsyncRepository<Sku> _skuRepository;

        public SkuController(IAsyncRepository<Sku> skuRepository)
        {
            _skuRepository = skuRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sku>>> GetSkus() {
            var skus = await _skuRepository.ListAllAsync();
            return skus.ToList();
        }

    }
}
