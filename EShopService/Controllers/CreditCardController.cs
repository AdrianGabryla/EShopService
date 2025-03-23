using EShop.Application.Services;
using EShop.Domain.Exceptions.CardNumber;
using Microsoft.AspNetCore.Mvc;
using System.Net;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EShopService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CreditCardController : ControllerBase
{
        
    private readonly CreditCardService _creditCardService;
    public CreditCardController(CreditCardService creditCardService)
    {
        _creditCardService = creditCardService;
    }
    [HttpPost]
    public IActionResult Post([FromBody] string cardNumber)
    {
        try
        {
            var isValid = _creditCardService.ValidateCard(cardNumber);
            if (isValid)
            {
                var cardType = _creditCardService.GetCardType(cardNumber);
                return Ok(new { cardNumber, cardType });
            }
            else
            {
                return BadRequest(new { error = "Invalid card number", code = HttpStatusCode.BadRequest });
            }
        }
        catch (CardNumberTooShortException)
        {
            return BadRequest(new { error = "Card number is too short", code = HttpStatusCode.BadRequest });
        }
        catch (CardNumberTooLongException)
        {
            return BadRequest(new { error = "Card number is too long", code = HttpStatusCode.BadRequest });
        }
        catch (CardNumberInvalidException)
        {
            return BadRequest(new { error = "Card number is invalid", code = HttpStatusCode.BadRequest });
        }
    }
}
