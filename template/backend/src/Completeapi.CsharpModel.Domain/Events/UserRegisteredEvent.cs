using Completeapi.CsharpModel.Domain.Entities;

namespace Completeapi.CsharpModel.Domain.Events
{
    public class UserRegisteredEvent
    {
        public User User { get; }

        public UserRegisteredEvent(User user)
        {
            User = user;
        }
    }
}
