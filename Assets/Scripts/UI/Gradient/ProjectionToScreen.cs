using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionToScreen : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public Vector2 ProjectRectangle(Collider sceneObject)
    {
        Vector2 minX = sceneObject.bounds.min;
        Vector2 maxX = new Vector2(sceneObject.bounds.max.x, sceneObject.bounds.min.y);

        Vector2 minY = sceneObject.bounds.min;
        Vector2 maxY = sceneObject.bounds.max;

        Vector2 minXScreen = _camera.WorldToScreenPoint(minX);
        Vector2 maxXScreen = _camera.WorldToScreenPoint(maxX);

        Vector2 minYScreen = _camera.WorldToScreenPoint(minY);
        Vector2 maxYScreen = _camera.WorldToScreenPoint(maxY);

        float width = maxXScreen.x - minXScreen.x;
        float height = maxYScreen.y - minYScreen.y;

        return new Vector2(width, height);
    }
}
