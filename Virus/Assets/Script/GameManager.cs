using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script
{
    public class GameManager
    {
        static GameManager _manger = null;

        public static GameManager GetManager()
        {
            if (_manger == null)
            {
                _manger = new GameManager(); // patern singleton
            }
            return _manger;
        }

        private List<BombeScript> m_bombList;
        public List<BombeScript> bombList
        {
            get { return m_bombList; }
            set { m_bombList = value; }
        }

        public Player[] m_playerTab;
        public Player m_currentPlayer;
        public TilesScript[] m_tilesTab;

        public int m_diceValue;
        public int _diceValue// ceci est une propertie;
        {
            get { return m_diceValue; }
            set
            {
                m_diceValue = value;
                if (m_diceValue == 0)
                    ChangePlayerRound();//changement de tour quand le compteur du dé est à 0;
            }
        }

        // -------------------- ADD 22/11 (Tactical Movement)

        List<TilesScript> _selectableTiles = new List<TilesScript>();

        Stack<TilesScript> path = new Stack<TilesScript>(); // je sais pas encore à quoi ça sert
        TilesScript _currentTile;
        
        public TilesScript GetCurrentTile(Player player)  //Détecte la tile sur laquelle le Player est situé
        {
            foreach(TilesScript item in m_tilesTab)
            {
                if (player.transform.position == item.transform.position)
                {
                    return item;
                }
            }
            Debug.Log("Impossible de trouver la current tile");
            return null;
        }

        private void ComputeAdjacencyLists()
        {
            foreach (TilesScript tile in m_tilesTab)
            {
                tile.FindNeighbors();
            }
        }

        private void FindSelectableTiles()
        {
            ComputeAdjacencyLists();
            _currentTile = GetCurrentTile(m_currentPlayer);

            // Start BFS

            Queue<TilesScript> _process = new Queue<TilesScript>();

            _process.Enqueue(_currentTile);
            _currentTile.SetVisitedBool(true);

            while(_process.Count > 0)
            {
                TilesScript t = _process.Dequeue();

                _selectableTiles.Add(t);
                t.SetSelectableBool(true);

                if(t.GetDistance() < m_diceValue)
                {     

                    foreach(TilesScript tile in t._adjacentTiles)
                    {
                        if(!tile.GetVisitedBool())
                        {
                            tile.SetParent(t);
                            tile.SetVisitedBool(true);
                            tile.SetDistance(1 + t.GetDistance());
                            _process.Enqueue(tile);
                        }
                    }
                }
            }
        }

        

        // ---------------------   
        
        public enum PlayerRound
        {
            P1 = 0,
            P2,
            P3,
            P4
        }
        public PlayerRound m_currentPlayerRound = PlayerRound.P1;

        

        public void InitGame(Player[] playerTab)
        {
            m_playerTab = playerTab;
            PlayerPosition();
            m_currentPlayer = playerTab[0];
            NextRound();
            m_bombList = new List<BombeScript>();
        }

        public void InitTilesTab(TilesScript[] TilesTab)
        {
            m_tilesTab = TilesTab;           
        }

        public void PlayerPosition()
        {
            m_playerTab[0].transform.position = m_tilesTab[15].transform.position;
            m_playerTab[1].transform.position = m_tilesTab[26].transform.position;
            m_playerTab[2].transform.position = m_tilesTab[99].transform.position;
            m_playerTab[3].transform.position = m_tilesTab[110].transform.position;
        }

        public void ChangePlayerRound() // pas ouf peut être...
        {
            CheckOnBomb();

            if(m_currentPlayerRound == PlayerRound.P1)
            {
                m_currentPlayerRound = PlayerRound.P2;
                m_currentPlayer = m_playerTab[1];
                NextRound();
            }
            else if (m_currentPlayerRound == PlayerRound.P2)
            {
                m_currentPlayerRound = PlayerRound.P3;
                m_currentPlayer = m_playerTab[2];
                NextRound();
            }
            else if (m_currentPlayerRound == PlayerRound.P3)
            {
                m_currentPlayerRound = PlayerRound.P4;
                m_currentPlayer = m_playerTab[3];
                NextRound();
            } 
            else
            {
                m_currentPlayerRound = PlayerRound.P1;
                m_currentPlayer = m_playerTab[0];
                NextRound();
            }
        }

        public void NextRound()//déroulement d'un tour
        {
            m_diceValue = Random.Range(1, 6);
            FindSelectableTiles();
        }

        public void MovePlayer(GameObject tile)//appelé lorsqu'on click sur une tiles accessible par le player;
        {
            if (tile.GetComponent<TilesScript>().GetWalkableBool())
            {
                GetCurrentTile(m_currentPlayer).SetWalkableBool(true);
                m_currentPlayer.transform.position = tile.transform.position;
                TilesScript tileObject = tile.GetComponent<TilesScript>();
                _diceValue -= tileObject.GetDistance();
                tileObject.SetWalkableBool(false);
                foreach (TilesScript t in m_tilesTab)
                    t.Reset();

                FindSelectableTiles();
            }
        }

        public void EndTurn()
        {
            ChangePlayerRound();
        }

        public void CheckOnBomb()
        {
            List<BombeScript> copyBombList = new List<BombeScript>();
            copyBombList.AddRange(m_bombList);

            foreach(BombeScript bomb in copyBombList)
            {
                bomb.UpdateCounter();
            }
        }
    }
}
