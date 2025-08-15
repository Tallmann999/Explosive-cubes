using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action<Vector2> Clicked;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Clicked?.Invoke(Input.mousePosition);
        }
    }
}
