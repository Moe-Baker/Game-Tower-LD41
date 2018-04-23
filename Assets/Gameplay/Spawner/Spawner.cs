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
	public class Spawner : MonoBehaviour
	{
		[SerializeField]
        protected GameObject prefab;
        public GameObject Prefab { get { return prefab; } }

        public virtual void Init()
        {
        }

        void Start()
        {
            StartCoroutine(Procedure());

        }

        protected virtual IEnumerator Procedure()
        {
            while(true)
            {
                Spawn();

                yield return new WaitForSeconds(3f);
            }
        }

        protected virtual void Spawn()
        {
            var instance = Instantiate(prefab, transform.position, transform.rotation);
        }
    }
}