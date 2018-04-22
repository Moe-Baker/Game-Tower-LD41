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
    public class CardPlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public event Action OnClick;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (OnClick != null)
                OnClick();
        }

        public event Action OnHighlight;
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (OnHighlight != null)
                OnHighlight();
        }

        public event Action OnUnHighlight;
        public void OnPointerExit(PointerEventData eventData)
        {
            if (OnUnHighlight != null)
                OnUnHighlight();
        }
    }
}