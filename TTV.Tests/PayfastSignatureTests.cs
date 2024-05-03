using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTV.Infrastructure;

namespace TTV.Tests;
public class PayfastSignatureTests
{
    [Fact]
    public void TestSignature()
    {
        var payload = "m_payment_id=243824d1-98bc-4e4f-a502-120021385afe&pf_payment_id=1887869&payment_status=COMPLETE&item_name=Order%232034&item_description=&amount_gross=80.00&amount_fee=-2.30&amount_net=77.70&custom_str1=&custom_str2=&custom_str3=&custom_str4=&custom_str5=&custom_int1=&custom_int2=&custom_int3=&custom_int4=&custom_int5=&name_first=&name_last=&email_address=LARobinson89%40gmail.com&merchant_id=10031303&signature=4a100b35197fe4039119d97d9a51a6bc";
        var data = payload.Split("&").Select(x => x.Split("=")).ToDictionary(x => x[0], x => Uri.UnescapeDataString(x[1]));

        var signature = GenerateSignature(data
            .Where(kvp => kvp.Key != "signature" && kvp.Key != "pf_host")
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value ?? string.Empty), passphrase: "TwitchingCokeCatToyGiftBag");
        Assert.Equal(signature, data["signature"]);
    }

    private static string DataToString(Dictionary<string, string> data, bool urlEncode = true)
    {
        return string.Join("&", data.Keys.Select(k => $"{k}={(urlEncode ? Uri.EscapeDataString(data[k]) : data[k])}"));
    }

    private static string GenerateSignature(Dictionary<string, string> data, string? passphrase = null, bool urlEncode = true)
    {
        var signature = DataToString(data, urlEncode);
        if (!string.IsNullOrEmpty(passphrase))
            signature += $"&passphrase={passphrase}";
        return signature.ToMD5Hash();
    }
}
