using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [Header("Bag Stack")]
    [SerializeField] private List<GameObject> stack = new();
    public float stackHeight;

    void Update()
    {
        UpdateStack();
    }

    private void UpdateStack()
    {
        for (int i = 0; i < stack.Count; i++)
        {
            stack[i].SetActive(i < Mathf.FloorToInt(stackHeight));
        }

        if (stackHeight >= 1.0f && stackHeight <= 20.0f)
        {
            stack[Mathf.FloorToInt(stackHeight) - 1].SetActive(true);
        }
    }
}
