using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIReader : MonoBehaviour
{
    [SerializeField] private string APIUrl;
    void Start()
    {
        StartCoroutine(GetAPIRequest());
    }
    private IEnumerator GetAPIRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get(APIUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text;
            Debug.Log(response);
        }
        else
        {
            Debug.LogError($"Error in getting api request: {request.error}");
        }
    }
}
