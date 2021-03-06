﻿using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

using Moe.Tools;

namespace Game
{
	public class CardsManager : MonoBehaviour
	{
        [SerializeField]
        protected Card[] list;
        public Card[] List { get { return list; } }
        public virtual Card GetRandomCard()
        {
            return list.GetRandom();
        }

        public CardsInventoryManager Inventory { get; protected set; }

        protected virtual void Start()
        {
            Inventory = GetComponent<CardsInventoryManager>();
        }
    }
}