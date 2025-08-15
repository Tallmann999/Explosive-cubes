using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CreateRandomColor : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        SetRandomColor();
    }

    private void SetRandomColor() => _renderer.material.color = Random.ColorHSV();
}
