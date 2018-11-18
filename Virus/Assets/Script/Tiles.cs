using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour {

    private SpriteRenderer tilesImage;
    bool startok = false;

    [SerializeField]
    private bool m_isWall = false;

    // Use this for initialization
    void Start ()
    {
        startok = true;
        tilesImage = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ChangeTypeTile(bool isWall)
    {
        m_isWall = isWall;
        if (tilesImage == null)
        {
            Debug.Log("tilesImage == null");
            tilesImage = GetComponent<SpriteRenderer>();
        }
        if (tilesImage != null)
        {
            if (m_isWall)
                tilesImage.color = Color.blue;
            else
                tilesImage.color = Color.red;
        }
    }
}
