using UnityEngine;
using UnityEngine.UI;

public class SetHorseButton : MonoBehaviour
{
    // Instant Fields
    private Button _self;

    // Start is called before the first frame update
    void Start()
    {
        _self = GetComponent<Button>();
        if(_self == null)
        {
            CustomLogger.Print(this, $"_self is null!!");
        }
        _self.onClick.AddListener(Hide);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
