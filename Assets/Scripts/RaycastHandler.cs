using System;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class RaycastHandler : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    public event Action<Cube> HitDetected;

    private void OnEnable() => _inputReader.Clicked += OnPerformRaycast;

    private void OnDisable() => _inputReader.Clicked -= OnPerformRaycast;

    private void OnPerformRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<Cube>(out var cube))
            {
                HitDetected?.Invoke(cube);
            }
        }
    }
}