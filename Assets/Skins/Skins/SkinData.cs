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
        private List<ColorPalettes> colorPalettes;
    }

    [System.Serializable]
    public class ColorPalettes
    {
        [SerializeField]
        private string name;

        [SerializeField]
        private List<ColorPalette> colorPalettes;
    }

    [System.Serializable]
    public class ColorPalette
    {
        [SerializeField]
        private string key;

        [SerializeField]
        private Color color;

        [SerializeField]
        private int cost;
    }
}
