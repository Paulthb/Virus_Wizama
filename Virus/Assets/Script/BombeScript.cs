using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Script;
using System;

public class BombeScript : MonoBehaviour {

    [SerializeField]
    private Sprite[] m_numberSprite;
    [SerializeField]
    private SpriteRenderer m_counterSprite;
    [SerializeField]
    private int m_counter = 3;

    private int m_range = 3;
    private int m_bombTimer = 2;
    private float m_boxwidth = 1;
    private Vector2 m_boxSizeH;
    private Vector2 m_boxSizeV;
    private float m_offset;

    
    private List<TilesScript> m_affectedTilesV = new List<TilesScript>(); //Liste pour les tiles affectées sur l'axe vertical
    //private List<TilesScript> m_affectedTilesH= new List<TilesScript>(); //Liste pour les tiles affectées sur l'axe horizontal

    
    private List<TilesScript> m_affectedTilesL = new List<TilesScript>();
 

    private BoxCollider2D m_boxCollider;
	// Use this for initialization
	void Start ()
    {
        GameManager.GetManager().bombList.Add(this);//ajoute la bombe à la liste des bombes au GameManager
        m_counterSprite.sprite = m_numberSprite[m_counter];//affiche le compteur actuel

        m_boxCollider = GetComponent<BoxCollider2D>();
        m_boxSizeH = new Vector2((m_range * m_boxwidth) - 0.2f, 0.8f); //Prépare la box qui détectera les collisions sur le nombre de tile affectées sur l'axe horizontal
        m_boxSizeV = new Vector2(0.8f, (((m_range * 2) + 1) * m_boxwidth) - 0.2f); //Prépare la box qui détectera les collisions sur le nombre de tile affectées sur l'axe vertical
        
        m_offset = 1;

        GetAffectedTiles();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    private void Explode()
    {
        /*foreach (TilesScript tile in m_affectedTilesH)
        {
            tile.destroyTile = false;
        }
        foreach (TilesScript tile in m_affectedTilesV)
        {
            tile.destroyTile = false;
        }
        Debug.Log("censé exploser");
        */
        GameManager.GetManager().bombList.Remove(this);
        Destroy(this.gameObject);
        
    }

    private void GetAffectedTiles()
    {
        GetOffSet();
        GetAffectedTileLeft();
        
        
        /*
        Collider2D[] _collidersV = Physics2D.OverlapBoxAll(transform.position, m_boxSizeV, 0);
        foreach (Collider2D item in _collidersV)
        {
            TilesScript tile = item.GetComponent<TilesScript>(); //check si c'est bien une tile
            if (tile != null)
            {
                tile.destroyTile = true;
                m_affectedTilesV.Add(tile);
            }
        }*/

    }
    private void GetAffectedTileLeft()
    {
        Collider2D[] _collidersL = Physics2D.OverlapBoxAll(new Vector2(transform.position.x - m_offset, transform.position.y), m_boxSizeH, 0); //Le collider sur le côté L

        foreach (Collider2D item in _collidersL)
        {
            TilesScript tile = item.GetComponent<TilesScript>(); //check si c'est bien une tile
            if (tile != null)
            {
                m_affectedTilesL.Add(tile);
            }

        }

        m_affectedTilesL.Sort((a, b) => (b.Id.CompareTo(a.Id)));


        int count = 0;

        for (count = 0; count < m_affectedTilesL.Count; count++)
        {
            if (m_affectedTilesL[count].m_currentTilesType == TilesScript.TilesType.BORDER)
            {               
                break;
            }
            else if (m_affectedTilesL[count].m_currentTilesType == TilesScript.TilesType.WALL)
            {
                count++;
                break;
            }
        }


        if(m_affectedTilesL.Count > 0)
        { 
            for(int i = m_affectedTilesL.Count - 1; i > count -1; i--)
            {
                m_affectedTilesL.RemoveAt(i);
            }
        }
        
        foreach (TilesScript tile in m_affectedTilesL)
            Debug.Log("La tile " + tile.Id + " est dans la liste finale");

        
    }
    private void GetOffSet()
    {
        for(int i = 1; i < m_range; i++)
        {
            m_offset += 0.5f;
        }
    }

    public void UpdateCounter()
    {
        m_counter = m_counter - 1;
        m_counterSprite.sprite = m_numberSprite[m_counter];
        if (m_counter == 0)
            Explode();
    }
}
