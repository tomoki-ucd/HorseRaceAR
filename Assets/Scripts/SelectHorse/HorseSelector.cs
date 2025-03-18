using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Provides the functionality to select a horse to bet.
/// </summary>
public class HorseSelector : MonoBehaviour
{
    // Instant fields
    private string _selectedHorse;
    private string[] _horseNames = new string[Horse.NUM_OF_HORSES];
    private GameObject _selectedButtonObj = null;
    [SerializeField] private Button[] _selectHorseButtons = new Button[Horse.NUM_OF_HORSES];
    [SerializeField] private Button _fixSelectButton;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Button button in _selectHorseButtons)
        {
            if(button == null)
            {
                CustomLogger.Print(this, $"{button} is null.");
                continue;
            }
            GameObject buttonObj = button.gameObject;
            button.onClick.AddListener(() => SelectHorse(buttonObj));
        }
    }


    /// <summary>
    /// Select a horse to bet.
    /// </summary>
    /// <param name="buttonObj"></param>/// 
    private void SelectHorse(GameObject buttonObj)
    {
        if(_selectedButtonObj == null)
        {
            SetButtonOutlineActive(buttonObj, true);
            SetFixSelectButtonActive();
        }
        else if(_selectedButtonObj != buttonObj)
        {
            SetButtonOutlineActive(buttonObj, true);
            SetButtonOutlineActive(_selectedButtonObj, false);
        }
        _selectedButtonObj = buttonObj;
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
}
