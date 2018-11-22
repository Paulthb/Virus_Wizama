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

        public Player[] m_playerTab;
        public Player m_currentPlayer;
        public TilesScript[] m_tilesTab;

        // -------------------- ADD 22/11 (Tactical Movement)

        List<TilesScript> _selectableTiles = new List<TilesScript>();

        Stack<TilesScript> path = new Stack<TilesScript>(); // je sais pas encore à quoi ça sert
        TilesScript _currentTile;

        private int _move = 5;
        private int _moveSpeed = 2;
        private Vector3 _velocity = new Vector3();
        private Vector3 _heading = new Vector3();
        
        private void GetCurrentTile(Player player)  //Détecte la tile sur laquelle le Player est situé
        {
            //_currentTile = GetTargetTile(m_currentPlayer.gameObject);
            foreach(TilesScript item in m_tilesTab)
            {
                if(player.transform.position == item.transform.position)          // EDIT : 2 possibilités enfaite soit gérer ça avec le trigger stay du player ou de la tile soit de cette manière
                {                                                                 // mais je pense que la condition d'égalité est assez bancale  
                    _currentTile = item;
                }
            }
        }

        private void ComputeAdjacencyLists()
        {
            foreach (TilesScript tile in m_tilesTab)
                tile.FindNeighbors();
        }

        private void FindSelectableTiles()
        {
            ComputeAdjacencyLists();
            GetCurrentTile(m_currentPlayer);

            // Start BFS

            Queue<TilesScript> _process = new Queue<TilesScript>();

            _process.Enqueue(_currentTile);
            _currentTile.SetVisitedBool(true);

            while(_process.Count > 0)
            {
                TilesScript t = _process.Dequeue();

                _selectableTiles.Add(t);
                t.SetSelectableBool(true);

                if(t.GetDistance() < _move)
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
        //public PlayerRound

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void InitGame(Player[] playerTab)
        {
            m_playerTab = playerTab;

            for (int i = 0; i < m_playerTab.Length; i++)
            {
                m_playerTab[i].Talk();
            }

            m_currentPlayer = playerTab[0];             // Pour les tests
            PlayerPosition();
        }

        public void InitTilesTab(TilesScript[] TilesTab)
        {
            m_tilesTab = TilesTab;           
        }

        public void PlayerPosition()
        {
            m_playerTab[0].transform.position = m_tilesTab[0].transform.position;
           /* m_playerTab[1].transform.position = m_tilesTab[11].transform.position;
            m_playerTab[2].transform.position = m_tilesTab[72].transform.position;
            m_playerTab[3].transform.position = m_tilesTab[83].transform.position;*/
        }

        //public void NextRound()
        //{
        //    if (pla)
        //}
    }
}
