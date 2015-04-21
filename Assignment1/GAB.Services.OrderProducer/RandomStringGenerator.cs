namespace GAB.Services.OrderProducer
{
    using System.IO;

    public static class RandomStringGenerator
    {
        public static string GetRandomString()
        {
            string path = Path.GetRandomFileName();
            
            path = path.Replace(".", "");

            return path;
        }
    }
}