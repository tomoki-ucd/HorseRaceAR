using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddHorses : MonoBehaviour
{
    [SerializeField] private TUDebugPlaneDetection _TUDebugPlaneDetection;
    [SerializeField] private GameObject _horsePrefab;
    [SerializeField] private Toggle _fixToggle;

    // Start is called before the first frame update
    void Start()
    {
        _fixToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnToggleValueChanged(bool isOn)
    {
        AddHorsesOnRacetrack(isOn);
        DisableRacetrackControlUIs(isOn);
    }

    private void AddHorsesOnRacetrack(bool toggle)
    {
        if(toggle == false)
        {
            if (_TUDebugPlaneDetection == null)
            {
                MyDebugLog("_TUDebugPlaneDetection is null");
                return;
            }

            GameObject _racetrack = _TUDebugPlaneDetection.SpawnedRacetrack;
            Renderer _renderer = _horsePrefab.GetComponent<Renderer>();
            Vector3 _objectSize = _renderer.bounds.size;
            float _halfHeight = _objectSize.y / 2;
            Vector3 _position = _racetrack.transform.position;
            _position.y += _halfHeight;
            Instantiate(_horsePrefab, _position, Quaternion.identity);
        }
    }

    private void DisableRacetrackControlUIs(bool isOn)
    {
        GameObject _upButton = GameObject.Find("UpButton");
        GameObject _downButton = GameObject.Find("DownButton");
        GameObject _slider = GameObject.Find("Slider");

        _upButton.SetActive(isOn);
        _downButton.SetActive(isOn);
        _slider.SetActive(isOn);
                                  
    }                             


    private void MyDebugLog(string message)
    {
        Debug.Log($"[{this.GetType().Name}] {message}");
    }
}
