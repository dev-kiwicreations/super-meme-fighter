using System;
using System.Collections;
using System.Text;
using NaughtyAttributes;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class APIReader : MonoBehaviour
{
    [SerializeField] private string APIUrl;
    [SerializeField] private string AuthorizationToken;
    public static APIReader Instance;
    private string _requestData = $"";
    private PostAPIResponse _postAPIResponse;
    private void Awake()
    {
        Instance = this;
    }

    [Button("POST API Request")]
    public void PostAPIRequest(string playerName, string enemyName, int difficulty)
    {
        APIPostData postData = new APIPostData
        {
            player_wallet_id = "0x87a16F829Dd5979548D5581f1B32503Eb0b3f9D4",
            player_meme = playerName,
            enemy_meme = enemyName,
            started_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            difficulty = difficulty
        };
        //time format: "2024-07-24 01:02:03"
        _requestData = JsonConvert.SerializeObject(postData);
        StartCoroutine(GetAPIRequest(_requestData, RequestType.POST));
    }
    [Button("PUT API Request")]
    public void PutAPIRequest(bool winCondition)
    {
        APIPutData putData = new APIPutData
        {
            win = winCondition
        };
        _requestData = JsonConvert.SerializeObject(putData);
        StartCoroutine(GetAPIRequest(_requestData, RequestType.PUT));
    }
    private IEnumerator GetAPIRequest(string postData, RequestType requestType)
    {
        UnityWebRequest request = new UnityWebRequest();
        
        switch (requestType)
        {
            case RequestType.GET:
                break;
            case RequestType.POST:
                request = new UnityWebRequest(APIUrl, "POST");
                break;
            case RequestType.PUT:
                request = new UnityWebRequest(APIUrl + $"/{_postAPIResponse.id}", "PUT");
                break;
        }
        ProcessRequestSettings(postData, request);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text;
            Debug.Log(response);
            if (requestType == RequestType.POST)
            {
                _postAPIResponse = JsonConvert.DeserializeObject<PostAPIResponse>(response);
            }
        }
        else
        {
            Debug.LogError($"Error in getting api request: {request.error}");
        }
    }

    private void ProcessRequestSettings(string postData, UnityWebRequest request)
    {
        byte[] bodyRaw = Encoding.UTF8.GetBytes(postData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + AuthorizationToken);
        request.SetRequestHeader("Accept-Encoding", "gzip, deflate, br");
        request.SetRequestHeader("Connection", "keep-alive");
    }
}
public struct APIPostData
{
    public string player_wallet_id;
    public string player_meme;
    public string enemy_meme;
    public string started_at;
    public int difficulty;
}

public struct PostAPIResponse
{
    public int id;
}
public struct APIPutData
{
    public bool win;
}
public enum RequestType
{
    GET = 0,
    POST = 1,
    PUT = 2
}
