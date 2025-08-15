using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _baseForce = 100f;
    [SerializeField] private float _baseRadius = 5f;
    [SerializeField] private float _upwardModifier = 0.5f;
    [SerializeField] private float _sizeForceMultiplier;
    [SerializeField] private float _sizeRadiusMultiplier;

    public void ApplyExplosionToRigidbody(Vector3 explosionCenter, List<Cube> specificCubes)
    {
        List<Rigidbody> allAffectedRigidbodies = GetExplosionObjects(explosionCenter);

        foreach (Rigidbody rigidbody in allAffectedRigidbodies)
        {
            ExplosionToRigidbody(rigidbody, explosionCenter);
        }

        if (specificCubes != null)
        {
            foreach (Cube cube in specificCubes)
            {
                if (cube != null && cube.Rigidbody != null &&
                    !allAffectedRigidbodies.Contains(cube.Rigidbody))
                {
                    ExplosionToRigidbody(cube.Rigidbody, explosionCenter);
                }
            }
        }
    }

    public void ApplyExplosionSimple(Vector3 explosionCenter, List<Cube> spawnedObjects)
    {
        float minvalue = 0.8f;
        float maxValue = 1.2f;

        foreach (Cube spawnedObject in spawnedObjects)
        {
            if (spawnedObject.Rigidbody != null)
            {
                spawnedObject.Rigidbody.AddExplosionForce(_baseForce * Random.Range(minvalue, maxValue),
                explosionCenter, _baseRadius, _upwardModifier, ForceMode.Impulse);
            }
        }
    }

    private void ExplosionToRigidbody(Rigidbody rigidbody, Vector3 explosionCenter)
    {
        float startSize = 1f;
        Vector3 direction = rigidbody.position - explosionCenter;
        float sqrDistance = direction.sqrMagnitude;
        float explosionRadius = _baseRadius * (startSize / rigidbody.transform.localScale.magnitude) * _sizeRadiusMultiplier;
        float sqrExplosionRadius = explosionRadius * explosionRadius;

        float sizeFactor = startSize / rigidbody.transform.localScale.magnitude;
        float explosionForce = _baseForce * sizeFactor * _sizeForceMultiplier;
        float distance = Mathf.Sqrt(sqrDistance);
        float distanceFactor = Mathf.Clamp01(1 - (distance / explosionRadius));
        explosionForce *= distanceFactor;

        rigidbody.AddExplosionForce(explosionForce, explosionCenter, explosionRadius,
            _upwardModifier, ForceMode.Impulse);
    }

    private List<Rigidbody> GetExplosionObjects(Vector3 explosionCenter)
    {
        Collider[] hits = Physics.OverlapSphere(explosionCenter, _baseRadius * _sizeRadiusMultiplier);
        List<Rigidbody> rigidbodys = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null && !rigidbodys.Contains(hit.attachedRigidbody))
            {
                rigidbodys.Add(hit.attachedRigidbody);
            }
        }

        return rigidbodys;
    }
}