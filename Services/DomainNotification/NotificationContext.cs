using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace urlShortener.Services.DomainNotification
{
    public class NotificationContext : INotificationContext
    {
        private readonly List<Notification> _notifications;
        public IReadOnlyCollection<Notification> Notifications => _notifications;
        public bool HasNotifications => _notifications.Any();


        public NotificationContext()
        {
            _notifications = new List<Notification>();
        }

        public void AddNotification(string message)
        {
            _notifications.Add(new Notification(message));
        }

        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }

        public void AddNotifications(IEnumerable<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }
        public void AddNotifications(IEnumerable<string> notifications)
        {
            foreach (var notification in notifications)
            {
                _notifications.Add(new Notification(notification));
                
            }
        }


	
    }
}