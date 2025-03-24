using EShop.Application.Services;
using EShop.Domain.Exceptions.CardNumber;
using Microsoft.AspNetCore.Mvc;
using System.Net;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EShopService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreditCardController : ControllerBase
{
    protected ICreditCardService
    
    
    private readonly CreditCardService _creditCardService = new();



    [HttpPost("validate/{cardNumber}")]
    public IActionResult ValidateCardNumber([FromRoute] string cardNumber)
    {
        try
        {
            bool isValid = _creditCardService.ValidateCard(cardNumber);
            return Ok(new { Valid = isValid });
        }
        catch (CardNumberInvalidException)
        {
            return BadRequest(new { code = HttpStatusCode.NotAcceptable });
        }
        catch (CardNumberTooShortException e)
        {
            return BadRequest(new { code = HttpStatusCode.BadRequest });
        }
        catch (CardNumberTooLongException e)
        {
            return BadRequest(new { code = HttpStatusCode.RequestedRangeNotSatisfiable });
        }

    }

    [HttpPost("getCardType/{cardNumber}")]
    public IActionResult GetCardType([FromRoute] string cardNumber)
    {
        try
        {
            string validCardProvider = _creditCardService.GetCardType(cardNumber);
            return Ok(new { Valid = validCardProvider });
        }
        catch (CardNumberInvalidException)
        {
            return BadRequest(new { code = HttpStatusCode.NotAcceptable });
        }
    }


}