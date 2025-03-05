using System.Text;
using backend.Data;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class UrlShorteningService : IUrlShorteningService
    {
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private const int _shortUrlLength = 8;
        private readonly Random _random = new Random();

        public string GenerateUrlCode()
        {
            var sb = new StringBuilder(_shortUrlLength);
            for (int i = 0; i < _shortUrlLength; i++)
            {
                sb.Append(_chars[_random.Next(_chars.Length)]);
            }
            return sb.ToString();
        }
    }
}
