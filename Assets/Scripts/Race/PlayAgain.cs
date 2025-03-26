using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Provides the functionality to play the game again. 
/// </summary>
public class PlayAgain : MonoBehaviour
{
    void Awake()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(LoadMainMenu);
    }


    /// <summary>
    /// Load MainMenu to play the game again.
    /// </summary>
    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
