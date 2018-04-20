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
	public class CharactersParser : EditorWindow
	{
        string variablesText;
        string indexText;

        public string FullText
        {
            get
            {
                return variablesText + Environment.NewLine + Environment.NewLine + indexText;
            }
        }

        Vector2 scrollPosition;

        [MenuItem(ArabicFixer.MenuPath + "Character Parser")]
        static void Init()
        {
            GetWindow<CharactersParser>().Show();
        }

        void OnEnable()
        {
            variablesText = "";
            indexText = "";
        }

        void OnGUI()
        {
            if(GUILayout.Button("Select File"))
            {
                var path = EditorUtility.OpenFilePanel("Select Text", "", "");

                if (File.Exists(path))
                    ProcessText(path);
            }

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            {
                EditorGUILayout.SelectableLabel(FullText);
            }
            EditorGUILayout.EndScrollView();
        }

        void ProcessText(string path)
        {
            variablesText = "";
            var lines = File.ReadAllLines(path);

            //public static readonly CharacterData[] Array = new CharacterData[] { Alif, Ba, Ta, Tha };
            indexText = "public static readonly CharacterData[] Array = new CharacterData[] { ";

            for (int i = 0; i < lines.Length; i++)
            {
                variablesText += ParseLine(lines[i], i);

                if (i != lines.Length - 1)
                {
                    variablesText += Environment.NewLine;
                    indexText += ", ";
                }
            }

            indexText += "};";

            Repaint();
        }

        string ParseLine(string line, int index)
        {
            string resault = "public static readonly CharacterData ";
            string[] parts = line.Split(' ');

            resault += parts[0];

            resault += " = new CharacterData(";

            resault += "0x" + parts[1] + ", " + "0x" + parts[2] + ", ";
                
            if(parts.Length == 4)
                resault += "0x" + parts[2] + ", " + "0x" + parts[3] + ", " + "0x" + parts[3];
            else
                resault += "0x" + parts[5] + ", " + "0x" + parts[4] + ", " + "0x" + parts[3];

            resault += ");";

            indexText += parts[0];

            return resault;
        }
	}
}