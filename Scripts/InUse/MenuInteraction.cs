using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuInteraction : MonoBehaviour {

    public GameObject vaseSpawner;
    public GameObject musicPlayer;
    public SteamVR_Controller.Device controller;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding with " + gameObject.name );
        if(gameObject.name == "Start Panel" && collision.gameObject.tag == "ball")
        {
            vaseSpawner.SetActive(true);
            musicPlayer.SetActive(true);

            Destroy(GameObject.FindWithTag("Start"));
            Destroy(GameObject.FindWithTag("Quit"));
            Destroy(GameObject.FindWithTag("Credits"));
        }
        else if(gameObject.name == "Credits Panel" && collision.gameObject.tag == "ball")
        {
            Debug.Log("I should restart.");
            SceneManager.LoadScene(1);
        }
        else if (gameObject.name == "Restart Panel" && collision.gameObject.tag == "ball")
        {
            SceneManager.LoadScene(2);
        }




    }

    
}
