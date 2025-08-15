using UnityEngine;

[RequireComponent(typeof(ExplosionCube))]
[RequireComponent(typeof(RaycastHandler))]
[RequireComponent(typeof(Spawner))]
public class CubeInteractionHandler : MonoBehaviour
{
    [SerializeField] private ExplosionCube _explosion;
    [SerializeField] private RaycastHandler _raycastHandler;
    [SerializeField] private Spawner _spawner;

    bool isSeparation;

    private void OnEnable()
    {
        _raycastHandler.HitDetected += OnHandleHit;
        _spawner.Separation += OnTrySeparation;
    }

    private void Awake()
    {
        _explosion = GetComponent<ExplosionCube>();
        _raycastHandler = GetComponent<RaycastHandler>();
    }

    private void OnDisable()
    {
        _raycastHandler.HitDetected -= OnHandleHit;
        _spawner.Separation -= OnTrySeparation;
    }

    private void OnTrySeparation(bool status)
    {
        isSeparation = status;
    }

    private void OnHandleHit(Cube cube)
    {
        _spawner.ProcessCubeGeneration(cube);

        if (isSeparation)
        {
            _explosion.ApplyExplosionSimple(cube.transform.position, _spawner.SpawnedObjects);
        }
        else
        {
            _explosion.ApplyExplosionToRigidbody(cube.transform.position, _spawner.SpawnedObjects);
        }
    }
}
