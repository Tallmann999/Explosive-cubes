using UnityEngine;

[RequireComponent(typeof(ExplosionCube))]
[RequireComponent(typeof(RaycastHandler))]
[RequireComponent(typeof(Spawner))]
public class CubeInteractionHandler : MonoBehaviour
{
    [SerializeField] private ExplosionCube _explosion;
    [SerializeField] private RaycastHandler _raycastHandler;
    [SerializeField] private Spawner _spawner;

    private void OnEnable() => _raycastHandler.HitDetected += OnHandleHit;

    private void Awake()
    {
        _explosion = GetComponent<ExplosionCube>();
        _raycastHandler = GetComponent<RaycastHandler>();
    }

    private void OnDisable() => _raycastHandler.HitDetected -= OnHandleHit;

    private void OnHandleHit(Cube cube)
    {
        _spawner.ProcessCubeGeneration(cube);
        _explosion.ApplyExplosion(cube.transform.position, _spawner.SpawnedObjects);
    }
}
