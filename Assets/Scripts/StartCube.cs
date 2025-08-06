using UnityEngine;

public class StartCube : MonoBehaviour
{
    [SerializeField] private float _currentChance = 1f;

    public float CurrentChance
    {
        get => _currentChance;
        set => _currentChance = value;
    }

}
