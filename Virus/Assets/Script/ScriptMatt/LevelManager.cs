using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Script;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] _tilePrefabs;

    [SerializeField]
    private Player[] m_playerTab;

    [SerializeField]
    private String _levelName = "Level1";

    private List<TilesScript> TilesList = new List<TilesScript>();
    private float _offset;

    private int m_idTiles = 0;

    public float TileSize // Calculate the tile Size
    {
        get { return _tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    void Awake ()
    {
        _offset = _tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
        CreateLevel(); //Function called to generate the level
  
    }

    void Update () {
		
	}

    private void CreateLevel()
    {
        string[] _mapData = ReadLevel(); //Read txt to generate the level

        int _mapXSize = _mapData[0].ToCharArray().Length; //Width of the map
        int _mapYSize = _mapData.Length;                  //Height of the map  

        Vector3 _worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height,0)); //Set the starting point of generation to top left corner
        _worldStart.x += _offset;
        _worldStart.y -= _offset;

        //Grid is 5 by 5

        for (int y = 0; y < _mapYSize; y++) //y position
        {
            char[] _newTiles = _mapData[y].ToCharArray(); 

            for (int x = 0; x < _mapXSize; x++) //x position
            {
                PlaceTile(_newTiles[x].ToString(),x,y,_worldStart); //Place a tile each time this function is called
            }
        }

        GameManager.GetManager().InitTilesTab(TilesList.ToArray());//on envoie la liste de tiles sous forme de tableau au gameManager
        GameManager.GetManager().InitGame(m_playerTab);
    }

    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        int _tileIndex = int.Parse(tileType); //string to int
        GameObject _tmpTile = Instantiate(_tilePrefabs[_tileIndex]); //Create a tile at the current position
        _tmpTile.transform.position = new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0); //Move to next position
        _tmpTile.GetComponent<TilesScript>().Id = m_idTiles;
        TilesList.Add(_tmpTile.GetComponent<TilesScript>());//les tiles sont stocké dans la liste au fur et à mesure
        m_idTiles = m_idTiles + 1;
    }
    
    private string[] ReadLevel()
    {
        TextAsset _bindData = Resources.Load(_levelName) as TextAsset;
        string _data = _bindData.text.Replace(Environment.NewLine, string.Empty);
        return _data.Split('-');
    }
}
