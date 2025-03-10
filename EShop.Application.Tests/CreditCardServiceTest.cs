using EShop.Application.Services;
using System.ComponentModel.DataAnnotations;

namespace EShop.Application.Tests
{
    public class CreditCardServiceTest
    {
        [Fact]
        public void CheckCardNumber_TooShortLength_ReturnsFalse()
        {
            var creditCardService = new CreditCardService();
            string num = "12345678";
            var result = creditCardService.ValidateCard(num);
            Assert.False(result);

        }
        [Fact]
        public void CheckCardNumber_TooLongLength_ReturnsFalse()
        {
            var creditCardService = new CreditCardService();
            string num = "12345678912345678878676767";
            var result = creditCardService.ValidateCard(num);
            Assert.False(result);
        }
        [Fact]
        public void  CheckCardSpelling_LettersUsage_ReturnFalse()
        {
            var creditCardService = new CreditCardService();
            string num = "ABC232-3A3422";
            var result = creditCardService.ValidateCard(num);
            Assert.False(result);
        }
        [Fact]
        public void CheckVisa_WrongType_ReturnVisa()
        {
            var creditCardService = new CreditCardService();
            string type = "Visa";
        }
        [Theory]
        [InlineData("3497 7965 8312 797", "American Express")]
        public void GerCardType_CheckNumber_ReturnTrue(string number, string type)
        {
            var creditCardService = new CreditCardService();
            var result = creditCardService.GetCardType(number);
            Assert.Equal(type, result);
        }
    }

}