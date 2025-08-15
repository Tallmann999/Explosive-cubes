using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _currentChance;
    [SerializeField] private Cube _prefab;

    public event Action<bool> Separation;

    private List<Cube> _spawnedObjects = new List<Cube>();

    public List<Cube> SpawnedObjects => GetCopyObject();

    public void ProcessCubeGeneration(Cube generator)
    {
        float startValue = 0f;
        float halfValue = 0.5f;
        float maxValue = 1f;
        int percent = 100;

        if (Random.Range(startValue, maxValue + 0.1f) <= _currentChance)
        {
            CreateCubes(generator.transform);
            _currentChance *= halfValue;
            Separation?.Invoke(true);
            Debug.Log($"Разделение получилось: {_currentChance * percent}%");
        }
        else
        {
            Separation?.Invoke(false);
            Debug.Log("Разделение не получилось!!");
        }

        Destroy(generator.gameObject);
    }

    private List<Cube> GetCopyObject()
    {
        List<Cube> copyCube = _spawnedObjects;
        return copyCube;
    }

    private void CreateCubes(Transform generatorTransform)
    {
        float startValue = 0f;
        float halfValue = 0.5f;
        float negativeValue = -1f;
        float positiveValue = 1f;
        int minValue = 2;
        int maxValue = 6;
        int count = Random.Range(minValue, maxValue);

        Vector3 center = generatorTransform.position;
        _spawnedObjects.Clear();

        for (int i = 0; i < count; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(negativeValue, positiveValue),
                Random.Range(startValue, positiveValue), Random.Range(negativeValue, positiveValue)).normalized * halfValue;

            Cube newObject = Instantiate(_prefab, center + randomOffset, Random.rotation);
            _spawnedObjects.Add(newObject);
            Cube currentObject = newObject;

            if (currentObject != null)
            {
                currentObject.Initialize(generatorTransform);
            }
        }
    }
}