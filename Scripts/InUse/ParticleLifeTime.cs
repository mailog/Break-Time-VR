using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLifeTime : MonoBehaviour {

    public float lifeTimeCounter, lifeTimeMax;
	// Use this for initialization
	void Start () {
        lifeTimeCounter = lifeTimeMax;
	}
	
	// Update is called once per frame
	void Update () {
        lifeTimeCounter -= Time.deltaTime;
        if(lifeTimeCounter <= 0)
        {
            Destroy(gameObject);
        }
	}
}
