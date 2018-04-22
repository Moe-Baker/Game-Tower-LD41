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

using UnityEngine.EventSystems;

namespace Game
{
    public class Tower : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        protected PartsData parts;
        public PartsData Parts { get { return parts; } }
        [Serializable]
        public class PartsData
        {
            [SerializeField]
            protected GameObject _base;
            public GameObject Base { get { return _base; } }
            public const float BaseHeight = 3f;

            [SerializeField]
            protected Unit defaultUnit;
            public Unit DefaultUnit { get { return defaultUnit; } }
            public const float UnitHeight = 3f;

            [SerializeField]
            protected GameObject top;
            public GameObject Top { get { return top; } }
        }

        public List<Unit> Units { get; protected set; }

        protected virtual void Start()
        {
            Units = new List<Unit>();
        }

        public virtual void AddUnit(GameObject prefab)
        {
            var instance = Instantiate(prefab).GetComponent<Unit>();

            instance.transform.SetParent(transform, false);
            instance.transform.localPosition = Vector3.up * (PartsData.BaseHeight + ((Units.Count + 1) * PartsData.UnitHeight));

            parts.Top.transform.localPosition = Vector3.up * (instance.transform.localPosition.y + PartsData.UnitHeight);

            Units.Add(instance);
            instance.Init(this);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            
        }

        public GameObject unitPrefab;

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.H))
            {
                AddUnit(unitPrefab);
            }
        }
    }
}