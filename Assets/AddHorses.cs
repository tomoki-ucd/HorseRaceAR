using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHorses : MonoBehaviour
{
    [SerializeField] private TUDebugPlaneDetection _TUDebugPlaneDetection;
    [SerializeField] private GameObject _horsePrefab;
    // TO DO
    // Add a UI to spawn horses

    // Start is called before the first frame update
    void Start()
    {
        // TO DO
        // Add a event listener to the UI element
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddHorsesOnRacetrack()
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


    private void MyDebugLog(string message)
    {
        Debug.Log($"[{this.GetType().Name}] {message}");
    }
}
