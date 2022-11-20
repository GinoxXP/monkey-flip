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
        private int cost;
        [SerializeField]
        private bool isBought;
        [SerializeField]
        private bool isSelected;
        [SerializeField]
        private GameObject model;
        [SerializeField]
        private Sprite icon;

        public string Name => name;

        public int Cost => cost;

        public bool IsBought { get => isBought; set => isBought = value; }

        public bool IsSelected { get => isSelected; set => isSelected = value; }

        public GameObject Model => model;

        public Sprite Icon => icon;
    }
}
