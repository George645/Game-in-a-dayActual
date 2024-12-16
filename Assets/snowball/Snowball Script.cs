using UnityEngine;
using System;

public class SnowballScript : MonoBehaviour{
    public bool isBeingPushed = true;
    public Player pusher;
    Vector3 startposition;
    float sizeScale = 0.2f;
    [SerializeField] float sizeLimiter = 100;
    [SerializeField] float DistanceFromSnowballToPlayer = 1;
    void Start(){
        transform.position = new Vector3(pusher.transform.position.x, pusher.transform.position.y, pusher.transform.position.z) + new Vector3(-(pusher.followingCamera.transform.position.x - pusher.transform.position.x) * Input.GetAxis("Vertical") - (pusher.followingCamera.transform.position.z - pusher.transform.position.z) * Input.GetAxis("Horizontal"), 0, -(pusher.followingCamera.transform.position.z - pusher.transform.position.z) * Input.GetAxis("Vertical") + (pusher.followingCamera.transform.position.x - pusher.transform.position.x) * Input.GetAxis("Horizontal")).normalized * DistanceFromSnowballToPlayer;
        startposition = transform.position;
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }
    void Update(){
        Debug.Log(transform.localScale);
        if (startposition != transform.position) {
            sizeScale += (startposition - transform.position).sqrMagnitude / /*(float)Math.Pow(*/sizeLimiter/*, 2)*/;
            startposition = transform.position;
        }
        sizeScale = (float)Math.Clamp(sizeScale, 0.01, 3);
        transform.localScale = new Vector3(sizeScale, sizeScale, sizeScale);
        if (isBeingPushed) {
            if (new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).magnitude > 0.75) {
                startposition = transform.position;
                transform.position = new Vector3 (pusher.transform.position.x, pusher.transform.position.y, pusher.transform.position.z) + new Vector3(-(pusher.followingCamera.transform.position.x - pusher.transform.position.x) * Input.GetAxis("Vertical") - (pusher.followingCamera.transform.position.z - pusher.transform.position.z) * Input.GetAxis("Horizontal"), transform.localScale.x * 1.5f, - (pusher.followingCamera.transform.position.z - pusher.transform.position.z) * Input.GetAxis("Vertical") + (pusher.followingCamera.transform.position.x - pusher.transform.position.x) * Input.GetAxis("Horizontal")).normalized * DistanceFromSnowballToPlayer * sizeScale;
                if (Math.Abs(transform.position.magnitude - startposition.magnitude) > 1) {
                    startposition = transform.position;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space)){
               isBeingPushed = false;
            }
        }
    }
}