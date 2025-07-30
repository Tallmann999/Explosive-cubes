using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    //[SerializeField] private float _explosionForce = 10f;
    //[SerializeField] private float _explosionRadius = 5f;
    //[SerializeField] private float _boundarySize = 20f;
    [SerializeField] private GameObject _prefab;
  
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

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Generator[] generators = hit.collider.GetComponents<Generator>();

            if (generators.Length > 0)
            {
                foreach (Generator generator in generators)
                {
                    // Проверяем шанс разделения
                    if (Random.Range(0f, 1f) <= generator.CurrentChance)
                    {
                        // Успех - создаём новые объекты
                        CreateMutableObject(generator.transform);
                        
                        // Уменьшаем шанс для следующего раза
                        generator.CurrentChance *= 0.5f;

                        Debug.Log($"Разделение успешно! Новый шанс: {generator.CurrentChance * 100}%");
                    }
                    else
                    {
                        Debug.Log("Разделение не произошло");
                    }

                    Destroy(generator.gameObject);
                }
            }
        }
    }
    //private void OnClick()
    //{
    //    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
    //    Debug.DrawRay(_camera.transform.position, ray.direction, Color.red);

    //    if (Physics.Raycast(ray, out RaycastHit hit))
    //    {
    //        Generator[] generators = hit.collider.GetComponents<Generator>();

    //        if (generators.Length > 0)
    //        {
    //            foreach (Generator generator in generators)
    //            {
    //                CreateMutableObject(generator.transform); 
    //            }

    //            foreach (Generator generator in generators)
    //            {
    //                Destroy(generator.gameObject); 
    //            }
    //        }
    //    }
    //}

    private void CreateMutableObject(Transform generatorTransform)
    {
        int minValue = 2;
        int maxValue = 7;
        int randomValue = Random.Range(minValue, maxValue); 

        for (int i = 0; i < randomValue; i++)
        {
            GameObject newObject = Instantiate(_prefab, CreateRandomPosition(), Quaternion.identity);
            MutableObject currentObject = newObject.GetComponent<MutableObject>();

            if (currentObject != null)
            {
                currentObject.SetDownSize(generatorTransform);
                currentObject.CreateRandomColor();
            }
        }
    }

    private Vector3 CreateRandomPosition()
    {
        int minvalue = -15;
        int maxvalue = 15;
        int randomValue = Random.Range(minvalue, maxvalue);
        return new Vector3(randomValue, randomValue, randomValue);
    }
}