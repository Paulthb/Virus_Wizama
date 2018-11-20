using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    [SerializeField]
    private GameObject tile;

    public Tiles[][] Grid;

    // Use this for initialization
    void Start ()
    {
        SpawnGrid();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnGrid()
    {
        float tilesSizeX = tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        float tilesSizeY = tile.GetComponent<SpriteRenderer>().sprite.bounds.size.y;

        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 6; j++)
            {

                GameObject temp = Instantiate(tile, new Vector2(i * tilesSizeX, j * tilesSizeY), Quaternion.identity);
                Tiles theTile = temp.GetComponent<Tiles>();
                if (i == 3 && j == 5)
                    theTile.ChangeTypeTile(true);
            }
        }
    }
}

