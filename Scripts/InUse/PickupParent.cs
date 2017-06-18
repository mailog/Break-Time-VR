using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickupParent : MonoBehaviour {

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    //public GameObject ballPrefab;
    //private GameObject ball;
    //private Transform ballTransform;


    public GameObject sphere;
    public float throwFactor;
    public float bulletTimeModifier;
    public float bulletTimeLength;
    private bool inBulletTime;
    private float bulletTimeCounter;
    private float ballSpawnCounter, ballSpawnRate;
    private int spawnCounter = 0;
    public Text paused;
    public bool pause;

    private void Start()
    {
        //ball = Instantiate(ballPrefab);
        //ballTransform = ball.transform;
        ballSpawnRate = 0.5f;
        ballSpawnCounter = ballSpawnRate;
    }

    void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

    void Update()
    {
        /*
        if (Time.timeScale == bulletTimeModifier)
        {
            inBulletTime = true;
        }
        else
        {
            inBulletTime = false;
        }
        */
        // Pause and unpause game
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {

            if (!pause)
            {
                Debug.Log("You have paused the game");
                Time.timeScale = 0;
                pause = true;
                paused.text = "Paused";
                
            }
            else
            {
                Debug.Log("You have UNpaused the game");
                Time.timeScale = 1;
                pause = false;
                paused.text = " ";
                //GameObject.FindWithTag("pause").SetActive(false);
            }
        }
        /*
        if (inBulletTime)
        {
            bulletTimeCounter += Time.deltaTime;
        }
        if (bulletTimeCounter >= bulletTimeLength)
        {
            Time.timeScale = 1f;
            bulletTimeCounter = 0;
        }*/

    }


    void FixedUpdate () {
        device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You are holding 'Touch' on the Trigger");
        }

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You activated TouchDown on the Trigger");
        }

        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You activated TouchUp on the Trigger");
        }

        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You are holding 'Press' on the Trigger");
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You activated PressDown on the Trigger");
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You activated PressUp on the Trigger");
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && !device.GetTouch(SteamVR_Controller.ButtonMask.Trigger) && ballSpawnCounter <= 0f)
        {
            Debug.Log("You activated PressUp on the Touchpad");
            Debug.Log("Balls Spawned: " + (++spawnCounter));
            Vector3 test = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
            // Vector3 camera = new Vector3(0, 1, 5);
            Instantiate(sphere, test, Quaternion.identity).GetComponent<Rigidbody>().velocity = new Vector3(0, 2.5f, 0);
            Time.timeScale = bulletTimeModifier;
            ballSpawnCounter = ballSpawnRate;
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            device.TriggerHapticPulse(3500);
        }
        ballSpawnCounter -= Time.deltaTime;
    }



    void OnTriggerStay(Collider col)
    {
        // col.attachedRigidbody.isKinematic = false;
        // col.attachedRigidbody.AddForce(device.angularVelocity);
        // col.attachedRigidbody.angularVelocity = device.angularVelocity;
        //Debug.Log("You have collided with " + col.name + " and activated OnTriggerStay");
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger) && col.tag.Equals("ball") && col.gameObject.GetComponent<BallController>().isHeld == false)
        {
            Debug.Log("You have collided with " + col.name + " while holding down Touch");
            col.attachedRigidbody.isKinematic = true;
            col.gameObject.GetComponent<BallController>().isHeld = true;
            col.gameObject.transform.SetParent(gameObject.transform);
        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger) && col.tag.Equals("ball") && col.gameObject.GetComponent<BallController>().isHeld == true)
        {
            Debug.Log("You have released Touch while colliding with " + col.name);
            col.gameObject.transform.SetParent(null);
            col.gameObject.GetComponent<BallController>().isHeld =  false;
            col.attachedRigidbody.isKinematic = false;
            Time.timeScale = 0.5f;
            device.TriggerHapticPulse(3000);
            tossObject(col.attachedRigidbody);
        }
    }

    void tossObject(Rigidbody rigidBody)
    {
        Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
        if (origin != null)
        {
            rigidBody.velocity = origin.TransformVector(device.velocity);
            rigidBody.angularVelocity = origin.TransformVector(device.angularVelocity);
        } else
        {
            rigidBody.velocity = device.velocity;
            rigidBody.angularVelocity = device.angularVelocity;
        }

        rigidBody.velocity = throwFactor* rigidBody.velocity;
        rigidBody.angularVelocity = throwFactor * rigidBody.angularVelocity;
        Time.timeScale = 1;
    }


}
