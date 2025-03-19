using System.Collections;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEditor.ShaderGraph.Serialization;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// Provides the functionality to generate horse names using generative AI.
/// </summary>
public class HorseNameGenerator : MonoBehaviour
{
    // Instant fields
    // TO DO : Encrypt the API key.
    private const string XAI_API_KEY = "xai-eW1UuspgMn5J3V1C8oxHvSHGNKxo6vgfLtwEANHFrVqiRUblIdznOnC5KEWDtaNphLBSGgQdzFjsORGV";
    private const string BASE_URL = "https://api.x.ai/v1";  // grok url
    private Button _startToPlayButton;

    private sealed class Message
    {
        public string role {get; set;}
        public string content {get; set;}
    }

    [System.Serializable]
    private sealed class Data
    {
        public Message[] messages {get; set;} = new Message[2];
        public string model;
        public bool stream;
        public int temperature;
    }


    // Start is called before the first frame update
    void Start()
    {
        CustomLogger.Print(this, "Add RequestHorseName()");
        _startToPlayButton = GetComponent<Button>();
        if(_startToPlayButton != null)
        {
            // TO DO: Consider using UnitTask library
            _startToPlayButton.onClick.AddListener(() => {StartCoroutine(SendRequest());});
        }
    }


    /// <summary>
    /// Coroutine to send HTTP request to grok.
    /// </summary>
    /// <returns> IEnumerator </returns>
    IEnumerator SendRequest()
    {
        string url = BASE_URL + "/chat/completions";
        string apiKey = XAI_API_KEY;
        var data = new Data();
        data.messages[0] = new Message(){
                                        role = "system", 
                                        content = "あなたは独創的な名付け親です。"
                                        } ;
        data.messages[1] = new Message(){
                                        role = "user", 
                                        content = "競走馬の名前を3つ、カタカナで考えて下さい。以下のフォーマットに従い回答して。 {\"名前1\", \"名前2\", \"名前3\"}。"
                                        } ;
        data.model = "grok-2-latest";
        data.stream = false;
        data.temperature = 0;
        CustomLogger.Print(this, $"data\n{data}");
        string json = JsonUtility.ToJson(data);
        CustomLogger.Print(this, $"json\n{json}");
        byte[] body = System.Text.Encoding.UTF8.GetBytes(json);
        CustomLogger.Print(this, $"body\n{body}");
        var request = new UnityWebRequest(url, "POST"); 
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(body);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", $"Bearer {apiKey}");
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            CustomLogger.Print(this, $"Response : {request.downloadHandler.text}");
        }
        else
        {
            CustomLogger.Print(this, $"Response : {request.error}");
        }
    }
}
