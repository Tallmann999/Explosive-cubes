using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Generator : MonoBehaviour
{
    [SerializeField] private float _currentChance = 1f; // Начальный шанс 100%

    public float CurrentChance
    {
        get => _currentChance;
        set => _currentChance = value;
    }

    // Другие ваши свойства и методы Generator
}

