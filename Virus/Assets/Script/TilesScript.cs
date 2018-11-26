using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class TilesScript : MonoBehaviour {

    private SpriteRenderer _spriteRenderer;
    private bool _currentTile = false;
    private bool _targetTile = false;
    private bool _selectableTile = false;
    private bool _walkableTile = true;

    public List<TilesScript> _adjacentTiles = new List<TilesScript>(); //Pas de déplacement en diagonal on va utiliser cette liste pour se déplacer

    //Les booléens liés au BFS = breadth first search (j'assimile cette notion en écrivant ça)
    private bool _visited = false; //En gros ça permet au "path finding" d'ignorer la case qu'on vient de quitter (je crois)
    private TilesScript _parent = null;
    private int _distance = 0; //la variable dans laquelle on va stocker la distance entre le player et cette tile

    //Variables hors tuto
  
    private float _offset = 0.001f; //Pour être sûr d'être sur le collider
    private float _width;
    private Vector2 _center;

    private Color _redColor;
    private Vector2 _size;

    private void Start()
    {
        _size = GetComponent<BoxCollider2D>().size;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _width = _spriteRenderer.sprite.bounds.size.x;
       
        _center = new Vector2(transform.position.x + (_width / 2), transform.position.y - (_width / 2));
        _redColor = Color.red;
        _redColor.a = 0.80f;
        
    }

    private void Update()
    {
        if (_currentTile)
        { 
            _spriteRenderer.color = Color.magenta;
        }
        else if (_targetTile)
        {
            _spriteRenderer.color = Color.green;
        }
        else if (_selectableTile && _walkableTile)
        {
            _spriteRenderer.color = _redColor;
        }
        else
        {
            _spriteRenderer.color = Color.white;
        }

    }


    public void Reset()
    {
        _adjacentTiles.Clear();

        _currentTile = false;
        _targetTile = false;
        _selectableTile = false;

        _visited = false;
        _parent = null;
        _distance = 0;
    }

    public void FindNeighbors()
    {
        Reset();
        CheckTile();
    }

    private void CheckTile()
    {
        Collider2D[] _colliders = Physics2D.OverlapBoxAll(transform.position, _size, 45.0f);
        
        foreach(Collider2D item in _colliders)
        {
            Debug.Log("je compte");
            TilesScript tile = item.GetComponent<TilesScript>(); //check si c'est bien une tile
            if(tile != null && tile._walkableTile)
            {
                _adjacentTiles.Add(tile);
            }
        }
    }

    void OnMouseDown()
    {
        if (_selectableTile)
        {
            GameManager.GetManager().MovePlayer(gameObject);
        }
    }

    //SETTERS      (ya un exemple de propertie dans le game manager) ********

    public void SetSelectableBool (bool b)
    {
        _selectableTile = b;
    }
    public void SetCurrentBool(bool b)
    {
        _currentTile = b;
    }
    public void SetWalkableBool(bool b)
    {
        _walkableTile = b;
    }
    public void SetTargetBool(bool b)
    {
        _targetTile = b;
    }
    public void SetVisitedBool(bool b)
    {
        _visited = b;
    }
    public void SetParent(TilesScript t)
    {
        _parent = t;
    }
    public void SetDistance(int d)
    {
        _distance = d;
    }

    //GETTERS

    public bool GetSelectableBool()
    {
        return _selectableTile;
    }
    public bool GetCurrentBool()
    {
        return _currentTile;
    }
    public bool GetWalkableBool()
    {
        return _walkableTile;
    }
    public bool GetTargetBool()
    {
        return _targetTile;
    }
    public bool GetVisitedBool()
    {
        return _visited;
    }
    public TilesScript GetPArent()
    {
        return _parent;
    }
    public int GetDistance()
    {
        return _distance;
    }
    



/* remplacer tout les booleens par l'enum en dessous et modifié le code en fonction :
 public enum tilesState
    {
        CURRENT;
        TARGET;
        SELECT;
        WALKABLE;
    }
    public tileState m_currentTileState = tileState.CURRENT;*/

}
