using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private float _currentChance = 1f;

    public float CurrentChance
    {
        get => _currentChance;
        set => _currentChance = value;
    }
}

