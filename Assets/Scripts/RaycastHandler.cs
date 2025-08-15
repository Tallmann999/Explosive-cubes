using System;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class RaycastHandler : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    public event Action<Cube> HitDetected;

    private void OnEnable() => _inputReader.Clicked += OnPerformRaycast;

    private void OnDisable() => _inputReader.Clicked -= OnPerformRaycast;

    private void OnPerformRaycast(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out Cube resource))
            {
                HitDetected?.Invoke(resource);
            }
        }
    }
}