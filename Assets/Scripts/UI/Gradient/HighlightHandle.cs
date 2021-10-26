using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightHandle : MonoBehaviour
{
    [SerializeField] private Slider _gradient;
    [SerializeField] private Image _image;

    private Color _default;
    private Slider.Direction _direction;
    private float _distance;

    private void Awake()
    {
        _direction = GetComponentInParent<Slider>().direction;
        _default = _image.color;
    }

    private void Update()
    {
        if (_direction == Slider.Direction.LeftToRight ||
            _direction == Slider.Direction.RightToLeft)
        {
            _distance = transform.position.x - _gradient.handleRect.position.x;
        }
        else
        {
            _distance = transform.position.y - _gradient.handleRect.position.y;
        }

        if (Mathf.Abs(_distance) < 30f)
            _image.color = _gradient.GetComponentInChildren<UIGradient>().m_color1;
        else
            _image.color = _default;
    }
}
