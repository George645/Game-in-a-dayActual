using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour{
    public Camera followingCamera;
    float playerSpeed = 5;
    CharacterController cc;
    public GameObject createSnowBall;
    GameObject currentSnowball;
    public GameObject lookAt;
    private void Awake() {
        followingCamera = FindFirstObjectByType<Camera>();
    }
    void Start(){
        followingCamera = FindFirstObjectByType<Camera>();
        cc = GetComponent<CharacterController>();
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            currentSnowball = Instantiate(createSnowBall);
            currentSnowball.GetComponent<SnowballScript>().pusher = this;
        }
    }
    private void FixedUpdate() {
        if (new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).magnitude > 0.75f) {
            Vector3 move = new Vector3(-(followingCamera.transform.position.x - transform.position.x) * Input.GetAxis("Vertical") + -(followingCamera.transform.position.z - transform.position.z) * Input.GetAxis("Horizontal"), 0, -(followingCamera.transform.position.z - transform.position.z) * Input.GetAxis("Vertical") + (followingCamera.transform.position.x - transform.position.x) * Input.GetAxis("Horizontal"));
            transform.LookAt(move + transform.position);
            move = move.normalized;
            cc.Move(move * Time.deltaTime * playerSpeed);
        }
    }
}
