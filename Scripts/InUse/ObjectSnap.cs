using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSnap : MonoBehaviour {

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    public Rigidbody rb;

	// Use this for initialization
	void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        rb.MovePosition(new Vector3(transform.position.x, transform.position.y, transform.position.z) + device.velocity * Time.deltaTime);
        rb.rotation = trackedObj.transform.rotation * Quaternion.Euler(90,0,0);
    }
}
