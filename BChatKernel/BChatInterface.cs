using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BChatKernel;

public record Message
{
    public long id;
    public string content;
}
public record Friend {
    public long id;
    public string nickname;
    public Status status;
}
public enum Status{online,offline}
public enum LoginErrorType { Net,Account}

public interface IBChatInterface
{
    //net 
    public void OnLoginError(Exception e,LoginErrorType type);
    public void onConnectionLost(Exception e);

    //message
    public void OnReceivingFriendMessage(Message message);
        
    //friend
    public void RequestAddFriend(int id);
    public void RequestDeleteFriend(int id);
}
