#if UNITY_EDITOR
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

namespace Moe.Tools
{
	public class ChangeTextFont : EditorWindow
	{
		[MenuItem(MoeTools.Constants.Paths.Tools + "Change Text Font")]
        static void Init()
        {
            GetWindow<ChangeTextFont>().Show();
        }

        Font normal;
        Font bold;
        Font italic;
        Font boldAndItalic;
        Font GetFontByStyle(FontStyle stlye)
        {
            switch (stlye)
            {
                case FontStyle.Normal:
                    return normal;

                case FontStyle.Bold:
                    return bold;

                case FontStyle.Italic:
                    return italic;

                case FontStyle.BoldAndItalic:
                    return boldAndItalic;
            }

            throw new ArgumentOutOfRangeException();
        }

        void OnGUI()
        {
            normal = (Font)EditorGUILayout.ObjectField("Normal", normal, typeof(Font), false);
            bold = (Font)EditorGUILayout.ObjectField("Bold", bold, typeof(Font), false);
            italic = (Font)EditorGUILayout.ObjectField("Italic", italic, typeof(Font), false);
            boldAndItalic = (Font)EditorGUILayout.ObjectField("Bold & Italic", boldAndItalic, typeof(Font), false);

            if (GUILayout.Button("Recurse To Selection"))
                Process();
        }

        void Process()
        {
            for (int i = 0; i < Selection.gameObjects.Length; i++)
                Process(Selection.gameObjects[i]);
        }
        void Process(GameObject gameObject)
        {
            List<Text> texts = MoeTools.GameObject.GetAllComponents<Text>(true);

            for (int i = 0; i < texts.Count; i++)
                texts[i].font = GetFontByStyle(texts[i].fontStyle);
        }
	}
}
#endif