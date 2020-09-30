using System.Collections.Generic;
using System.Linq;

namespace urlShortener.Services.DomainNotification
{
    public interface INotificationContext
    {
        IReadOnlyCollection<Notification> Notifications {get;}
        bool HasNotifications {get;}

        void AddNotification(string message);

        void AddNotification(Notification notification);

        void AddNotifications(IEnumerable<Notification> notifications);
        void AddNotifications(IEnumerable<string> notifications);
    }
}