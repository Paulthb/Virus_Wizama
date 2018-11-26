using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class InterfaceManager : MonoBehaviour {

    public GameObject Bomb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaceBomb()
    {
        Instantiate(Bomb, GameManager.GetManager().m_currentPlayer.transform.position, Quaternion.identity);
    }
}
////////////////garder le interfaceManager ou non ?? fin du tour a faire aussi