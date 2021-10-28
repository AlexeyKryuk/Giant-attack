using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFraming : MonoBehaviour
{
    [SerializeField] private LinearTwisting _twisting; 
    [SerializeField] private Camera _camera;
    [SerializeField] private Texture _texture;
    [SerializeField] private float _defaultBoundsHalfSize = 15.0f;

    private GameObject _target;
    private Collider _renderer;

    void OnGUI()
    {
        if (!_twisting.enabled || Event.current.type != EventType.Repaint) return;

        _target = _twisting.GetNearest().gameObject;
        _renderer = _target.GetComponent<Collider>();

        Vector3 center, extends;
        if (_renderer != null)
        {
            center = _renderer.bounds.center;
            extends = _renderer.bounds.extents;
        }
        else
        {
            center = _target.transform.position;
            extends = new Vector3(_defaultBoundsHalfSize, _defaultBoundsHalfSize, 0);
        }

        if (_camera == null) _camera = Camera.main;

        Vector3[] vec = new Vector3[8];

        vec[0] = _camera.WorldToScreenPoint(new Vector3(center.x - extends.x, center.y + extends.y, center.z + extends.z));
        vec[1] = _camera.WorldToScreenPoint(new Vector3(center.x + extends.x, center.y + extends.y, center.z + extends.z));
        vec[2] = _camera.WorldToScreenPoint(new Vector3(center.x - extends.x, center.y - extends.y, center.z + extends.z));
        vec[3] = _camera.WorldToScreenPoint(new Vector3(center.x + extends.x, center.y - extends.y, center.z + extends.z));

        vec[4] = _camera.WorldToScreenPoint(new Vector3(center.x - extends.x, center.y + extends.y, center.z - extends.z));
        vec[5] = _camera.WorldToScreenPoint(new Vector3(center.x + extends.x, center.y + extends.y, center.z - extends.z));
        vec[6] = _camera.WorldToScreenPoint(new Vector3(center.x - extends.x, center.y - extends.y, center.z - extends.z));
        vec[7] = _camera.WorldToScreenPoint(new Vector3(center.x + extends.x, center.y - extends.y, center.z - extends.z));

        float xMinf, xMaxf, yMinf, yMaxf;
        xMaxf = yMaxf = 0;
        xMinf = yMinf = 10000;

        for (int i = 0; i < 8; i++)
        {
            if (vec[i].x < xMinf) xMinf = vec[i].x;
            if (vec[i].y < yMinf) yMinf = vec[i].y;
            if (vec[i].x > xMaxf) xMaxf = vec[i].x;
            if (vec[i].y > yMaxf) yMaxf = vec[i].y;
        }

        Rect boxRect = new Rect(xMinf, Screen.height - yMinf - (yMaxf - yMinf), xMaxf - xMinf, yMaxf - yMinf);
        if (_texture != null)
        {
            GUI.DrawTexture(boxRect, _texture, ScaleMode.StretchToFill);
        }
        else
        {
            GUI.Box(boxRect, "");
        }
    }
}
