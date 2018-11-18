using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour {

    private SpriteRenderer tilesImage;
    bool startok = false;

    [SerializeField]
    private bool m_isWall = false;
    //public bool IsWall
    //{
    //    get { return m_isWall; }
    //    set
    //    {
    //        m_isWall = value;
    //        if (m_isWall)
    //            tilesImage.color = Color.blue;
    //        else
    //            tilesImage.color = Color.red;
    //    }
    //}

    // Use this for initialization
    void Start ()
    {
        startok = true;
        tilesImage = GetComponent<SpriteRenderer>();
        //if (m_isWall)
        //    tilesImage.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ChangeTypeTile(bool isWall)
    {
        if (startok) Debug.Log("start ok"); else Debug.Log("start not ok");

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
