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
        public GameObject[] m_tilesTab;

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
            Debug.Log("ALOOOOOOOOOOOOOOOO");

            for (int i = 0; i < m_playerTab.Length; i++)
            {
                Debug.Log("ALOOOOOOOOOOOOOOOO");
                m_playerTab[i].Talk();
            }

            m_currentPlayer = playerTab[0];
        }

        public void InitTilesTab(GameObject[] TilesTab)
        {
            m_tilesTab = TilesTab;

            Debug.Log(m_playerTab[0]);
            Debug.Log(m_tilesTab[0]);

            m_playerTab[0].transform.position = m_tilesTab[0].transform.position;
            m_playerTab[1].transform.position = m_tilesTab[11].transform.position;
            m_playerTab[2].transform.position = m_tilesTab[72].transform.position;
            m_playerTab[3].transform.position = m_tilesTab[83].transform.position;
        }

        //public void NextRound()
        //{
        //    if (pla)
        //}
    }
}
