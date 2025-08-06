using System.Collections.Generic;
using UnityEngine;

public class ExplosionCube : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _upwardModifier;

    public void ApplyExplosion(Vector3 explosionCenter, List<Cube> spawnedObjects)
    {
        foreach (Cube spawnedObject in spawnedObjects)
        {
            Cube mutableObject = spawnedObject.GetComponent<Cube>();

            if (mutableObject != null)
            {
                mutableObject.ApplyExplosionForce(explosionCenter, _explosionForce, _explosionRadius, _upwardModifier);
            }
        }
    }
}
