using PhoneKeypadDecoderApp.Core.Classes;

namespace PhoneKeypadDecoderTestApp
{
    public class AlternativDecoderTests
    {
        [Fact]
        public void AlternativDecoder_HelloWorldExample_ReturnsCorrectResult()
        {
            string input = "4433555 555666096667775553#";
            string expected = "HELLO WORLD";
            string result = AlternativDecoder.DecodeInput(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AlternativDecoder_WithStar_RemovesLastCharacter()
        {
            string input = "44 45*#";
            string expected = "HG";
            string result = AlternativDecoder.DecodeInput(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AlternativDecoder_ZeroKey_ReturnsSpace()
        {
            string input = "0#";
            string expected = " ";
            string result = AlternativDecoder.DecodeInput(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AlternativDecoder_MultiplePressesOnSameKey_ReturnsCorrectCharacters()
        {
            string input = "222#";
            string expected = "C";
            string result = AlternativDecoder.DecodeInput(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AlternativDecoder_Example34447288555_ReturnsDIPAUL()
        {
            string input = "34447288555#";
            string expected = "DIPAUL";
            string result = AlternativDecoder.DecodeInput(input);
            Assert.Equal(expected, result);
        }
    }
}
