using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SkinScriptableObject", order = 1)]
public class SkinData : ScriptableObject
{
    public static readonly string CHIMPANZE_NAME = "Chimpanzee";
    public static readonly string GORILLA_NAME = "Gorilla";

    public List<Skin> skins = new List<Skin>();

    [System.Serializable]
    public class Skin
    {
        [SerializeField]
        private string name;
        [SerializeField]
        private List<ColorPalette> colorPalettes;
        [SerializeField]
        private bool isBought;
        [SerializeField]
        private bool isSelected;
        [SerializeField]
        private GameObject model;
        [SerializeField]
        private Sprite icon;

        public string Name => name;

        public List<ColorPalette> ColorPalettes => colorPalettes;

        public bool IsBought { get => isBought; set => isBought = value; }

        public bool IsSelected => isSelected;

        public GameObject Model => model;

        public Sprite Icon => icon;
    }

    [System.Serializable]
    public class ColorPalette
    {
        [SerializeField]
        private string name;
        [SerializeField]
        private List<ColorSet> colorSets;
        [SerializeField]
        private Material targetMaterial;

        public string Name => name;

        public List<ColorSet> ColorSets => colorSets;

        public Material TargetMaterial => targetMaterial;
    }

    [System.Serializable]
    public class ColorSet
    {
        [SerializeField]
        private Color color;
        [SerializeField]
        private int cost;
        [SerializeField]
        private bool isBought;
        [SerializeField]
        private bool isSelected;

        public Color Color => color;

        public int Cost => cost;

        public bool IsBought { get => isBought; set => isBought = value; }

        public bool IsSelected { get => isSelected; set => isSelected = value; }
    }
}
