using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPlacementEventHandler: MonoBehaviour
{
    [SerializeField] private ARPlaneController _arPlaneController;
    [SerializeField] private GameObject _horsePrefab;
    [SerializeField] private Toggle _fixToggle;
    GameObject _upButton;
    GameObject _downButton;
    GameObject _slider;

    private GameObject _spawnedHorse;
    public GameObject SpawnedHorse
    {
        get
        {
            return _spawnedHorse;
        }
        private set
        {
            _spawnedHorse = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _upButton = GameObject.Find("UpButton");
        _downButton = GameObject.Find("DownButton");
        _slider = GameObject.Find("Slider");
        _fixToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnToggleValueChanged(bool isOn)
    {
        DisableRacetrackControlUIs(isOn);   // If DisableRacetrackControlUIs() is executed after AddHorsesOnRacetarck(), it does not disable the UIs.
        AddHorsesOnRacetrack(isOn);
    }

    private void AddHorsesOnRacetrack(bool isOn)
    {
        if(isOn == false)
        {
            if (_arPlaneController == null)
            {
                CustomLogger.Print(this, "_arPlaneController is null");
                return;
            }

            GameObject _racetrack = _arPlaneController.SpawnedRacetrack;
            Renderer _renderer = _horsePrefab.GetComponent<Renderer>();
            Vector3 _objectSize = _renderer.bounds.size;
            float _halfHeight = _objectSize.y / 2;
            Vector3 _position = _racetrack.transform.position;
            _position.y += _halfHeight;
            _spawnedHorse = Instantiate(_horsePrefab, _position, Quaternion.identity);
        }
    }

    private void DisableRacetrackControlUIs(bool isOn)
    {
        _upButton.SetActive(isOn);
        _downButton.SetActive(isOn);
        _slider.SetActive(isOn);
    }                             
}
