namespace MobileStoreFeatureFlags.Models
{
    public class Mobile
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Specification { get; set; }
        public MobileReview MobileReview { get; set; }
    }
}
