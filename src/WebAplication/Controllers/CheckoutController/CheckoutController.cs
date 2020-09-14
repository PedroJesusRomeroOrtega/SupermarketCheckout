using Microsoft.AspNetCore.Mvc;
using SupermarketCheckout.Core.Entities;
using SupermarketCheckout.Core.Interfaces;
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

        [HttpPost]
        public async Task<ActionResult<CheckoutUnitDto>> AddUnit(CheckoutUnitDto checkoutUnitDto)
        {
            var checkout = await _checkoutService.GetOrCreateCheckout(checkoutUnitDto.CheckOutId);
            await _checkoutService.AddUnits(checkout, checkoutUnitDto.SkuId, checkoutUnitDto.NumberOfUnits);

            var totalPrice = await _skuService.CalculatePrice(checkoutUnitDto.SkuId, checkoutUnitDto.NumberOfUnits);

            return CreatedAtAction(nameof(AddUnit), new { id = checkout.Id },
                new CheckoutUnitDto() { CheckOutId = checkout.Id, SkuId = checkoutUnitDto.SkuId, NumberOfUnits = checkoutUnitDto.NumberOfUnits, TotalPrice = totalPrice });
        }
    }
}
