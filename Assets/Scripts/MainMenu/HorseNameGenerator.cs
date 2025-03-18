using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HorseNameGenerator : MonoBehaviour
{
    // Instant fields
    private readonly HttpClient _httpClient = new HttpClient();
    private const string XAI_API_KEY = "xai-eW1UuspgMn5J3V1C8oxHvSHGNKxo6vgfLtwEANHFrVqiRUblIdznOnC5KEWDtaNphLBSGgQdzFjsORGV";
    private const string BASE_URL = "https://api.x.ai/v1";  // grok url
    [SerializeField] private Button _startToPlayButton;
    private bool isRunning = false;



    // Start is called before the first frame update
    void Start()
    {
        CustomLogger.Print(this, "Add RequestHorseName()");
        _startToPlayButton = GetComponent<Button>();
        if(_startToPlayButton != null)
        {
            // TO DO: Consider using UnitTask library
            CustomLogger.Print(this, "Add RequestHorseName()");
            _startToPlayButton.onClick.AddListener(async () => 
            {
                if(!isRunning)
                {
                    isRunning = true;
                    await RequestHorseNames();
                    isRunning = false;
                }
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async Task<string> RequestHorseNames()
    {
        CustomLogger.Print(this, "RequestHorseName() is called.");
        string url = BASE_URL;
        HttpResponseMessage response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseString = await response.Content.ReadAsStringAsync();
        CustomLogger.Print(this, responseString);
        return responseString;
    }
}
