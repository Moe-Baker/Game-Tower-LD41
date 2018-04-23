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

        public Level Level { get { return Level.Current; } }
        public CardsManager CardsManager { get { return Level.CardsManager; } }

        protected virtual void Start()
        {

        }

        public event Action<Card> OnAdd;
        public virtual void Add(Card card)
        {
            list.Add(card);

            if (OnAdd != null)
                OnAdd(card);
        }

        public event Action<Card> OnUse;
        public virtual bool Use(Card card)
        {
            if (!list.Contains(card))
                throw new ArgumentException("Inventory Doesn't have a card of " + card.name + " So It Cannot be used");

            if (Level.ScoreManager.Value > card.UseCost)
            {
                Level.ScoreManager.Subtract(card.UseCost);
                card.Use();
                list.Remove(card);

                if (OnUse != null)
                    OnUse(card);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}