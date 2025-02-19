using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInputHandler : MonoBehaviour
{
    [SerializeField] private Button _upButton;
//    [SerializeField] private Button _downButton;
    [SerializeField] private TUDebugPlaneDetection _tUDebugPlaneDetection;
    private GameObject _spawnedRacetrack;

    // Start is called before the first frame update
    void Start()
    {
        _upButton.onClick.AddListener(RaiseRacetrack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MyDebugLog(string message)
    {
//        Debug.Log($"[TUDebugPlaneDetection] {message}");
        Debug.Log($"[{this.GetType().Name}] {message}");
    }

    /// <summary>
    /// Raise the position of Racetrack object.
    /// </summary>
    /// <param name=""></param>
    /// <returns></return>
    private void RaiseRacetrack()
    {
        _spawnedRacetrack = _tUDebugPlaneDetection.SpawnedRacetrack;

        if(_spawnedRacetrack == null)
        {
            MyDebugLog("_spawnedRacetrack is null.");
            return;
        }
        Vector3 pos = _spawnedRacetrack.transform.position;
        pos.y += 1;
        _spawnedRacetrack.transform.position = pos;
    }

}
