using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action Cliked;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("� ������� �� ������");
            Cliked?.Invoke();
        }
    }
}
