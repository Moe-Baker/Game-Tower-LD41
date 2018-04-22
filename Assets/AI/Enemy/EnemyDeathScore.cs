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
	public class EnemyDeathScore : Enemy.Module
	{
        [SerializeField]
        protected uint points = 100;
        public uint Points { get { return points; } }

        public override void Init(Enemy link)
        {
            base.Init(link);

            link.Entity.OnDied += OnDeath;
        }

        private void OnDeath(IDamager obj)
        {
            if (obj.GameObject != Link.gameObject)
                References.Level.ScoreManager.Add(points);
        }
    }
}