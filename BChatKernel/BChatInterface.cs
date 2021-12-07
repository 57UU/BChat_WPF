using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BChatKernel;
public record Friend {
    public long id;
    public string nickname;
    public Status status;
}
public enum Status{online,offline}
public enum ErrorType { Net, Account_Error, Cancelled, Force_Offline, Server }
public interface IBChatInterface
{
    //net 
    /// <summary>
    /// before login,if any excecption occurred,this function will be raised
    /// </summary>
    /// <param name="e"></param>
    /// <param name="errorType">Net,Cancelled or Account_Error</param>
    public void onLoginError(Exception e,ErrorType errorType);
    /// <summary>
    /// after login,if any excecption occurred,this function will be raised
    /// </summary>
    /// <param name="e"></param>
    /// <param name="type">Net,Cancelled or Force_Offline</param>
    public void onConnectionLost(Exception e,ErrorType type);

    //message
    public void OnReceivingFriendMessage(MessageInfo message);
        
    //friend
    public void RequestAddFriend(int id);
    public void RequestDeleteFriend(int id);
    //server
    public void ImportMessage(MessageInfo message);
}
