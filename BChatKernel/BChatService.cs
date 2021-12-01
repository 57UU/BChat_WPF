using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BChatKernel;

public sealed class BChatService
{
    //friens list
    private Dictionary<long, Friend> friends = new();
    //conmunication socket
    private SecureSocket.SecureSocket socket = new();
    public BChatService(IBChatInterface chatInterface,long id,string password)
    {
        this.Interface = chatInterface;
        
    }
    private IBChatInterface Interface;
    public void SendFriendMessage(Message message)
    {

    }

}

