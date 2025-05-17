using PhoneKeypadDecoderApp.Core.Classes;


namespace PhoneKeypadDecoderTestApp
{
    
    public class PhoneKeypadDecoderTest
    {
        [Fact]
        public void DecodeInput_HelloWorldExample_ReturnsCorrectResult()
        {
            string input = "4433555 555666096667775553#";
            string expected = "HELLO WORLD";
            string result = PhoneKeypadDecoder.DecodeInput(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DecodeInput_WithStar_RemovesLastCharacter()
        {
            string input = "44 45*#";
            string expected = "HG";
            string result = PhoneKeypadDecoder.DecodeInput(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DecodeInput_ZeroKey_ReturnsSpace()
        {
            string input = "0#";
            string expected = " ";
            string result = PhoneKeypadDecoder.DecodeInput(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DecodeInput_MultiplePressesOnSameKey_ReturnsCorrectCharacters()
        {
            string input = "222#";
            string expected = "C";
            string result = PhoneKeypadDecoder.DecodeInput(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DecodeInput_Example34447288555_ReturnsDIPAUL()
        {
            string input = "34447288555#";
            string expected = "DIPAUL";
            string result = PhoneKeypadDecoder.DecodeInput(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DecodeInput_StarAtBeginning_ThrowsException()
        {
            string input = "*44#";
            Assert.Throws<InvalidOperationException>(() => PhoneKeypadDecoder.DecodeInput(input));
        }

        [Fact]
        public void ValidateInput_WithInvalidCharacter_ThrowsArgumentException()
        {
            string input = "12#";
            Assert.Throws<ArgumentException>(() => PhoneKeypadDecoder.ValidateInput(input));
        }

        [Fact]
        public void ValidateInput_WithHashNotAtEnd_ThrowsException()
        {
            string input = "44#45";
            Assert.Throws<ArgumentException>(() => PhoneKeypadDecoder.ValidateInput(input));
        }

        [Fact]
        public void DecodeInput_MultipleStars_ThrowsInvalidOperationException()
        {
            string input = "22**#";
            Assert.Throws<InvalidOperationException>(() => PhoneKeypadDecoder.DecodeInput(input));
        }
    }
}
