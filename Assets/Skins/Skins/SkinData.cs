using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SkinScriptableObject", order = 1)]
public class SkinData : ScriptableObject
{
    public List<Skin> skins = new List<Skin>();
    
    [System.Serializable]
    public class Skin
    {
        [SerializeField]
        private string localizationKey;
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

        public string LocalizationKey => localizationKey;

        public int Cost => cost;

        public bool IsBought { get => isBought; set => isBought = value; }

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                IsSelectedShanged?.Invoke();
            }
        }

        public GameObject Model => model;

        public Sprite Icon => icon;

        public event Action IsSelectedShanged;

    }
}
