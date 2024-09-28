// Djaleen Malabonga
// Student #3128901
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RPGCameraManager : MonoBehaviour
{
    private static RPGCameraManager _instance = null; // singleton instance
    private CinemachineVirtualCamera _virtualCamera; // virtual camera that the camera manager is using

    public static RPGCameraManager Instance { // getter for instance
        get {
            return _instance;

        }
    }

    public CinemachineVirtualCamera VirtualCamera { // getter for the virtual camera
        get {
            return _virtualCamera;

        }
    }

    void Awake() { // singleton constructor
        if (_instance != null && _instance != this) {
            Destroy(gameObject);

        } else {
            _instance = this;

        }

        GameObject vCamGameObject = GameObject.FindWithTag("VirtualCamera");
        _virtualCamera = vCamGameObject.GetComponent<CinemachineVirtualCamera>(); 

    }
}
