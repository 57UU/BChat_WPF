using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BChatKernel;
using System.Windows;

namespace WPF_Interface;

public class Assets
{
    public static Assets assets=new Assets();
    private Assets() { }
    public BChatService kernel;
    public MainWindow initialWindow;
    public IBChatInterface chatInterface=new Interface();
}
public class Interface : IBChatInterface
{
    public void onConnectionLost(Exception e,ErrorType type)
    {
        switch (type)
        {
            case ErrorType.Net:
                Utilities.CrossThread(() =>
                {
                    Assets.assets.initialWindow.status.Content = "Can't connect to server";
                    Assets.assets.initialWindow.loginBtn.IsEnabled = true;
                });
                break;
            case ErrorType.Account_Error:
                Utilities.CrossThread(() =>
                {
                    Assets.assets.initialWindow.status.Content = "Account or password is incorrect";
                    Assets.assets.initialWindow.loginBtn.IsEnabled = true;
                });
                break;
        }
    }


    public void OnReceivingFriendMessage(Message message)
    {
        throw new NotImplementedException();
    }

    public void RequestAddFriend(int id)
    {
        throw new NotImplementedException();
    }

    public void RequestDeleteFriend(int id)
    {
        throw new NotImplementedException();
    }
}

