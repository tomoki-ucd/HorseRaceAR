using System.Collections;
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
    private const string XAI_API_KEY = "";
    private const string BASE_URL = "https://api.x.ai/v1";  // grok url
    [SerializeField] private Button _startToPlayButton;


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
