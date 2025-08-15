using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;

    private float _currentSplit = 1f;
    private List<Cube> _spawnedObjects = new List<Cube>();

    public List<Cube> SpawnedObjects => GetCopyObject();

    public void ProcessCubeGeneration(Cube cube)
    {
        CreateCubes(cube.transform);
        _currentSplit = cube.CurrentChance;
    }

    private List<Cube> GetCopyObject()
    {
        return new List<Cube>(_spawnedObjects);
    }

    private void CreateCubes(Transform centerTarget)
    {
        float startValue = 0f;
        float halfValue = 0.5f;
        float negativeValue = -1f;
        float positiveValue = 1f;
        int minValue = 2;
        int maxValue = 6;
        int count = Random.Range(minValue, maxValue);

        Vector3 center = centerTarget.position;
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
                currentObject.Initialize(centerTarget, _currentSplit);
            }
        }
    }
}