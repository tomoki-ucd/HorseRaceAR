using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HorseNameGenerator : MonoBehaviour
{
    // Instant fields
    private readonly HttpClient _httpClient = new HttpClient();
    // TO DO : Encrypt the API key.
    private const string XAI_API_KEY = "";
    private const string BASE_URL = "https://api.x.ai/v1";  // grok url
    [SerializeField] private Button _startToPlayButton;
//    private bool isRunning = false;



    // Start is called before the first frame update
    void Start()
    {
        CustomLogger.Print(this, "Add RequestHorseName()");
        _startToPlayButton = GetComponent<Button>();
        if(_startToPlayButton != null)
        {
            // TO DO: Consider using UnitTask library
            CustomLogger.Print(this, "Add RequestHorseName()");
//            _startToPlayButton.onClick.AddListener(StartSendRequest);
            _startToPlayButton.onClick.AddListener(() => {StartCoroutine(SendRequest());});

//            _startToPlayButton.onClick.AddListener(async () => 
//            {
//                if(!isRunning)
//                {
//                    isRunning = true;
//                    StartCoroutine(SendRequest());
//                    isRunning = false;
//                }
//            });
        }
    }

//    public async Task<string> RequestHorseNames()
//    {
//        HttpResponseMessage response = await _httpClient.GetAsync(url);
//        response.EnsureSuccessStatusCode();
//        string responseString = await response.Content.ReadAsStringAsync();
//        CustomLogger.Print(this, responseString);
//        return responseString;
//    }

    private void StartSendRequest()
    {
        StartCoroutine(SendRequest());
    }

    IEnumerator SendRequest()
    {
        CustomLogger.Print(this, $"StartToPlayButton is clicked.");
        string url = BASE_URL + "/models";
        string apiKey = XAI_API_KEY;
        UnityWebRequest request = UnityWebRequest.Get(url);
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
