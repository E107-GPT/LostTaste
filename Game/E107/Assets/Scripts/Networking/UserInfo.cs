
public class UserInfo 
{
    private static UserInfo instance;
    private string id;
    private string nickname;
    private string accessToken;
    private int jelly;
    private int currentServer;

    UserInfo() { }

    public static UserInfo GetInstance()
    {
        if(instance == null)
        {            
            instance = new UserInfo();
        }

        return instance;
    }

    public void SetId(string id)
    {
        this.id = id;
    }
    public void SetNickName(string nickname)
    {
        this.nickname = nickname;
    }
    public void SetToken(string accessToken)
    {
        this.accessToken = accessToken;
    }
    public void SetJelly(int jelly)
    {
        this.jelly = jelly;
    }
    public void SetCurrentServer(int server)
    {
        this.currentServer = server;
    }


    public int GetCurrentServer()
    {
        return currentServer;
    }
    public string getId()
    {
        return id;
    }
    public string getNickName()
    {
        return nickname;
    }
    public string getToken()
    {
        return accessToken;
    }
    public int getJelly()
    {
        return jelly;
    }
}
