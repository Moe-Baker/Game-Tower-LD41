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
	public class CardsInventoryManager : MonoBehaviour
	{
        [SerializeField]
        protected List<Card> list;
        public List<Card> List { get { return list; } }

        public CardsManager CardsManager { get { return References.Level.CardsManager; } }

        protected virtual void Start()
        {
            list = new List<Card>();
        }

        public event Action<Card> OnAdd;
        public virtual void Add(Card card)
        {
            list.Add(card);

            if (OnAdd != null)
                OnAdd(card);
        }

        public event Action<Card> OnUse;
        public virtual void Use(Card card)
        {
            if (!list.Contains(card))
                throw new ArgumentException("Inventory Doesn't have a card of " + card.name + " So It Cannot be used");

            card.Use();

            if (OnUse != null)
                OnUse(card);
        }
    }
}