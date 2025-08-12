using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ExplosionCube))]
[RequireComponent(typeof(RaycastHandler))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private StartCube _startCube;
    [SerializeField] private ExplosionCube _explosion;
    [SerializeField] private RaycastHandler _raycastHandler;

    private List<Cube> _spawnedObjects = new List<Cube>();
    private Vector3 _exsplosionCenter;

    private void OnEnable() => _raycastHandler.HitDetected += OnHandleHit;

    private void Awake()
    {
        _explosion = GetComponent<ExplosionCube>();
        _raycastHandler = GetComponent<RaycastHandler>();
    }

    private void OnDisable() => _raycastHandler.HitDetected -= OnHandleHit;

    private void OnHandleHit(RaycastHit hit)
    {
        _startCube = hit.collider.GetComponent<StartCube>();

        if (_startCube == null)
            return;

        _exsplosionCenter = _startCube.transform.position;
        ProcessCubeGeneration(_startCube, _exsplosionCenter);
    }

    private void ProcessCubeGeneration(StartCube generator, Vector3 explosionCenter)
    {
        float startValue = 0f;
        float halfValue = 0.5f;
        float maxValue = 1f;
        int percent = 100;

        if (Random.Range(startValue, maxValue) <= generator.CurrentChance)
        {
            CreateCubes(generator.transform);
            generator.CurrentChance *= halfValue;
            _explosion.ApplyExplosion(_exsplosionCenter, _spawnedObjects);
            Debug.Log($"Разделение получилось: {generator.CurrentChance * percent}%");
        }
        else
        {
            Debug.Log("Разделение не получилось!!");
        }

        Destroy(generator.gameObject);
        _spawnedObjects.Clear();
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