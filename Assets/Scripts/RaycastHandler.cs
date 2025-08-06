using System;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class RaycastHandler : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    public event Action<RaycastHit> HitDetected;

    private void OnEnable() => _inputReader.Clicked += OnPerformRaycast;

    private void OnDisable() => _inputReader.Clicked -= OnPerformRaycast;

    private void OnPerformRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            HitDetected?.Invoke(hit);
        }
    }
}