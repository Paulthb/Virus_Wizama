using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class BombeScript : MonoBehaviour {

    private int m_range = 3;
    private int m_bombTimer = 2;
    private int m_counter = 1;
    private float m_boxwidth;
    private Vector2 m_boxSizeH;
    private Vector2 m_boxSizeV;
    private List<TilesScript> m_affectedTilesV = new List<TilesScript>(); //Liste pour les tiles affectées sur l'axe vertical
    private List<TilesScript> m_affectedTilesH= new List<TilesScript>(); //Liste pour les tiles affectées sur l'axe horizontal

    private BoxCollider2D m_boxCollider;
	// Use this for initialization
	void Start ()
    {
        m_boxCollider = GetComponent<BoxCollider2D>();
        m_boxSizeH = new Vector2(((m_range * 2) + 1) * m_boxwidth, m_boxwidth); //Prépare la box qui détectera les collisions sur le nombre de tile affectées sur l'axe horizontal
        m_boxSizeH = new Vector2(m_boxwidth, ((m_range * 2) + 1) * m_boxwidth); //Prépare la box qui détectera les collisions sur le nombre de tile affectées sur l'axe vertical
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void Explode()
    {
       
    }

    private void GetAffectedTiles()
    {
        Collider2D[] _collidersH = Physics2D.OverlapBoxAll(transform.position, m_boxSizeH,0);
        foreach (Collider2D item in _collidersH)
        {
            TilesScript tile = item.GetComponent<TilesScript>(); //check si c'est bien une tile
            if (tile != null)
            {
                m_affectedTilesH.Add(tile);
            }
        }
        Collider2D[] _collidersV = Physics2D.OverlapBoxAll(transform.position, m_boxSizeV, 0);
        foreach (Collider2D item in _collidersV)
        {
            TilesScript tile = item.GetComponent<TilesScript>(); //check si c'est bien une tile
            if (tile != null)
            {
                m_affectedTilesV.Add(tile);
            }
        }
    }
}
