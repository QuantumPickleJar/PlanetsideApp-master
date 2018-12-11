using System.Threading.Tasks;

namespace PsApp
{
    public interface INotificationService
    {
        Task NotifyAsync(string title, string message);
    }
}