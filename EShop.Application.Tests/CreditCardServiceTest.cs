using EShop.Application.Services;
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
        public void ValidateCard_CheckLunhaAlgorithm_ReturnFalse()
        {
            var creditCardService = new CreditCardService();
            string num = "4539 1488 0343 6467";
            var result = creditCardService.ValidateCard(num);
            Assert.False(result);

        }
        [Fact]
        public void CheckCardNumber_TooShortLength_ReturnsFalse()
        {
            var creditCardService = new CreditCardService();
            string num = "1234678";
            var result = creditCardService.ValidateCard(num);
            Assert.False(result);

        }
        [Fact]
        public void CheckCardNumber_TooLongLength_ReturnsFalse()
        {
            var creditCardService = new CreditCardService();
            string num = "1234567896098345678878676767";
            var result = creditCardService.ValidateCard(num);
            Assert.False(result);
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