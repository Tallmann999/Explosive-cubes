using UnityEngine;

[RequireComponent(typeof(Exploder))]
[RequireComponent(typeof(RaycastHandler))]
[RequireComponent(typeof(Spawner))]
public class CubeInteractionHandler : MonoBehaviour
{
    [SerializeField] private Exploder _explosion;
    [SerializeField] private RaycastHandler _raycastHandler;
    [SerializeField] private Spawner _spawner;

    private void Awake()
    {
        _explosion = GetComponent<Exploder>();
        _raycastHandler = GetComponent<RaycastHandler>();
        _spawner = GetComponent<Spawner>();
    }

    private void OnEnable()
    {
        _raycastHandler.HitDetected += OnHandleHit;
    }

    private void OnDisable()
    {
        _raycastHandler.HitDetected -= OnHandleHit;
    }

    private void OnHandleHit(Cube cube)
    {
        float startValue = 0f;
        float maxValue = 1f;
        int percent = 100;
        bool shouldSplit = Random.Range(startValue, maxValue) <= cube.CurrentChance;

        if (shouldSplit)
        {
            _spawner.ProcessCubeGeneration(cube);
            Debug.Log($"Разделение получилось: {cube.CurrentChance * percent}%");
            _explosion.ApplyExplosionSimple(cube.transform.position, _spawner.SpawnedObjects);
        }
        else
        {
            Debug.Log("Разделение не получилось!!");
            _explosion.ApplyExplosionToRigidbody(cube.transform.position, _spawner.SpawnedObjects);
        }

        Destroy(cube.gameObject);
    }
}
