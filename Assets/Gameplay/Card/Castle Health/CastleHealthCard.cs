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
    [CreateAssetMenu]
	public class CastleHealthCard : Card, IUsableCard
    {
        public float value = 20;

        public override void Use()
        {
            base.Use();

            Level.Current.Castle.Heal(value);
        }
    }

    interface IUsableCard
    {

    }
}