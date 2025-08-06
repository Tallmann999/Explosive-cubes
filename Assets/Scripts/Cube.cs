using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _currentChance = 1f;

    public float CurrentChance
    {
        get => _currentChance;
        set => _currentChance = value;
    }

    private Renderer _renderer;
    private Rigidbody _rigidbody;

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

    public void ApplyExplosionForce(Vector3 explosionCenter, float force, float radius, float upwardModifier)
    {
        float minvalue = 0.8f;
        float maxValue = 1.2f;

        if (_rigidbody != null)
        {
            _rigidbody.AddExplosionForce(force * Random.Range(minvalue, maxValue),
                explosionCenter, radius, upwardModifier, ForceMode.Impulse);
        }
    }

    private void SetDownSize(Transform parentTransform)
    {
        transform.localScale = parentTransform.localScale * 0.5f;
    }

    private void CreateRandomColor()
    {
        if (_renderer != null)
        {
            _renderer.material.color = new Color(
                Random.Range(0.2f, 1f),
                Random.Range(0.2f, 1f),
                Random.Range(0.2f, 1f)
            );
        }
    }
}