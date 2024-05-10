using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using TTV.Application;
using TTV.Domain.Entities;

namespace TTV.Infrastructure;

public class DiscountVoucherCodeGenerator(IOptionsSnapshot<SystemSettings> config) : IDiscountVoucherCodeGenerator
{
    private readonly SystemSettings settings = config.Value;

    public string Generate(DiscountVoucher voucher)
    {
        byte[] voucherData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(voucher));
        byte[] salt = System.Security.Cryptography.RandomNumberGenerator.GetBytes(16);
        byte[] pepper = Encoding.UTF8.GetBytes(settings.DiscountVoucherPepper);
        byte[] dataToHash = voucherData.Union(salt).Union(pepper).ToArray();

        byte[] hash = System.Security.Cryptography.SHA256.HashData(dataToHash);
        byte[] truncated = hash.Take(6).ToArray(); // We can only store 6 bytes in a 12 character hex string. 2^48 is still more than enough unique voucher codes.

        return BitConverter.ToString(truncated).Replace("-", "");
    }
}