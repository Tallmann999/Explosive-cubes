using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action Clicked;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Clicked?.Invoke();
        }
    }
}
