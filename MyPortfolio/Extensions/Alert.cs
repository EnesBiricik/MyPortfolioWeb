using Microsoft.AspNetCore.Mvc;

namespace MyPortfolio.Web.Extensions
{
    public enum AlertType
    {
        Success = 1,
        Error = 2,
        Info = 3,
        Warning = 4
    }
    public static class Alert
    {
        public static string ViewAlert(this Controller controller, AlertType alert = AlertType.Info, string alertMessage = "Info")
        {
            if (alert == AlertType.Success)
            {
                return $"<span class='successAlert'>{alertMessage}</span>";
            }
            else if (alert == AlertType.Warning)
            {
                return $"<span class='warningAlert'>{alertMessage}</span>";
            }
            else if (alert == AlertType.Error)
            {
                return $"<span class='errorAlert'>{alertMessage}</span>";
            }
            else
            {
                return $"<span class='infoAlert'>{alertMessage}</span>";
            }
        }
    }
}
