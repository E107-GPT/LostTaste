using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;


public class HTTPRequest : MonoBehaviour
{
    string url = "https://j10e107.p.ssafy.io/api/";
    //string port = "443";
    // Start is called before the first frame update
    string accesstoken;

    // GET
    // ��θ� ����
    IEnumerator GET(string path)
    {
        UnityWebRequest request = UnityWebRequest.Get(url + path);
        request.SetRequestHeader("Authorization", "Bearer " + UserInfo.GetInstance().getToken());
        
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success) // Unity 2020.1 ���ĺ��ʹ� isNetworkError�� isHttpError ��� result ���
        {
            if (path.Equals("user/profile"))
            {
                GameObject.Find("Canvas/Login SignUp Window").GetComponent<Login>().ShowConnecting();
            }
            
            Debug.Log(request.error);
        }
        else
        {
            if(path.Equals("user/profile"))
            {
                UserData data = JsonUtility.FromJson<UserData>(request.downloadHandler.text);
                

                UserInfo.GetInstance().SetId(data.id);
                UserInfo.GetInstance().SetNickName(data.nickname);

                GameObject.Find("Canvas/Login SignUp Window").GetComponent<Login>().MoveScene();
            }
            // ���� �ؽ�Ʈ ó��
            Debug.Log(request.downloadHandler.text);
        }

    }
    public void GetCall(string path)
    {
        StartCoroutine(GET(path));
    }

    // POST
    // url + path = uri
    // dictionary�� �� ����
    IEnumerator POST(string path, Dictionary<string, string> postParam)
    {
        // Dictionary�� ���� JSON ���ڿ��� ��ȯ
        StringBuilder jsonDataBuilder = new StringBuilder("{");
        foreach (var item in postParam)
        {
            jsonDataBuilder.Append($"\"{item.Key}\":\"{item.Value}\",");
        }
        if (jsonDataBuilder.Length > 1) // ������ ��ǥ�� �����ϱ� ����
        {
            jsonDataBuilder.Remove(jsonDataBuilder.Length - 1, 1);
        }
        jsonDataBuilder.Append("}");
        string jsonData = jsonDataBuilder.ToString();

        byte[] jsonToSend = new UTF8Encoding().GetBytes(jsonData);
        Debug.Log(jsonData);
        Debug.Log(url + "api/" + path);
        UnityWebRequest postRequest = new UnityWebRequest(url + path, "POST");
        postRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        postRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        postRequest.SetRequestHeader("Content-Type", "application/json");

        yield return postRequest.SendWebRequest();

        if (postRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("error" + postRequest.error);
            Debug.Log("result" + postRequest.result);
            Debug.Log(postRequest.downloadHandler.text);

            if (path.Equals("user")) // ȸ�������� ���
            {
                // �α��� ȭ������ �̵�
                Login login = GameObject.Find("Canvas/Login SignUp Window").GetComponent<Login>();
                login.SignupFailure();
            }
            else if (path.Equals("auth/login")) // �α����� ���
            {
                Login login = GameObject.Find("Canvas/Login SignUp Window").GetComponent<Login>();
                login.LoginFailure();
            }
        }
        else // ��� ����
        {

            if (path.Equals("user")) // ȸ�������� ���
            {
                // �α��� ȭ������ �̵�
                Login login = GameObject.Find("Canvas/Login SignUp Window").GetComponent<Login>();
                login.ShowLoginPanel();
            }
            else if (path.Equals("auth/login")) // �α����� ���
            {
                UserData data = JsonUtility.FromJson<UserData>(postRequest.downloadHandler.text);
                accesstoken = data.accessToken;

                UserInfo.GetInstance().SetToken(accesstoken);
                //info.GetInstance().SetId(data.id);
                //info.GetInstance().SetNickName(data.nickname);

                //�׼��� ��ū���� id, pw �޾ƿ��� get ��������

                GameObject.Find("Canvas/Login SignUp Window").GetComponent<Login>().ShowConnecting();
                GetCall("user/profile");
            }
            Debug.Log(postRequest.downloadHandler.text);
        }
    }

    [System.Serializable]
    class UserData
    {
        public string accessToken;
        public string id;
        public string nickname;
    }

    public void POSTCall(string path, Dictionary<string, string> postParam)
    {
        StartCoroutine(POST(path, postParam));
    }
}
