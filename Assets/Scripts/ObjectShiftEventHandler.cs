using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectShiftEventHandler: MonoBehaviour
{
    [SerializeField] private Button _upButton;
    [SerializeField] private Button _downButton;
    [SerializeField] private ARPlaneController _arPlaneController;

    // Start is called before the first frame update
    void Start()
    {
        _upButton.onClick.AddListener(RaiseRacetrack);
        _downButton.onClick.AddListener(LowerRacetrack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MyDebugLog(string message)
    {
//        Debug.Log($"[ARPlaneController] {message}");
        Debug.Log($"[{this.GetType().Name}] {message}");
    }

    /// <summary>
    /// Raise the position of Racetrack object.
    /// </summary>
    /// <param name=""></param>
    /// <returns></return>
    private void RaiseRacetrack()
    {
        GameObject _spawnedRacetrack;
        _spawnedRacetrack = _arPlaneController.SpawnedRacetrack;

        if(_spawnedRacetrack == null)
        {
            MyDebugLog("_spawnedRacetrack is null.");
            return;
        }
        Vector3 pos = _spawnedRacetrack.transform.position;
        pos.y += 0.1f;
        _spawnedRacetrack.transform.position = pos;
    }


    /// <summary>
    /// Lower the position of Racetrack object.
    /// </summary>
    /// <param name=""></param>
    /// <returns></return>
    private void LowerRacetrack()
    {
        GameObject _spawnedRacetrack;
        _spawnedRacetrack = _arPlaneController.SpawnedRacetrack;

        if(_spawnedRacetrack == null)
        {
            MyDebugLog("_spawnedRacetrack is null.");
            return;
        }
        Vector3 pos = _spawnedRacetrack.transform.position;
        pos.y -= 0.1f;
        _spawnedRacetrack.transform.position = pos;
    }
}
