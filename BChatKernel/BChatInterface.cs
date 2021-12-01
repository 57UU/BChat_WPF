using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BChatKernel
{
    public record Message
    {
        public int id;
        public string content;
    }
    public record Friend {
        public int id;
        public string nickname;
    }

    public interface BChatInterface
    {
        //message
        public void OnReceivingFriendMessage(Message message);
        public void SendFriendMessage(Message message);
        //friend
        public void RequestAddFriend(int id);
        public void RequestDeleteFriend(int id);
    }
}
