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
    private Assets() {

        buildConfig = new BChatBuildConfig() {
            bChatInterface = chatInterface,
            ip = "127.0.0.1",
            port = 1234,
        };
        kernel = new BChatService(buildConfig);
         
    }
    public BChatBuildConfig buildConfig;
    public BChatService kernel;
    public MainWindow initialWindow;
    public IBChatInterface chatInterface=new Interface();
}
public class Interface : IBChatInterface
{
    public void onLoginError(Exception e,ErrorType errorType)
    {
        switch (errorType)
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
            case ErrorType.Cancelled:
                Utilities.CrossThread(() =>
                {
                    Assets.assets.initialWindow.status.Content = "You cancelled this oparation";
                    Assets.assets.initialWindow.loginBtn.IsEnabled = true;
                });
                break;
        }
    }
    public void onConnectionLost(Exception e,ErrorType type)
    {

    }


    public void OnReceivingFriendMessage(MessageInfo message)
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

