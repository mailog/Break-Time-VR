using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacquetDurability : MonoBehaviour {

    public float durability;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(durability);
		if(durability <= 0)
        {
            Destroy(gameObject);
        }
	}
}
