using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCube : MonoBehaviour
{
    [SerializeField] private float _baseForce = 100f;
    [SerializeField] private float _baseRadius = 5f;
    [SerializeField] private float _upwardModifier = 0.5f;
    [SerializeField] private float _sizeForceMultiplier;
    [SerializeField] private float _sizeRadiusMultiplier;

    public void ApplyExplosion(Vector3 explosionCenter, List<Cube> specificCubes)
    {
        List<Rigidbody> allAffectedRigidbodies = GetExplosionObjects(explosionCenter);

        foreach (Rigidbody rb in allAffectedRigidbodies)
        {
            ApplyExplosionToRigidbody(rb, explosionCenter);
        }

        if (specificCubes != null)
        {
            foreach (Cube cube in specificCubes)
            {
                if (cube != null && cube.Rigidbody != null &&
                    !allAffectedRigidbodies.Contains(cube.Rigidbody))
                {
                    ApplyExplosionToRigidbody(cube.Rigidbody, explosionCenter);
                }
            }
        }
    }

    private void ApplyExplosionToRigidbody(Rigidbody rigidbody, Vector3 explosionCenter)
    {
        float startSize = 1f;
        float distance = Vector3.Distance(explosionCenter, rigidbody.position);
        float sizeFactor = startSize / rigidbody.transform.localScale.magnitude;

        float explosionForce = _baseForce * sizeFactor * _sizeForceMultiplier;
        float explosionRadius = _baseRadius * sizeFactor * _sizeRadiusMultiplier;

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