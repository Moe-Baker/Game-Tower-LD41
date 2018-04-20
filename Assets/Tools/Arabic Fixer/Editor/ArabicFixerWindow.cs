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

namespace Moe.ArabicFixer
{
	public class ArabicFixerWindow : EditorWindow
	{
		[MenuItem(ArabicFixer.MenuPath + "Window")]
        static void Init()
        {
            GetWindow<ArabicFixerWindow>().Show();
        }

        string text;
        string resault;

        void OnEnable()
        {
            text = "السلام عليكم";
            resault = ArabicFixer.Process(text);
        }

        void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            {
                text = TextArea("Text", text);
            }
            if(EditorGUI.EndChangeCheck())
                resault = ArabicFixer.Process(text);

            TextArea("Resault", resault);

            if (GUILayout.Button("Update Resault"))
                resault = ArabicFixer.Process(text);
        }

        string TextArea(string label, string text)
        {
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.LabelField(label);

                text = EditorGUILayout.TextArea(text);
            }
            EditorGUILayout.EndVertical();

            return text;
        }
    }
}