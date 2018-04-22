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
    [CreateAssetMenu()]
	public class Card : ScriptableObject
	{
        [SerializeField]
        protected uint useCost = 100;
        public uint UseCost { get { return useCost; } }

        [SerializeField]
        protected Sprite icon;
        public Sprite Icon { get { return icon; } }

        [SerializeField]
        [TextArea]
        protected string description;
        public string Description { get { return description; } }

        public virtual void Use()
        {

        }
    }
}