using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryDrawing : MonoBehaviour
{
    [SerializeField] private GameObject _sphere;
    [SerializeField] private  GameObject _crosshair;

    private List<Transform> _poolOfSphere = new List<Transform>();
    private int _numberOfSpheres = 200;

    private void Awake()
    {
        CreateSphere(_numberOfSpheres);
    }

    private void CreateSphere(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Transform sphere = Instantiate(_sphere, transform).transform;
            sphere.gameObject.SetActive(false);
            _poolOfSphere.Add(sphere);
        }
    }

    public void SetPoints(Vector3[] points)
    {
        int lastIndex = 0;

        for (int i = 0; i < points.Length; i++)
        {
            if (i % 2 != 0)
                continue;

            if (i > _poolOfSphere.Count - 1)
                CreateSphere(points.Length - _poolOfSphere.Count);

            _poolOfSphere[i].position = points[i];
            _poolOfSphere[i].gameObject.SetActive(true);
            lastIndex = i;
        }
        _crosshair.SetActive(true);
        _crosshair.transform.position = _poolOfSphere[lastIndex - 2].position;

        Vector2 scale = new Vector2(_crosshair.transform.localPosition.z / 20, _crosshair.transform.localPosition.z / 20);
        _crosshair.transform.localScale = scale;
    }

    public void ResetPoints()
    {
        for (int i = 0; i < _poolOfSphere.Count; i++)
        {
            _poolOfSphere[i].gameObject.SetActive(false);
        }

        _crosshair.SetActive(false);
    }
}