using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] _tilePrefabs;
    
    public float TileSize // Calculate the tile Size
    {
        get { return _tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
}

	void Start () { 
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
        //Grid is 5 by 5
        for (int y = 0; y < _mapYSize; y++) //y position
        {
            char[] _newTiles = _mapData[y].ToCharArray(); 

            for (int x = 0; x < _mapXSize; x++) //x position
            {
                PlaceTile(_newTiles[x].ToString(),x,y,_worldStart); //Place a tile each time this function is called
            }
        }
    }

    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        int _tileIndex = int.Parse(tileType); //string to int
        Debug.Log(_tileIndex);
        GameObject _currentTile = Instantiate(_tilePrefabs[_tileIndex]); //Create a tile at the current position
        _currentTile.transform.position = new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0); //Move to next position
    }

    private string[] ReadLevel()
    {
        TextAsset _bindData = Resources.Load("Level1") as TextAsset;
        string _data = _bindData.text.Replace(Environment.NewLine, string.Empty);
        Debug.Log(_data);
        return _data.Split('-');
    }
}
