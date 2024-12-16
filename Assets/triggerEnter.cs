using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class triggerEnter : MonoBehaviour{
    public List<SnowballScript> snowballs = new();
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other + " entered");
        if (other.name.Contains("nowball")) {
            snowballs.Add(other.GetComponent<SnowballScript>());
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.name.Contains("nowball")) {
            snowballs.Remove(other.GetComponent<SnowballScript>());
        }
    }
    void Update(){
        Debug.Log(snowballs.Count);
        if (snowballs.Count == 3) {
            Debug.Log("You won");
        }
    }
}
