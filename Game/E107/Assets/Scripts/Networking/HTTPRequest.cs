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

        if (request.result != UnityWebRequest.Result.Success) // Unity 2020.1 이후부터는 isNetworkError와 isHttpError 대신 result 사용
        {
            Debug.Log(request.error);
        }
        else
        {
            // 응답 텍스트 처리
            Debug.Log(request.downloadHandler.text);
        }

    }
    public void GetCall(string path)
    {
        StartCoroutine(GET(path));
    }

    // POST
    // url + path = uri
    // dictionary로 값 전달
    IEnumerator POST(string path, Dictionary<string, string> postParam)
    {
        // Dictionary를 직접 JSON 문자열로 변환
        StringBuilder jsonDataBuilder = new StringBuilder("{");
        foreach (var item in postParam)
        {
            jsonDataBuilder.Append($"\"{item.Key}\":\"{item.Value}\",");
        }
        if (jsonDataBuilder.Length > 1) // 마지막 쉼표를 제거하기 위함
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
