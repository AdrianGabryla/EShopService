using EShop.Application.Services;
using EShop.Domain.Exceptions.CardNumber;
using System.ComponentModel.DataAnnotations;

namespace EShop.Application.Tests
{
    public class CreditCardServiceTest
    {
        [Fact]
        public void ValidateCard_CheckCard_ReturnTrue()
        {
            var creditCardService = new CreditCardService();
            string num = "5530016454538418";
            var result = creditCardService.ValidateCard(num);
            Assert.True(result);

        }
      
        [Fact]
        public void CheckCardNumber_ThrowsTooShortLengthException()
        {
            var creditCardService = new CreditCardService();
            Assert.Throws<CardNumberTooShortException>(() => creditCardService.ValidateCard("1234"));

        }

        [Fact]
        public void CheckCardNumber_ThrowsTooLongLengthException()
        {
            var creditCardService = new CreditCardService();
            Assert.Throws<CardNumberTooLongException>(() => creditCardService.ValidateCard("123412341234123412341234"));
        }
        [Fact]
        public void CheckCardNumber_ThrowsInvalidNumberException()
        {
            var creditCardService = new CreditCardService();
            Assert.Throws<CardNumberInvalidException>(() => creditCardService.ValidateCard("999999999999999"));
        }

        [Fact]
        public void CheckCardNumber_NoSpacesAndDashes_ReturnTrue()
        {
            var creditCardService = new CreditCardService();
            string num = "4532289052809181";
            var result = creditCardService.ValidateCard(num);
            Assert.True(result);
        }
        [Fact]
        public void CheckDash_CorrectCardNumber_ReturnTrue()
        {
            var creditCardService = new CreditCardService();
            string num = "4024-0071-6540-1778";
            var result = creditCardService.ValidateCard(num);
            Assert.True(result);
        }
        [Fact]
        public void Space_CorrectCardNumber_ReturnTrue()
        {
            var creditCardService = new CreditCardService();
            string num = "4532 2080 2150 4434";
            var result = creditCardService.ValidateCard(num);
            Assert.True(result);
        }
        [Theory]
        [InlineData("3497 7965 8312 797", "American Express")]
        [InlineData("345-470-784-783-010", "American Express")]
        [InlineData("378523393817437", "American Express")]
        [InlineData("4024-0071-6540-1778", "Visa")]
        [InlineData("4532 2080 2150 4434", "Visa")]
        [InlineData("4532289052809181", "Visa")]
        [InlineData("5530016454538418", "MasterCard")]
        [InlineData("5551561443896215", "MasterCard")]
        [InlineData("5131208517986691", "MasterCard")]
        public void GerCardType_CheckNumber_ReturnTrue(string number, string type)
        {
            var creditCardService = new CreditCardService();
            var result = creditCardService.GetCardType(number);
            Assert.Equal(type, result);
        }
    }

}