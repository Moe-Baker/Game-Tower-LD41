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

namespace Moe.ArabicFixer
{
	public static class ArabicFixer
	{
        public const string MenuPath = MoeTools.Constants.Paths.Menu + "Arabic Fixer/";

        public static string Process(string text)
        {
            if (text == null || text.Length < 2)
                return text;

            string resault = string.Empty;

            List<string> lines = new List<string>();

            CharactersIndex index;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '\n')
                {
                    lines.Add(resault);

                    resault = string.Empty;

                    continue;
                }

                UpdateIndex(text, i, out index);

                if(index.Current == null)
                {
                    if (char.IsNumber(text[i]))
                    {
                        try
                        {
                            resault += CharacterMap.Numbers[int.Parse(text[i].ToString())];
                        }
                        catch (Exception)
                        {
                            resault += text[i];
                        }
                    }
                    else
                        resault += text[i];
                }
                else
                {
                    if(index.Next)
                    {
                        if (index.Current == CharacterMap.Lam && index.Next == CharacterMap.Alif)
                        {
                            if (index.Previous && CharacterMap.IsConnector(index.Previous.General))
                                resault += CharacterMap.LamWaAlef.Medial;
                            else
                                resault += CharacterMap.LamWaAlef.Isolated;

                            i++;

                            continue;
                        }
                    }

                    //Final
                    if (!index.Next)
                    {
                        if (index.Previous && CharacterMap.IsConnector(index.Previous.General))
                            resault += index.Current.Final;
                        else
                            resault += index.Current.Isolated;
                    }
                    //Initial
                    else if (!index.Previous && index.Next)
                    {
                        resault += index.Current.Initial;
                    }
                    //Medial
                    else if (index.Previous && index.Previous)
                    {
                        if (CharacterMap.IsConnector(index.Previous.General))
                            resault += index.Current.Medial;
                        else
                            resault += index.Current.Initial;
                    }
                }
            }

            lines.Add(resault);
            resault = string.Empty;

            for (int i = 0; i < lines.Count; i++)
            {
                resault += new string(lines[i].Reverse().ToArray());

                if (i != lines.Count - 1)
                    resault += Environment.NewLine;
            }

