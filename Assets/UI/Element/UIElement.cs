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

namespace Game
{
	public class UIElement : MonoBehaviour
	{
        public virtual GameObject Target { get { return gameObject; } }

        public bool Visibile
        {
            get
            {
                return Target.activeSelf;
            }
            set
            {
                if (value)
                    Show();
                else
                    Close();
            }
        }

        public virtual void Show()
        {
            Target.SetActive(true);
        }
        public virtual void Close()
        {
            Target.SetActive(false);
        }
    }
}