using System;
using System.Collections.Generic;
using UnityEngine;

public class LootCreation : MonoBehaviour
{
    [SerializeField]
    private List<Loot> lootList = new List<Loot>();
    public int MaxProbabilityValue
    {
        get
        {
            var maxProbabilityValue = 0;

            foreach (var loot in lootList)
                maxProbabilityValue += loot.probabilityValue;

            return maxProbabilityValue;
        }
    }

    public GameObject GetLootObject(int probabilityValue)
    {
        var lootProbabilitiesSum = 0;

        foreach (var loot in lootList)
        {
            if (probabilityValue >= lootProbabilitiesSum && probabilityValue < loot.probabilityValue + lootProbabilitiesSum)
                return loot.lootObject;

            lootProbabilitiesSum += loot.probabilityValue;
        }

        return null;
    }

    [Serializable]
    public struct Loot
    {
        [SerializeField]
        public GameObject lootObject;
        [SerializeField]
        public int probabilityValue;
    }
}
