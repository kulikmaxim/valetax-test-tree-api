namespace ValetaxTestTree.Api.Extensions
{
    public static class HttpRequestExtensions
    {
        public static async Task<string> GetBodyStringAsync(this HttpRequest request)
        {
            try
            {
                if (!(request.Body.CanRead && request.Body.CanSeek))
                    return default;

                request.Body.Position = 0;
                var reader = new StreamReader(request.Body);
                var body = await reader.ReadToEndAsync();

                return body;
            }
            catch
            {
                return default;
            }
        }
    }
}
