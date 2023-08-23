namespace TTV.Application
{
    public record SystemSettings
    {
        public string VideosPath { get; set; } = string.Empty;
        public string DiscountVoucherPepper { get; set; } = string.Empty;
    }
}
