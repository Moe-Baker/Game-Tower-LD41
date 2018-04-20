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
    [DisallowMultipleComponent]
    public abstract class SelectableInputRelay : MonoBehaviour
    {
        protected List<Callback> Callbacks = new List<Callback>();
        protected virtual void InvokeCallbacks()
        {
            for (int i = 0; i < Callbacks.Count; i++)
                Callbacks[i].Invoke();
        }

        public virtual bool Register(Callback callback)
        {
            if (Callbacks.Contains(callback))
            {
                Debug.LogWarning("Cannot Add Callback " + callback.ToString() + " Because It Was Already Added To The Callbacks List");
                return false;
            }

            Callbacks.Add(callback);
            return true;
        }
        public virtual bool UnRegister(Callback callback)
        {
            if (!Callbacks.Contains(callback))
            {
                Debug.LogWarning("Cannot Remove Callback " + callback.ToString() + " Because It Wasn't Added To The Callbacks List");
                return false;
            }

            Callbacks.Remove(callback);
            return true;
        }

        public delegate void Callback();
    }

    public abstract class SelectableInputRelay<TController> : SelectableInputRelay
    {
        public TController Controller { get; protected set; }

        protected virtual void Awake()
        {
            Controller = GetComponent<TController>();
        }
    }
}