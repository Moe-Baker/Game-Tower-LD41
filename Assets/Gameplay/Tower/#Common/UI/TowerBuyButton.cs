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
	public class TowerBuyButton : MonoBehaviour
	{
        Button button;

        public TowerUpgradeCard card;

        Level Level { get { return References.Level; } }

        private void Start()
        {
            button = GetComponent<Button>();

            button.onClick.AddListener(OnClick);

            Level.CardsPlotManager.OnPickup += OnCardPlotPickup;
        }

        private void OnCardPlotPickup(CardPlot obj)
        {
            if (Level.CardsPlotManager.Cards.Count == 0)
                button.interactable = false;
        }

        void OnClick()
        {
            if(Level.ScoreManager.Value >= card.UseCost)
            {
                Level.ScoreManager.Subtract(card.UseCost);

                Level.TowersManager.StartPlacement();
            }
        }
    }
}