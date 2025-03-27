using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;    // For TextMeshProUGUI type

/// <summary>
/// Provides the functionality to select a horse to bet.
/// </summary>
public class HorseSelector : MonoBehaviour
{
    // Instant fields
    private string[] _horseNames = new string[Horse.NUM_OF_HORSES];
    private GameObject _selectedHorse = null;
    public static string selectedHorseName = null;
    [SerializeField] private Button[] _selectHorseButtons = new Button[Horse.NUM_OF_HORSES];
    [SerializeField] private Button _fixSelectButton;


    // Start is called before the first frame update
    void Start()
    {
        InitializeHorseButtonName();
        AddListeners();
    }


    /// <summary>
    /// Initialize the horse buttons' names with the names acquired by grok.
    /// </summary>
    private void InitializeHorseButtonName()
    {
        _horseNames = JsonConvert.DeserializeObject<string[]>(AppData.HorseName);
        for(int i = 0; i < _horseNames.Length; i++)
        {
            if(_selectHorseButtons[i] == null)
            {
                CustomLogger.Print(this, "_selectHorseButtons is null!!");
            }
            _selectHorseButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"#{i+1} " + _horseNames[i];
        }
    }


    /// <summary>
    /// Add event listeners.
    /// </summary>
    private void AddListeners()
    {
        // Listeners for each horse name button.
        foreach(Button button in _selectHorseButtons)
        {
            if(button == null)
            {
                CustomLogger.Print(this, $"{button} is null.");
                continue;
            }
            button.onClick.AddListener(() => SelectHorse(button.gameObject));
        }

        // Listeners for "Select" button.
        _fixSelectButton.onClick.AddListener(OnClicked);
    }


    /// <summary>
    /// Select a horse to bet.
    /// </summary>
    /// <param name="newlySelectedHorse"></param>/// 
    private void SelectHorse(GameObject newlySelectedHorse)
    {
        if(_selectedHorse == null)  // If there was no selected button
        {
            SetButtonOutlineActive(newlySelectedHorse, true);    // Display the outline for the selected button
            SetFixSelectButtonActive();                 // Activate "Select" button
        }
        else if(_selectedHorse != newlySelectedHorse)    // If the clicked button is not the one already selected
        {
            SetButtonOutlineActive(newlySelectedHorse, true);
            SetButtonOutlineActive(_selectedHorse, false);
        }
        _selectedHorse = newlySelectedHorse;
        selectedHorseName = _selectedHorse.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        selectedHorseName = selectedHorseName.Remove(0, 3);    // Remove "#n " from the name
    }


    /// <summary>
    /// Activate/Deactivate the Button Outline.
    /// </summary>
    /// <param name="button"></_param>/// 
    /// <param name="isActive"></_param>/// 
    private void SetButtonOutlineActive(GameObject button, bool isActive)
    {
        Transform outline = button.transform.Find("ButtonOutline");
        outline.gameObject.SetActive(isActive);
    }

    private void SetFixSelectButtonActive()
    {
        _fixSelectButton.interactable = true;
    }


    private void OnClicked()
    {
        SceneManager.LoadScene("Race");
    }
}