            return resault;
        }
        
        public static void UpdateIndex(string text, int index, out CharactersIndex current)
        {
            if (index == 0)
                current = new CharactersIndex(null,
                    CharacterMap.GetCharacter(text[index]),
                    CharacterMap.GetCharacter(text[index + 1]));

            else if (index == text.Length - 1)
                current = new CharactersIndex(CharacterMap.GetCharacter(text[index - 1]),
                    CharacterMap.GetCharacter(text[index]),
                    null);

            else
                current = new CharactersIndex(CharacterMap.GetCharacter(text[index - 1]),
                    CharacterMap.GetCharacter(text[index]),
                    CharacterMap.GetCharacter(text[index + 1]));
        }

        [Serializable]
        public struct CharactersIndex
        {
            public CharacterData Previous { get; private set; }
            public CharacterData Current { get; private set; }
            public CharacterData Next { get; private set; }

            public CharactersIndex(CharacterData previous, CharacterData current, CharacterData next)
            {
                this.Previous = previous;
                this.Current = current;
                this.Next = next;
            }
        }

        public static class CharacterMap
        {
            #region Variables
            public static readonly CharacterData Alif = new CharacterData(0x0627, 0xFE8D, 0xFE8D, 0xFE8E, 0xFE8E);
            public static readonly CharacterData Ba = new CharacterData(0x0628, 0xFE8F, 0xFE91, 0xFE92, 0xFE90);
            public static readonly CharacterData Ta = new CharacterData(0x062A, 0xFE95, 0xFE97, 0xFE98, 0xFE96);
            public static readonly CharacterData Tha = new CharacterData(0x062B, 0xFE99, 0xFE9B, 0xFE9C, 0xFE9A);
            public static readonly CharacterData Geem = new CharacterData(0x062C, 0xFE9D, 0xFE9F, 0xFEA0, 0xFE9E);
            public static readonly CharacterData Haa = new CharacterData(0x062D, 0xFEA1, 0xFEA3, 0xFEA4, 0xFEA2);
            public static readonly CharacterData Kha = new CharacterData(0x062E, 0xFEA5, 0xFEA7, 0xFEA8, 0xFEA6);
            public static readonly CharacterData Dal = new CharacterData(0x062F, 0xFEA9, 0xFEA9, 0xFEAA, 0xFEAA);
            public static readonly CharacterData Zal = new CharacterData(0x0630, 0xFEAB, 0xFEAB, 0xFEAC, 0xFEAC);
            public static readonly CharacterData Ra = new CharacterData(0x0631, 0xFEAD, 0xFEAD, 0xFEAE, 0xFEAE);
            public static readonly CharacterData Zay = new CharacterData(0x0632, 0xFEAF, 0xFEAF, 0xFEB0, 0xFEB0);
            public static readonly CharacterData Seen = new CharacterData(0x0633, 0xFEB1, 0xFEB3, 0xFEB4, 0xFEB2);
            public static readonly CharacterData Sheen = new CharacterData(0x0634, 0xFEB5, 0xFEB7, 0xFEB8, 0xFEB6);
            public static readonly CharacterData Sad = new CharacterData(0x0635, 0xFEB9, 0xFEBB, 0xFEBC, 0xFEBA);
            public static readonly CharacterData Dad = new CharacterData(0x0636, 0xFEBD, 0xFEBF, 0xFEC0, 0xFEBE);
            public static readonly CharacterData Taa = new CharacterData(0x0637, 0xFEC1, 0xFEC3, 0xFEC4, 0xFEC2);
            public static readonly CharacterData Za = new CharacterData(0x0638, 0xFEC5, 0xFEC7, 0xFEC8, 0xFEC6);
            public static readonly CharacterData Ayn = new CharacterData(0x0639, 0xFEC9, 0xFECB, 0xFECC, 0xFECA);
            public static readonly CharacterData Gayn = new CharacterData(0x063A, 0xFECD, 0xFECF, 0xFED0, 0xFECE);
            public static readonly CharacterData Fa = new CharacterData(0x0641, 0xFED1, 0xFED3, 0xFED4, 0xFED2);
            public static readonly CharacterData Qaf = new CharacterData(0x0642, 0xFED5, 0xFED7, 0xFED8, 0xFED6);
            public static readonly CharacterData Kaf = new CharacterData(0x0643, 0xFED9, 0xFEDB, 0xFEDC, 0xFEDA);
            public static readonly CharacterData Lam = new CharacterData(0x0644, 0xFEDD, 0xFEDF, 0xFEE0, 0xFEDE);
            public static readonly CharacterData Meem = new CharacterData(0x0645, 0xFEE1, 0xFEE3, 0xFEE4, 0xFEE2);
            public static readonly CharacterData Noon = new CharacterData(0x0646, 0xFEE5, 0xFEE7, 0xFEE8, 0xFEE6);
            public static readonly CharacterData Ha = new CharacterData(0x0647, 0xFEE9, 0xFEEB, 0xFEEC, 0xFEEA);
            public static readonly CharacterData Waw = new CharacterData(0x0648, 0xFEED, 0xFEED, 0xFEEE, 0xFEEE);
            public static readonly CharacterData Ya = new CharacterData(0x064A, 0xFEF1, 0xFEF3, 0xFEF4, 0xFEF2);
            public static readonly CharacterData AlifMaddah = new CharacterData(0x0622, 0xFE81, 0xFE81, 0xFE82, 0xFE82);
            public static readonly CharacterData TaMarbutah = new CharacterData(0x0629, 0xFE93, 0xFEF3, 0xFEF4, 0xFE94);
            public static readonly CharacterData AlifMaqsurah = new CharacterData(0x0649, 0xFEEF, 0xFEF3, 0xFEF4, 0xFEF0);
            public static readonly CharacterData AlifBeHamza = new CharacterData(0x0623, 0xFE83, 0xFE83, 0xFE84, 0xFE84);
            public static readonly CharacterData YaBeHamza = new CharacterData(0x0626, 0xFE89, 0xFE8C, 0xFE8B, 0xFE8A);

            public static readonly CharacterData[] Array = new CharacterData[] { Alif, Ba, Ta, Tha, Geem, Haa, Kha, Dal, Zal, Ra, Zay, Seen, Sheen, Sad, Dad, Taa, Za, Ayn, Gayn, Fa, Qaf, Kaf, Lam, Meem, Noon, Ha, Waw, Ya, AlifMaddah, TaMarbutah, AlifMaqsurah, AlifBeHamza, YaBeHamza };

            //Special Characters
            public static readonly CharacterData LamWaAlef = new CharacterData(0xFEFB, 0xFEFB, 0xFEFB, 0xFEFC, 0xFEFC);

            //Numbers
            public static readonly char Num0 = FromUnicode(0x06F0);
            public static readonly char Num1 = FromUnicode(0x06F1);
            public static readonly char Num2 = FromUnicode(0x06F2);
            public static readonly char Num3 = FromUnicode(0x06F3);
            public static readonly char Num4 = FromUnicode(0x06F4);
            public static readonly char Num5 = FromUnicode(0x06F5);
            public static readonly char Num6 = FromUnicode(0x06F6);
            public static readonly char Num7 = FromUnicode(0x06F7);
            public static readonly char Num8 = FromUnicode(0x06F8);
            public static readonly char Num9 = FromUnicode(0x06F9);

            public static readonly char[] Numbers = new char[] { Num0, Num1, Num2, Num3, Num4, Num5, Num6, Num7, Num8, Num9 };
            #endregion

            public static CharacterData GetCharacter(char character)
            {
                for (int i = 0; i < Array.Length; i++)
                {
                    if (Array[i].IsAny(character))
                        return Array[i];
                }

                if (LamWaAlef.IsAny(character))
                    return LamWaAlef;

                return null;
            }
            public static bool IsArabic(char character)
            {
                for (int i = 0; i < Array.Length; i++)
                {
                    if (Array[i].IsAny(character))
                        return true;
                }

                return false;
            }

            public static bool IsConnector(char character)
            {
                if (Alif.IsAny(character)) return false;

                if (Dal.IsAny(character)) return false;
                if (Zal.IsAny(character)) return false;

                if (Ra.IsAny(character)) return false;
                if (Zay.IsAny(character)) return false;

                if (Waw.IsAny(character)) return false;

                if (AlifMaddah.IsAny(character)) return false;
                if (AlifMaqsurah.IsAny(character)) return false;
                if (AlifBeHamza.IsAny(character)) return false;

                if (TaMarbutah.IsAny(character)) return false;

                if (LamWaAlef.IsAny(character)) return false;

                return true;
            }

            public static char FromUnicode(int code)
            {
                return (char)code;
            }

            public static class Variants
            {
                public static char GetIsolated(char character)
                {
                    return character;
                }

                public static char GetInitial(char character)
                {
                    return character;
                }

                public static char GetMedial(char character)
                {
                    return character;
                }

                public static char GetFinal(char character)
                {
                    return character;
                }
            }
        }

        [Serializable]
        public class CharacterData
        {
            public char[] Array { get; protected set; }

            public const int GeneralIndex = 0;
            public char General { get { return Array[GeneralIndex]; } }
            public bool IsGeneral(char character) { return character == General; }

            public const int IsolatedIndex = 1;
            public char Isolated { get { return Array[IsolatedIndex]; } }
            public bool IsIsolated(char character) { return character == Isolated; }

            public const int InitialIndex = 2;
            public char Initial { get { return Array[InitialIndex]; } }
            public bool IsInitial(char character) { return character == Initial; }

            public const int MedialIndex = 3;
            public char Medial { get { return Array[MedialIndex]; } }
            public bool IsMedial(char character) { return character == Medial; }

            public const int FinalIndex = 4;
            public char Final { get { return Array[FinalIndex]; } }
            public bool IsFinal(char character) { return character == Final; }

            public const int Length = 5;

            public bool IsAny(char character) { return Array.Contains(character); }

            public override string ToString()
            {
                return "General:" + General + ", " +
                "Isolated:" + Isolated + ", " +
                "Initial:" + Initial + ", " +
                "Medial:" + Medial + ", " +
                "Final:" + Final + ", ";
            }

            public CharacterData(string parseString)
            {
                if (parseString == null || parseString.Length < Length)
                    throw new ArgumentException("Parse String Must Be 4 Chracters Long");

                Construct(parseString[GeneralIndex],
                    parseString[IsolatedIndex],
                    parseString[InitialIndex],
                    parseString[MedialIndex],
                    parseString[FinalIndex]);
            }
            public CharacterData(int general, int Isolated, int initial, int medial, int final)
            {
                Construct(CharacterMap.FromUnicode(general),
                    CharacterMap.FromUnicode(Isolated),
                    CharacterMap.FromUnicode(initial),
                    CharacterMap.FromUnicode(medial),
                    CharacterMap.FromUnicode(final));
            }
            public CharacterData(char general, char isolated, char initial, char medial, char final)
            {
                Construct(general, isolated, initial, medial, final);
            }

            void Construct(char general, char isolated, char initial, char medial, char final)
            {
                Array = new char[Length];

                Array[GeneralIndex] = general;
                Array[IsolatedIndex] = isolated;
                Array[InitialIndex] = initial;
                Array[MedialIndex] = medial;
                Array[FinalIndex] = final;
            }

            public static implicit operator bool(CharacterData data)
            {
                return data != null;
            }
        }
	}
}