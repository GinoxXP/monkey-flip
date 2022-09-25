using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerModelController : MonoBehaviour
{
    public static string CHIMPANZE_NAME = "Chimpanze";
    public static string GORILLA_NAME = "Gorilla";

    public List<Monkey> Monkeys { get => monkeys; }

    [SerializeField]
    private List<Monkey> monkeys = new();

    private GameObject currentModel;
    private SmoothJump smoothJump;

    public bool SetMonkey(string tag)
    {
        foreach (var monkey in monkeys)
        {
            if (monkey.Name == tag)
            {
                if (currentModel != null)
                    Destroy(currentModel);

                currentModel = Instantiate(monkey.Model, transform);
                smoothJump.Animator = currentModel.GetComponent<Animator>();
                return true;
            }
        }

        return false;
    }

    private void Start()
    {
        SetMonkey(GORILLA_NAME);
    }

    [Inject]
    private void Init(SmoothJump smoothJump)
    {
        this.smoothJump = smoothJump;
    }

    [System.Serializable]
    public struct Monkey
    {
        [SerializeField]
        private GameObject model;
        [SerializeField]
        private string name;
        public GameObject Model { get => model; set => model = value; }
        public string Name { get => name; set => name = value; }
    }
}
