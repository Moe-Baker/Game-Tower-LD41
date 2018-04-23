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
	public class CardsPlotManager : MonoBehaviour
	{
        public CardPlot CurrentSeletion { get; protected set; }

        public List<CardPlot> Cards { get; protected set; }
        protected virtual void GetAllCards()
        {
            Cards = FindObjectsOfType<CardPlot>().ToList();
        }

        public Level Level { get { return References.Level; } }
        public CardsManager CardsManager { get { return References.Level.CardsManager; } }
        public CardsInventoryManager CardsInventoryManager { get { return CardsManager.Inventory; } }

        public virtual void Start()
        {
            InitCards();
        }

        protected virtual void InitCards()
        {
            GetAllCards();

            for (int i = 0; i < Cards.Count; i++)
                InitCard(Cards[i]);
        }
        protected virtual void InitCard(CardPlot card)
        {
            card.OnClick += () => OnCardClick(card);
            card.OnHighlight += () => OnCardHighlight(card);
            card.OnUnHighlight += () => OnCardUnHighlight(card);
        }

        public event Action<CardPlot> OnPickup;
        public virtual void PickupCard(CardPlot card)
        {
            Cards.Remove(card);

            CardsInventoryManager.Add(CardsManager.GetRandomCard());

            if (OnPickup != null)
                OnPickup(card);

            Destroy(card.gameObject);
        }

        public event Action<CardPlot> OnClick;
        protected virtual void OnCardClick(CardPlot card)
        {
            if (OnClick != null)
                OnClick(card);
        }

        public event Action<CardPlot> OnHighlight;
        protected virtual void OnCardHighlight(CardPlot card)
        {
            CurrentSeletion = card;

            if (OnHighlight != null)
                OnHighlight(card);
        }

        public event Action<CardPlot> OnUnHighlight;
        protected virtual void OnCardUnHighlight(CardPlot card)
        {
            if (CurrentSeletion == card)
                CurrentSeletion = null;

            if (OnUnHighlight != null)
                OnUnHighlight(card);
        }
    }
}