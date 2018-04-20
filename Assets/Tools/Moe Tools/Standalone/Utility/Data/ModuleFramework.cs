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

namespace Moe.Tools
{
    #region Base
    public interface IBaseMoeModule
    {
        
    }

    #region Modules
    public abstract class BaseMoeModule : IBaseMoeModule
    {

    }

    public abstract class BaseMoeScriptableModule : MonoBehaviour, IBaseMoeModule
    {

    }

    public abstract class BaseMoeBehaviourModule : MonoBehaviour, IBaseMoeModule
    {

    }
    #endregion

    public abstract class BaseMoeModuleManager<TModule>
        where TModule : IBaseMoeModule
    {
        [SerializeField]
        List<TModule> list;
        public List<TModule> List { get { return list; } }

        public virtual void Add<T>(IList<T> modules)
            where T : TModule
        {
            for (int i = 0; i < modules.Count; i++)
                Add(modules[i]);
        }
        public virtual void Add(TModule module)
        {
            if (list.Contains(module))
                throw new ArgumentException("Trying to add module of type " + module.GetType().Name + " But It Was Already Added To The Modules List");

            list.Add(module);
        }
        
        public virtual T Add<T>(GameObject gameObject)
            where T : TModule
        {
            var module = gameObject.GetComponent<T>();

            if (module == null)
                module = gameObject.GetComponentInChildren<T>();

            if (module == null)
                throw new Exception("No Module Of Type " + typeof(T).Name + " Was Found On Gameobject " + gameObject.name);

            Add(module);

            return module;
        }

        public virtual void AddAll(GameObject gameObject)
        {
            AddAll<TModule>(gameObject);
        }
        public virtual void AddAll<T>(GameObject gameObject)
            where T : TModule
        {
            AddAllChildern<T>(gameObject);
        }

        public virtual void AddAllLocal(GameObject gameObject)
        {
            AddAllLocal<TModule>(gameObject);
        }
        public virtual void AddAllLocal<T>(GameObject gameObject)
            where T : TModule
        {
            Add(gameObject.GetComponents<T>());
        }

        public virtual void AddAllChildern(GameObject gameObject)
        {
            AddAllChildern<TModule>(gameObject);
        }
        public virtual void AddAllChildern<T>(GameObject gameObject)
            where T : TModule
        {
            Add(gameObject.GetComponentsInChildren<T>());
        }

        public virtual T Find<T>()
            where T : TModule
        {
            for (int i = 0; i < list.Count; i++)
                if (IsTypeOf(list[i].GetType(), typeof(T)))
                    return (T)list[i];

            throw new ArgumentException("No Module Found Of Type " + typeof(T).Name + " In The Module Manager");
        }
        public virtual List<T> FindAll<T>()
            where T : TModule
        {
            List<T> list = new List<T>();

            for (int i = 0; i < list.Count; i++)
                if (IsTypeOf(list[i].GetType(), typeof(T)))
                    list.Add((T)list[i]);

            return list;
        }
        public virtual bool IsTypeOf(Type value, Type target)
        {
            return value == target || value.IsSubclassOf(target);
        }

        public virtual void ForAll(Action<TModule> action)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == null)
                    throw new NullReferenceException("Module At Index " + i + " Inside A " + GetType().Name + " Is Null");

                action(list[i]);
            }
        }

        public BaseMoeModuleManager()
        {
            list = new List<TModule>();
        }
    }
    #endregion

    #region Basic
    public interface IBasicMoeModule : IBaseMoeModule
    {
        void Init();
    }

    #region Modules
    [Serializable]
    public abstract class MoeBasicModule : IBasicMoeModule
    {
        public virtual void Init()
        {

        }
    }

    public abstract class MoeBasicScriptableModule : ScriptableObject, IBasicMoeModule
    {
        public virtual void Init()
        {

        }
    }

    public abstract class MoeBasicBehaviourModule : MonoBehaviour, IBasicMoeModule
    {
        public virtual void Init()
        {

        }
    }
    #endregion

    [Serializable]
    public abstract class MoeBasicModuleManager<TModule> : BaseMoeModuleManager<TModule>
        where TModule : IBasicMoeModule
    {
        public virtual void Init()
        {
            ForAll(InitModule);
        }
        protected virtual void InitModule(TModule module)
        {
            module.Init();
        }
    }
    #endregion

    #region Linked
    public interface IMoeLinkedModule<TLink> : IBaseMoeModule
    {
        void Init(TLink link);
    }

    #region Modules
    [Serializable]
    public abstract class MoeLinkedModule<TLink> : BaseMoeModule, IMoeLinkedModule<TLink>
    {
        public TLink Link { get; protected set; }

        public virtual void Init(TLink link)
        {
            this.Link = link;
        }
    }

    public abstract class MoeLinkedScriptableModule<TLink> : ScriptableObject, IMoeLinkedModule<TLink>
    {
        public TLink Link { get; protected set; }

        public virtual void Init(TLink link)
        {
            this.Link = link;
        }
    }

    public abstract class MoeLinkedBehaviourModule<TLink> : MonoBehaviour, IMoeLinkedModule<TLink>
    {
        public TLink Link { get; protected set; }

        public virtual void Init(TLink link)
        {
            this.Link = link;
        }
    }
    #endregion

    [Serializable]
    public abstract class MoeLinkedModuleManager<TModule, TLink> : BaseMoeModuleManager<TModule>
        where TModule : IMoeLinkedModule<TLink>
    {
        public TLink Link { get; protected set; }

        public virtual void Init(TLink link)
        {
            this.Link = link;

            ForAll(InitModule);
        }
        protected virtual void InitModule(TModule module)
        {
            module.Init(Link);
        }

        
    }
    #endregion
}