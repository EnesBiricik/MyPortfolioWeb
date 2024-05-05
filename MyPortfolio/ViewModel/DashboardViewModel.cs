using MyPortfolio.Dtos;
using MyPortfolio.Web.Models;

namespace MyPortfolio.Web.ViewModel
{
    public class DashboardViewModel
    {
        public uint TotalVisitorCount {  get; set; }
        public uint TotalPostCount { get; set; }
        public uint TotalProjectCount { get; set; }
        public Dictionary<string, uint>? DailyVisitors { get; set; }
        public Dictionary<string, uint>? SocialMediaVisitStatistic { get; set; }
        public int maxVisitorValue { get; set; }

    }
}
