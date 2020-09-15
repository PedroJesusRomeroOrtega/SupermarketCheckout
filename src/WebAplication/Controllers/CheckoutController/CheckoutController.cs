using Microsoft.AspNetCore.Mvc;
using SupermarketCheckout.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketCheckout.WebAplication.Controllers.CheckoutController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;
        private readonly ISkuService _skuService;

        public CheckoutController(ICheckoutService checkoutService,
            ISkuService skuService)
        {
            _checkoutService = checkoutService;
            _skuService = skuService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheckoutDto>>> GetCheckouts()
        {
            var checkouts = await _checkoutService.GetCheckouts();
            //TODO: add automapper
            //TODO: calculate totalprice
            var checkoutsDto = checkouts.Select(c => new CheckoutDto() { Id = c.Id, Date = c.Date, TotalPrice = 0m });
            return checkoutsDto.ToList();
        }

        [HttpGet("GetOrCreate/{checkoutID:int?}")]
        public async Task<ActionResult<CheckoutDto>> GetCheckout(int? checkoutId)
        {
            var checkout = await _checkoutService.GetOrCreateCheckout(checkoutId);
            var totalPricesTask = checkout.Units.Select(async u => await _skuService.CalculatePrice(u.SkuId, u.NumberOfUnits));
            var totalPrices = await Task.WhenAll(totalPricesTask);
            return new CheckoutDto() { Id = checkout.Id, Date = checkout.Date, TotalPrice = totalPrices.Sum() };
        }

        [HttpGet("checkoutUnits/{checkoutId:int?}")]
        public async Task<ActionResult<IEnumerable<CheckoutUnitDto>>> GetCheckoutUnits(int? checkoutId)
        {
            var checkout = await _checkoutService.GetOrCreateCheckout(checkoutId);
            var checkoutUnitsDtoTasks = checkout.Units.Select(async u => new CheckoutUnitDto() { CheckoutId = checkout.Id, SkuId = u.SkuId, NumberOfUnits = u.NumberOfUnits, TotalPrice = await _skuService.CalculatePrice(u.SkuId, u.NumberOfUnits) });
            var checkoutUnitsDto = await Task.WhenAll(checkoutUnitsDtoTasks);
            return checkoutUnitsDto;
        }

        [HttpPost("checkoutUnits")]
        public async Task<ActionResult<CheckoutUnitDto>> AddUnit(CheckoutUnitDto checkoutUnitDto)
        {
            var checkout = await _checkoutService.GetOrCreateCheckout(checkoutUnitDto.CheckoutId);
            var totalUnits = await _checkoutService.AddUnits(checkout, checkoutUnitDto.SkuId, checkoutUnitDto.NumberOfUnits);

            var totalPrice = await _skuService.CalculatePrice(checkoutUnitDto.SkuId, totalUnits);

            return CreatedAtAction(nameof(AddUnit), new { id = checkout.Id },
                new CheckoutUnitDto() { CheckoutId = checkout.Id, SkuId = checkoutUnitDto.SkuId, NumberOfUnits = totalUnits, TotalPrice = totalPrice });
        }
    }
}
