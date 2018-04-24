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

using Moe.Tools;

namespace Game
{
	public class TitleMenu : Menu
	{
        public static bool BeenDisplayed { get; protected set; }

        [SerializeField]
        protected Menu nextMenu;
        public Menu NextMenu { get { return nextMenu; } }

        protected virtual void Reset()
        {
            try
            {
                nextMenu = FindObjectOfType<MainMenu>().StartMenu;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual void Init()
        {
            if (!Visibile)
                Show();

            if (nextMenu.Visibile)
                nextMenu.Close();

            StartCoroutine(Procedure());
        }

        protected virtual IEnumerator Procedure()
        {
            if (!BeenDisplayed)
            {
                BeenDisplayed = true;

                yield return new WaitUntil(GetInput);
            }

            Close();
        }
        protected virtual bool GetInput()
        {
            if (Input.anyKey)
                return true;

            return Input.touchCount > 0;
        }

        public override void Close()
        {
            base.Close();

            nextMenu.Show();
        }
    }
}
