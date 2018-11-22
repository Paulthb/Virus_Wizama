using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private TilesScript _currentTile;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Talk()
    {
        Debug.Log("Je m'appelle : " + gameObject.name);
    }
    
}

