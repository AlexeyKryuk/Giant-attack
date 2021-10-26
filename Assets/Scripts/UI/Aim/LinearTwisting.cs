using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinearTwisting : Twisting
{
    [SerializeField] private Slider _horizontal;
    [SerializeField] private Slider _vertical;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _slowSpeed;
    
    private float _fastSpeed;
    private Slider _currentSlider;
    private Side _side = Side.Middle;
    private List<Enemy> _enemies;

    public RectTransform Crosshair { get => _crosshair; private set => _crosshair = value; }

    private void Awake()
    {
        _fastSpeed = _speed;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _enemies = _player.AllEnemies;
        _currentSlider = _horizontal;
        _horizontal.gameObject.SetActive(true);
        _vertical.gameObject.SetActive(true);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _horizontal.gameObject.SetActive(false);
        _vertical.gameObject.SetActive(false);
    }

    private void Update()
    {
        Twist(_currentSlider);
        FollowSlider(_crosshair);
        TrySlowDown();
    }

    private IEnumerator DisableByTime(float time)
    {
        yield return new WaitForSeconds(time);

        enabled = false;
    }

    protected override void OnTouch()
    {
        if (_currentSlider != _vertical)
            _currentSlider = _vertical;
        else
        {
            StartCoroutine(DisableByTime(0.15f));
        }
    }

    private void Twist(Slider slider)
    {
        if (_side == Side.Middle)
        {
            slider.value += Mathf.Lerp((float)Side.Middle, (float)Side.Right, _speed * Time.unscaledDeltaTime);
            if (slider.value == (float)Side.Right)
                _side = Side.Right;
        }
        else
        {
            slider.value -= Mathf.Lerp((float)Side.Middle, (float)Side.Right, _speed * Time.unscaledDeltaTime);
            if (slider.value == (float)Side.Middle)
                _side = Side.Middle;
        }
    }

    private void FollowSlider(Transform crosshair)
    {
        crosshair.position = new Vector3(_horizontal.handleRect.transform.position.x, _vertical.handleRect.transform.position.y, crosshair.position.z);
    }

    private void TrySlowDown()
    {
        Vector2 nearestOnScreen = GetNearest();

        float distanceX = nearestOnScreen.x - Crosshair.position.x;
        float distanceY = nearestOnScreen.y - Crosshair.position.y;

        if (Mathf.Abs(distanceX) < 100f && _currentSlider != _vertical)
            _speed = Mathf.Lerp(_speed, _slowSpeed, 5f * Time.deltaTime);
        else if (Mathf.Abs(distanceY) < 100f && _currentSlider == _vertical)
            _speed = Mathf.Lerp(_speed, _slowSpeed, 10f * Time.deltaTime);
        else
            _speed = Mathf.Lerp(_speed, _fastSpeed, 10f * Time.deltaTime);
    }

    public Vector2 GetNearest()
    {
        if (_enemies == null)
            return Vector2.zero;

        if (_enemies.Count < 1)
            return Vector2.zero;

        Enemy nearest = _enemies[0];

        for (int i = 1; i < _enemies.Count; i++)
        {
            float distanceFirst = Vector3.Distance(_player.transform.position, nearest.transform.position);
            float distanceSecond = Vector3.Distance(_player.transform.position, _enemies[i].transform.position);

            if (distanceSecond < distanceFirst)
                nearest = _enemies[i];
        }

        Vector3 pos = nearest.transform.position + nearest.transform.up * 4;
        return _camera.WorldToScreenPoint(pos);
    }
}
