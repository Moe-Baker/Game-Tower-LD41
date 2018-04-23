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
    public class Tower : MonoBehaviour, ITowerDamager, IPointerClickHandler
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

        public EnemiesManager EnemiesManager { get { return References.Level.EnemiesManager; } }

        public List<Unit> Units { get; protected set; }

        Tower ITowerDamager.Tower { get { return this; } }

        public virtual void Place()
        {
            enabled = true;

            Units = new List<Unit>();

            parts.DefaultUnit.Init(this);
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

        public virtual Enemy FindEnemy()
        {
            return FindEnemy(Mathf.Infinity);
        }
        public virtual Enemy FindEnemy(float range)
        {
            if (EnemiesManager.List.Count == 0)
                return null;

            if (EnemiesManager.List.Count == 1)
                return EnemiesManager.List[0];

            int targetIndex = 0;
            float closestDistance = Mathf.Infinity;

            for (int i = 0; i < EnemiesManager.List.Count; i++)
            {
                var currentDistance = Vector3.Distance(EnemiesManager.List[i].transform.position, transform.position);

                if (currentDistance < closestDistance)
                {
                    targetIndex = i;
                    closestDistance = currentDistance;
                }
            }

            if (closestDistance > range)
                return null;

            return EnemiesManager.List[targetIndex];
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