using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnClickPlay()
    {
        SceneManager.LoadScene("Race");
    }
}
