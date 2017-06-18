using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {
    
    public int bPoints;
    public int rPoints;
    public float bonusTime;
    public GameObject explosion;

    public float liveCounter, liveTime;
	// Use this for initialization
	void Start () {
        liveCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
        liveCounter += Time.deltaTime;
        if (liveCounter >= liveTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ball" && !collision.gameObject.GetComponent<BallController>().isHeld)
        {
            if (gameObject.transform.parent.GetComponent<GameManager>().timeCurrent > 0)
            {
                gameObject.transform.parent.GetComponent<GameManager>().score += bPoints;
                gameObject.transform.parent.GetComponent<GameManager>().timeCurrent += bonusTime;
            }
        }
        if (collision.gameObject.tag == "racquet")
        {
            if (gameObject.transform.parent.GetComponent<GameManager>().timeCurrent > 0)
            {
                gameObject.transform.parent.GetComponent<GameManager>().score += rPoints;
            }
            GameObject tmp = Instantiate(explosion, transform.position, Quaternion.identity);
            tmp.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ball" && !collision.gameObject.GetComponent<BallController>().isHeld)
        {
            GameObject tmp = Instantiate(explosion, transform.position, Quaternion.identity);
            tmp.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
            
        }
    }
}
