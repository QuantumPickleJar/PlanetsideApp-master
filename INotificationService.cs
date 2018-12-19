using System.Threading.Tasks;

namespace PsApp
{
    public interface INotificationService
    {
        Task NotifyAsync(string title, string message);
        void NotifyOld(string title, string message);
        Task NotifyOldAsync(string title, string message);
    }
}