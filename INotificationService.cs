using System.Threading.Tasks;

namespace PsApp
{
    public interface INotificationService
    {
        Task NotifyAsync(string title, string message);
        Task NotifyAsync(string title, string message, int theId);
        void NotifyOld(string title, string message);
        Task NotifyOldAsync(string title, string message);
        Task NotifyBigAsync(string v1, string v2);
        Task NotifyBigAsync(string v1, string v2, CompactWorldEvent theEvent, string theTime);
    }
}