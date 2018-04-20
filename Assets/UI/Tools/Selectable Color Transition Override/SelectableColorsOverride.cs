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

using Moe.Tools;

namespace Game
{
	public class SelectableColorsOverride : MonoBehaviour
	{
		[SerializeField]
        protected SelectableColorsProfile profile;
        public SelectableColorsProfile Profile { get { return profile; } }

        protected virtual void Awake()
        {
            Apply();
        }

        protected virtual void Apply()
        {
            var Selectable = GetComponent<Selectable>();

            if (Selectable == null)
                throw MoeTools.ExceptionTools.Templates.MissingDependacny<Selectable, SelectableColorsOverride>(this.name);

            Selectable.colors = profile.ColorBlock;
        }
    }
}