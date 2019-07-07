﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Storage
{
    [Serializable]
    public class ShopData
    {
        [SerializeField]
        private List<string> boughtItems = new List<string>();
        public List<string> BoughtItems
        {
            get { return boughtItems; }
        }
    }
}