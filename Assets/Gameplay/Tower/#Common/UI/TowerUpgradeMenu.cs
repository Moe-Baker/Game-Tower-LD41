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
	public class TowerUpgradeMenu : Menu
	{
		[SerializeField]
        protected CardsUI cardsUI;
        public CardsUI CardsUI { get { return cardsUI; } }

        public Tower Tower { get; protected set; }

        public virtual void Display(Tower tower)
        {
            Show();

            this.Tower = tower;
            Debug.Log(tower.name);

            cardsUI.Query(UpgradeQuery);

            cardsUI.OnClick += OnCardClicked;
        }

        protected virtual bool UpgradeQuery(Card card)
        {
            return card is TowerUpgradeCard;
        }

        private void OnCardClicked(CardUITemplate template)
        {
            cardsUI.OnClick -= OnCardClicked;

            var card = template.Data as TowerUpgradeCard;

            if (References.Level.CardsManager.Inventory.Use(card))
            {
                Tower.AddUnit(card.Prefab);

                Close();
            }
        }
    }
}