using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private float _currentChance = 1f;
    private float _halfValue = 0.5f;

    public float CurrentChance => _currentChance;
    public Rigidbody Rigidbody { get; private set; }

    public void Initialize(Transform parentTransform, float parentSplitChance)
    {
        _currentChance = parentSplitChance * _halfValue;
        SetDownSize(parentTransform);
    }

    private void SetDownSize(Transform parentTransform)
    {
        float halfSizwValue = 0.5f;
        transform.localScale = parentTransform.localScale * halfSizwValue;
    }
}