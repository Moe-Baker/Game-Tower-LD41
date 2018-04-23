using System;
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

namespace Game
{
	public class HUDMenu : Menu
	{
        [SerializeField]
        protected TowerUpgradeMenu towerUpgrade;
        public TowerUpgradeMenu TowerUpgrade { get { return towerUpgrade; } }

        public CardAquireUI CardAquire;

        public WavePopup WavePopup;

        public virtual void Init()
        {
        }

        private void Start()
        {
            CardAquire.Init();

        }
    }
}