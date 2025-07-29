using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class MutableObject : MonoBehaviour
{
    private GameObject[] _gameObject;

    //private void Start()
    //{
    //}

    public  void HandleClick()
    {
        int randomValue = Random.Range(2,6);

        for (int i = 0; i < randomValue; i++)
        {
            _gameObject[i]= Instantiate(_gameObject[i], CreateRandomPosition(), Quaternion.identity);
        }

        //CreateRandomColor();
        //CreateRandomPosition();
        SetDownSize();
    }

    private Vector3 CreateRandomPosition()
    {
        int randomValue = Random.Range(-15, 15);
        return new Vector3(randomValue, randomValue, randomValue);
    }

    private void SetDownSize()
    {
       
    }
}