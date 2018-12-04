using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Script;

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
    private List<TilesScript> m_affectedTilesV = new List<TilesScript>(); //Liste pour les tiles affectées sur l'axe vertical
    private List<TilesScript> m_affectedTilesH= new List<TilesScript>(); //Liste pour les tiles affectées sur l'axe horizontal

    private BoxCollider2D m_boxCollider;
	// Use this for initialization
	void Start ()
    {
        GameManager.GetManager().bombList.Add(this);//ajoute la bombe à la liste des bombes au GameManager
        m_counterSprite.sprite = m_numberSprite[m_counter];//affiche le compteur actuel

        m_boxCollider = GetComponent<BoxCollider2D>();
        m_boxSizeH = new Vector2((((m_range * 2) + 1) * m_boxwidth) - 0.2f, 0.8f); //Prépare la box qui détectera les collisions sur le nombre de tile affectées sur l'axe horizontal
        m_boxSizeV = new Vector2(0.8f, (((m_range * 2) + 1) * m_boxwidth) - 0.2f); //Prépare la box qui détectera les collisions sur le nombre de tile affectées sur l'axe vertical
        GetAffectedTiles();

        Debug.Log("vector 2D horizontal : " + m_boxSizeH);
        Debug.Log("vector 2D vertical : " + m_boxSizeV);
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    private void Explode()
    {
        foreach (TilesScript tile in m_affectedTilesH)
        {
            tile.destroyTile = false;
        }
        foreach (TilesScript tile in m_affectedTilesV)
        {
            tile.destroyTile = false;
        }
        Debug.Log("censé exploser");
        GameManager.GetManager().bombList.Remove(this);
        Destroy(this.gameObject);
    }

    private void GetAffectedTiles()
    {
        Collider2D[] _collidersH = Physics2D.OverlapBoxAll(transform.position, m_boxSizeH,0);
        foreach (Collider2D item in _collidersH)
        {
            TilesScript tile = item.GetComponent<TilesScript>(); //check si c'est bien une tile
            if (tile != null)
            {
                tile.destroyTile = true;
                m_affectedTilesH.Add(tile);
            }
        }
        Collider2D[] _collidersV = Physics2D.OverlapBoxAll(transform.position, m_boxSizeV, 0);
        foreach (Collider2D item in _collidersV)
        {
            TilesScript tile = item.GetComponent<TilesScript>(); //check si c'est bien une tile
            if (tile != null)
            {
                tile.destroyTile = true;
                m_affectedTilesV.Add(tile);
            }
        }

        //foreach(TilesScript tile in m_affectedTilesH)
        //{
        //    Debug.Log("tile concerné à l'horizontal : " + tile.Id);
        //}
        //foreach (TilesScript tile in m_affectedTilesV)
        //{
        //    Debug.Log("tile concerné à la vertical : " + tile.Id);
        //}
    }

    public void UpdateCounter()
    {
        m_counter = m_counter - 1;
        m_counterSprite.sprite = m_numberSprite[m_counter];
        if (m_counter == 0)
            Explode();
    }
}
