using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _currentChance = 1f;

    private Renderer _renderer;
    private Rigidbody _rigidbody;

    public Rigidbody Rigidbody => _rigidbody;
    public float CurrentChance
    {
        get => _currentChance;
        set => _currentChance = value;
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(Transform parentTransform)
    {
        SetDownSize(parentTransform);
        CreateRandomColor();
    }

    private void SetDownSize(Transform parentTransform)
    {
        float halfSizwValue = 0.5f;

        transform.localScale = parentTransform.localScale * halfSizwValue;
    }

    private void CreateRandomColor()
    {
        float minValue = 0.2f;
        float maxValue = 1f;

        if (_renderer != null)
        {
            _renderer.material.color = new Color(
                Random.Range(minValue, maxValue),
                Random.Range(minValue, maxValue),
                Random.Range(minValue, maxValue)
            );
        }
    }
}