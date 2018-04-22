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
	public class ScoreManager : MonoBehaviour
	{
        [SerializeField]
        protected uint value = 0;
        public uint Value { get { return value; } }

        public event Action<uint> OnChanged;
        protected virtual void TriggerOnChanged()
        {
            if (OnChanged != null)
                OnChanged(value);
        }

        public virtual void Add(uint points)
        {
            value += points;

            TriggerOnChanged();
        }
        public virtual void Subtract(uint points)
        {
            value -= points;

            TriggerOnChanged();
        }

        protected virtual void Start()
        {

        }
	}
}