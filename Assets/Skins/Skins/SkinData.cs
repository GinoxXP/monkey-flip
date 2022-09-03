using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SkinScriptableObject", order = 1)]
public class SkinData : ScriptableObject
{
    public List<Skin> skins = new List<Skin>();

    [System.Serializable]
    public class Skin
    {
        [SerializeField]
        private string name;
        [SerializeField]
        private string key;
        [SerializeField]
        private List<ColorPalette> colorPalettes;

        public string Name => name;

        public string Key => key;

        public List<ColorPalette> ColorPalettes => colorPalettes;
    }

    [System.Serializable]
    public class ColorPalette
    {
        [SerializeField]
        private string name;

        [SerializeField]
        private List<ColorSet> colors;

        public string Name => name;

        public List<ColorSet> Colors => colors;
    }

    [System.Serializable]
    public class ColorSet
    {
        [SerializeField]
        private string key;

        [SerializeField]
        private Color color;

        [SerializeField]
        private int cost;

        public string Key => key;

        public Color Color => color;

        public int Cost => cost;
    }
}
