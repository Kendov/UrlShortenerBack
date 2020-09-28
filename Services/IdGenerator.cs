using shortid;
using shortid.Configuration;

namespace urlShortener.Services
{
    public static class IdGenerator
    {
        public readonly static GenerationOptions options = new GenerationOptions{
            UseNumbers = true,
            UseSpecialCharacters = true,
            Length = 8
        };

        public static string GenerateId()
        {
            return ShortId.Generate(options);
        }
    }
    
}