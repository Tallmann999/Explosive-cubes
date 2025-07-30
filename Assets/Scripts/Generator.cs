using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Generator : MonoBehaviour
{
    [SerializeField] private float _currentChance = 1f; // ��������� ���� 100%

    public float CurrentChance
    {
        get => _currentChance;
        set => _currentChance = value;
    }

    // ������ ���� �������� � ������ Generator
}

