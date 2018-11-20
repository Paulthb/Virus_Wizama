﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asset.Script
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

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
