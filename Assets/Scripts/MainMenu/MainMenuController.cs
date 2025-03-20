using System.Collections;
using System.Collections.Generic;   // For List<T>
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;  // For JsonConvert
using UnityEngine.Networking;
using System.Threading;
using Newtonsoft.Json.Linq;  // For JObject

public class MainMenuController : MonoBehaviour
{
    private class HorseName
    {
        public string value{get; set;}
    }

    // Static fields
    // TO DO : Encrypt the API key
    private const string XAI_API_KEY = "xai-eW1UuspgMn5J3V1C8oxHvSHGNKxo6vgfLtwEANHFrVqiRUblIdznOnC5KEWDtaNphLBSGgQdzFjsORGV";
    private const string BASE_URL = "https://api.x.ai/v1";  // grok url

    // Instant fields
    [SerializeField] private Button _startToPlayButton;
    private HorseName _horseName = new HorseName();


    void Awake()
    {
        _startToPlayButton.onClick.AddListener(OnClicked);
    }


    void Start()
    {
        GetHorseName();
    }


    private void GetHorseName()
    {
        StartCoroutine(SendRequest(_horseName));
    }


    IEnumerator SendRequest(HorseName horseName)
    {
        string url = BASE_URL + "/chat/completions";
        string apiKey = XAI_API_KEY;
        string json = CreateBody();
        byte[] body = System.Text.Encoding.UTF8.GetBytes(json); // Convert string to byte to send Json to the server.
        var request = new UnityWebRequest(url, "POST");
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(body);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", $"Bearer {apiKey}");
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text;
            JObject jsonObject = JObject.Parse(response);
            CustomLogger.Print(this, $"jsonObject: {jsonObject.ToString()}");
            horseName.value = jsonObject["choices"]?[0]?["message"]?["content"]?.ToString();
        }
        else
        {
            CustomLogger.Print(this, "$Response : {request.error}");
        }
    }

    
    private string CreateBody()
    {
        var jsonData = new JsonData();
        jsonData.messages = new List<Message>()
        {
            new Message()
            {
                role = "system", 
                content = $"あなたは独創的な名付け親です。"
            }, 
            new Message()
            {
                role = "user", 
                content = $"競走馬の名前を3つカタカナで考えて。\"[\"名前1\", \"名前2\", \"名前3\"]\"の形式で回答して。{AppData.HorseName}の名前以外にして。"
            }
        } ;
        jsonData.model = "grok-2-latest";
        jsonData.stream = false;
        jsonData.temperature = 0.9f;
        string json = JsonConvert.SerializeObject(jsonData, Formatting.Indented);

        return json;
    }


    private void OnClicked()
    {
        AppData.HorseName = _horseName.value;
        SceneManager.LoadScene("SelectHorse");
    }
}