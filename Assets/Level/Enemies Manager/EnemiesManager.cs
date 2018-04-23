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
	public class EnemiesManager : MonoBehaviour
	{
		[SerializeField]
        protected List<Enemy> list;
        public List<Enemy> List { get { return list; } }

        public virtual void Add(Enemy enemy)
        {
            list.Add(enemy);

            enemy.Entity.OnDied += (IDamager damager) => OnDeath(enemy, damager);
        }

        protected virtual void OnDeath(Enemy enemy, IDamager damager)
        {
            list.Remove(enemy);
        }
    }
}