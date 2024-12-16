using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour{
    Camera followingCamera;
    float playerSpeed = 5;
    Rigidbody rb;
    void Start(){
        followingCamera = FindFirstObjectByType<Camera>();
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {
        Vector3 move = new Vector3(-(followingCamera.transform.position.x - transform.position.x) * Input.GetAxis("Vertical") + -(followingCamera.transform.position.z - transform.position.z) * Input.GetAxis("Horizontal"), 0, -(followingCamera.transform.position.z - transform.position.z) * Input.GetAxis("Vertical") + (followingCamera.transform.position.x - transform.position.x) * Input.GetAxis("Horizontal"));
        move = move.normalized;
        move *= playerSpeed;
        rb.AddForce(move);
    }
}
