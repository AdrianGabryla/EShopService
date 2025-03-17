using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Exceptions.CardNumber;

public class CardNumberTooLongException : Exception
{
    public CardNumberTooLongException() : base("414")
    { }
    public CardNumberTooLongException(Exception innerException) : base("414", innerException) { }

}
