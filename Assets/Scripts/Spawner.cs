using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private MutableObject _prefab;
    //[SerializeField] private float _explosionForce = 10f;
    //[SerializeField] private float _explosionRadius = 5f;
    //[SerializeField] private float _boundarySize = 20f;
    [SerializeField] private Camera _camera;
   
    private void OnEnable()
    {
        _inputReader.Cliked += OnClick;
    }

    private void OnDisable()
    {
        _inputReader.Cliked -= OnClick;
    }

    private void OnClick()
    {       
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(_camera.transform.position, ray.direction, Color.red);

        if (Physics.Raycast(ray,out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<MutableObject>( out _prefab))
            {
                Debug.Log("Нашел колайдер");
                //_prefab.HandleClick();
                int randomValue = Random.Range(-10,10);
                _prefab = Instantiate(_prefab,new Vector3(randomValue, randomValue, randomValue),Quaternion.identity);
            }
        }
   
    }
}