using UnityEngine;
using System;
using Unity.Cinemachine;

public class CinemachineCameraScipt : MonoBehaviour {
    double rotation;
    double startZ = -5f;
    double startX = 0f;
    CinemachineFollow cinemachineFollow;
    Vector3 startMousePosition;
    void Start() {
        cinemachineFollow = GetComponent<CinemachineFollow>();
    }
    void Update() {
        cinemachineFollow.FollowOffset.x = (float)((float)(Math.Sin(rotation * Math.PI / 180)) * startZ) + (float)((float)(Math.Cos(rotation * Math.PI / 180)) * startX);
        cinemachineFollow.FollowOffset.z = (float)((float)(Math.Cos(rotation * Math.PI / 180)) * startZ) + (float)((float)(-Math.Sin(rotation * Math.PI / 180)) * startX);
        if (Input.GetMouseButtonDown(1)) {
            startMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(1)) {
            rotation = (Input.mousePosition.x - startMousePosition.x) / 5;
            startZ = cinemachineFollow.FollowOffset.z;
            startX = cinemachineFollow.FollowOffset.x;
            startMousePosition = Input.mousePosition;
        }
    }
    private void OnApplicationQuit() {
        cinemachineFollow.FollowOffset.x = 0;
        cinemachineFollow.FollowOffset.z = -5;
    }
}
