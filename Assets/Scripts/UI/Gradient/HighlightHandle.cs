using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightHandle : MonoBehaviour
{
    [SerializeField] private LinearTwisting _twisting;
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
        Vector2[] size = _twisting.ProjectRectangle(_twisting.Nearest);

        if (_direction == Slider.Direction.LeftToRight ||
            _direction == Slider.Direction.RightToLeft)
        {
            if (transform.position.x > size[0].x && transform.position.x < size[1].x)
            {
                _image.color = _gradient.GetComponentInChildren<UIGradient>().m_color1;
            }
            else
            {
                _image.color = _default;
            }
        }
        else
        {
            if (transform.position.y > size[2].y && transform.position.y < size[3].y)
                _image.color = _gradient.GetComponentInChildren<UIGradient>().m_color1;
            else
                _image.color = _default;
        }
    }
}
