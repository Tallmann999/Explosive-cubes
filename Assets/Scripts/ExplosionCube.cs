using System.Collections.Generic;
using UnityEngine;

public class ExplosionCube : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _upwardModifier;

    public void ApplyExplosion(Vector3 explosionCenter, List<Cube> spawnedObjects)
    {
        float minvalue = 0.8f;
        float maxValue = 1.2f;

        foreach (Cube spawnedObject in spawnedObjects)
        {
            if (spawnedObject.Rigidbody != null)
            {
                spawnedObject.Rigidbody.AddExplosionForce(_explosionForce * Random.Range(minvalue, maxValue),
                explosionCenter, _explosionRadius, _upwardModifier, ForceMode.Impulse);
            }
        }
    }
}
