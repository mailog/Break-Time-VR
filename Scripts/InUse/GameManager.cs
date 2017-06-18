using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public SteamVR_Controller.Device device;
    private bool isPaused;
    public Text paused;

    public GameObject cannonShot;

    public bool burst;
    public int score;
    public int timeMax;
    public GameObject[] thrownObjects;
    public Text scoreText, timeText, timesUpText;
    public float spawnMax;
    public float spawnRate;
    public float speedMin, speedMax;
    public float bufferTime, bufferMax;
    public GameObject[] spawnLocations;
    public float timeCurrent;
    private float spawnCounter;

    // Use this for initialization

    void Start () {
        score = 0;
        spawnCounter = 0;
        timeCurrent = timeMax;
        spawnRate = spawnMax;
        bufferTime = bufferMax;
        spawnDestructible();
    }
	
	// Update is called once per frame
	void Update () {
        
        timeCurrent -= Time.deltaTime;
        if (!burst)
        {
            spawnRate = ((timeCurrent / timeMax) * spawnMax) + 0.5f;
        }
        if (spawnCounter >= spawnRate)
        {
            spawnCounter = 0;
            spawnDestructible();
        }
        else
        {
            spawnCounter += Time.deltaTime;
        }
        if (timeCurrent <= 0)
        {
            timesUpText.text = "TIME'S UP";
            timeCurrent = 0;
            Time.timeScale = 0.1f;
            bufferTime -= Time.deltaTime;
            if(bufferTime <= 0)
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(1);
            }
        }
        scoreText.text = score.ToString();
        timeText.text = (timeCurrent).ToString("F2");
    }

    private void spawnDestructible()
    {
        int location = Random.Range(0, spawnLocations.Length);
        float speed = Random.Range(speedMin, speedMax);
        float randRotX = Random.Range(0, 360);
        float randRotY = Random.Range(0, 360);
        float randRotZ = Random.Range(0, 360);
        int randObject = Random.Range(0, thrownObjects.Length);
        Vector3 spawnLoc = new Vector3(spawnLocations[location].transform.position.x, spawnLocations[location].transform.position.y, spawnLocations[location].transform.position.z);
        if (!burst)
        {
            GameObject tmpShot = (GameObject)Instantiate(cannonShot, spawnLoc, Quaternion.identity);
            tmpShot.GetComponent<AudioSource>().Play();
        }
        GameObject tmp = (GameObject)Instantiate(thrownObjects[randObject], spawnLoc, Quaternion.Euler(randRotX, randRotY, randRotZ));
        tmp.GetComponent<Rigidbody>().velocity = new Vector3(0f, 3f, speed);
        tmp.GetComponent<Rigidbody>().angularVelocity = new Vector3(speed, speed, speed);
        tmp.transform.parent = gameObject.transform;
    }

}
