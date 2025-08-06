using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(InputReader))]
public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Cube _prefab;
    [SerializeField] private float _explosionForce = 500f;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _upwardModifier = 2f;

    private List<Cube> _spawnedObjects = new List<Cube>();

    private void OnEnable() => _inputReader.Cliked += OnClick;
    private void OnDisable() => _inputReader.Cliked -= OnClick;

    private void OnClick()
    {
        float startValue = 0f;
        float halfValue = 0.5f;
        float maxValue = 1f;
        int percent = 100;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Cube[] generators = hit.collider.GetComponents<Cube>();

            if (generators.Length > 0)
            {
                foreach (Cube generator in generators)
                {
                    _spawnedObjects.Clear();
                    Vector3 explosionCenter = generator.transform.position;

                    if (Random.Range(startValue, maxValue) <= _prefab.CurrentChance)
                    {
                        CreateMutableObjects(generator.transform);
                        _prefab.CurrentChance *= halfValue;

                        ApplyExplosionForce(explosionCenter);
                        Debug.Log($"���������� �������! ����� ����: {_prefab.CurrentChance * percent}%");
                    }
                    else
                    {
                        Debug.Log("���������� �� ���������");
                    }

                    Destroy(generator.gameObject);
                }
            }
        }
    }

    private void CreateMutableObjects(Transform generatorTransform)
    {
        float startValue = 0f;
        float halfValue = 0.5f;
        float negativeValue = -1f;
        float positiveValue = 1f;

        int minValue = 2;
        int maxValue = 6;
        int count = Random.Range(minValue, maxValue);
        Vector3 center = generatorTransform.position;

        for (int i = 0; i < count; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(negativeValue, positiveValue),
                Random.Range(startValue, positiveValue), Random.Range(negativeValue, positiveValue)).normalized * halfValue;

            Cube newObject = Instantiate(_prefab, center + randomOffset, Random.rotation);
            _spawnedObjects.Add(newObject);
            Cube currentObject = newObject.GetComponent<Cube>();

            if (currentObject != null)
            {
                currentObject.Initialize(generatorTransform);
            }
        }
    }

    private void ApplyExplosionForce(Vector3 explosionCenter)
    {
        foreach (Cube obj in _spawnedObjects)
        {
            Cube mutable = obj.GetComponent<Cube>();

            if (mutable != null)
            {
                mutable.ApplyExplosionForce(explosionCenter, _explosionForce, _explosionRadius, _upwardModifier);
            }
        }
    }
}