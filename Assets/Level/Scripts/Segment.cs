using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField]
    private List<Item> items;
    [SerializeField]
    private Branch branch;

    public double RandomValue { get; set; }

    public Branch Branch => branch;

    private void Start()
    {
        foreach(var item in items)
        {
            if(1 - item.spawnProbability <= RandomValue)
            {
                item.itemObject.SetActive(true);
                break;
            }
        }
    }

    [System.Serializable]
    public struct Item
    {
        public GameObject itemObject;

        [Range(0, 1)]
        public double spawnProbability;
    }
}
