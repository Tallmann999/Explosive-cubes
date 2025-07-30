using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class MutableObject : MonoBehaviour
{
    [SerializeField] private Color _mColor = Color.white;
 
    private Renderer _renderer;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetDownSize(Transform targetTransform)
    {
        transform.localScale = targetTransform.localScale * 0.5f; // Уменьшаем в 2 раза
    }

    public void CreateRandomColor()
    {
        if (_renderer != null)
        {
            _mColor = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f)
            );
            _renderer.material.color = _mColor;
        }
    }
}