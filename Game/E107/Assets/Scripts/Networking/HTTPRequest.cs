using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;


public class HTTPRequest : MonoBehaviour
{
    string url = "https://j10e107.p.ssafy.io/";
    string port = "443";
    // Start is called before the first frame update
    string id = "sd";

    private void Start()
    {    
    }


    // GET
    IEnumerator GET(string path)
    {
        UnityWebRequest request = UnityWebRequest.Get(url + "api/" +path);
        
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success) // Unity 2020.1 ���ĺ��ʹ� isNetworkError�� isHttpError ��� result ���
        {
            Debug.Log(request.error);
        }
        else
        {
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
        UnityWebRequest postRequest = new UnityWebRequest(url + "api/" + path, "POST");
        postRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        postRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        postRequest.SetRequestHeader("Content-Type", "application/json");

        yield return postRequest.SendWebRequest();

        if (postRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("error" + postRequest.error);
            Debug.Log("result" + postRequest.result);
            Debug.Log(postRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log(postRequest.downloadHandler.text);
        }
    }


    public void POSTCall(string path, Dictionary<string, string> postParam)
    {
        StartCoroutine(POST(path, postParam));
    }
}
