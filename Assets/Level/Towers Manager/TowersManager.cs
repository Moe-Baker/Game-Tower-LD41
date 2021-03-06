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
	public class TowersManager : MonoBehaviour
	{
        [SerializeField]
        protected GameObject prefab;
        public GameObject Prefab { get { return prefab; } }

        public Level Level { get { return References.Level; } }
        public CardsPlotManager CardsPlotManager { get { return Level.CardsPlotManager; } }

        public Tower Placement { get; protected set; }
        public CardPlot PlacementCardPlot { get; protected set; }
        public virtual bool IsPlacing { get { return Placement != null; } }

        public AudioClip placementSFX;

		public virtual void StartPlacement()
        {
            if (IsPlacing)
                throw new Exception("Already Placing a tower");

            Placement = Instantiate(prefab).GetComponent<Tower>();

            Placement.enabled = false;
            Placement.gameObject.SetActive(false);

            CardsPlotManager.OnHighlight += PlacementHighlightAction;

            if (CardsPlotManager.CurrentSeletion != null)
                PlacementHighlightAction(CardsPlotManager.CurrentSeletion);

            StartCoroutine(PlacementUpdate());
        }

        protected virtual void PlacementHighlightAction(CardPlot card)
        {
            Placement.gameObject.SetActive(true);

            card.gameObject.SetActive(false);

            if (PlacementCardPlot != null)
                PlacementCardPlot.gameObject.SetActive(true);

            PlacementCardPlot = card;

            Placement.transform.position = card.transform.position;
        }

        protected virtual IEnumerator PlacementUpdate()
        {
            while(true)
            {
                if(Input.GetMouseButton(0) && Placement.gameObject.activeSelf)
                    break;

                yield return new WaitForEndOfFrame();
            }

            EndPlacement();
        }

        protected virtual void EndPlacement()
        {
            Placement.Place();

            Level.CardsPlotManager.OnHighlight -= PlacementHighlightAction;

            Level.CardsPlotManager.PickupCard(PlacementCardPlot);

            Placement = null;

            Level.SFXManager.Play(placementSFX);
        }
    }
}