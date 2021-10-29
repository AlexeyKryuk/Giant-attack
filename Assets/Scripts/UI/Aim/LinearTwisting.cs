using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinearTwisting : Twisting
{
    [SerializeField] private float _slowSpeed;
    [SerializeField] private Slider _horizontal;
    [SerializeField] private Slider _vertical;
    [SerializeField] private Camera _camera;
    [SerializeField] private Aiming _aiming;
    
    private float _fastSpeed;
    private Slider _currentSlider;
    private Side _side = Side.Middle;
    private List<Enemy> _enemies;
    private Enemy _nearest;

    public RectTransform Crosshair { get => _crosshair; private set => _crosshair = value; }
    public Enemy Nearest => _nearest;
    public float FastSpeed => _fastSpeed;
    public Slider CurrentSlider => _currentSlider;

    private void Awake()
    {
        _fastSpeed = _currentSpeed;
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
            StartCoroutine(DisableByTime(0.25f));
        }
    }

    private void Twist(Slider slider)
    {
        if (_side == Side.Middle)
        {
            slider.value += Mathf.Lerp((float)Side.Middle, (float)Side.Right, _currentSpeed * Time.unscaledDeltaTime);
            if (slider.value == (float)Side.Right)
                _side = Side.Right;
        }
        else
        {
            slider.value -= Mathf.Lerp((float)Side.Middle, (float)Side.Right, _currentSpeed * Time.unscaledDeltaTime);
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
        Vector2 nearestOnScreen = GetNearestOnScreen();
        Vector2[] size = ProjectRectangle(GetNearest());

        Enemy enemy = _aiming.Target.collider.GetComponentInParent<Enemy>();

        if (Crosshair.position.x > size[0].x && Crosshair.position.x < size[1].x && _currentSlider != _vertical)
            _currentSpeed = Mathf.Lerp(_currentSpeed, _slowSpeed, 10f * Time.deltaTime);
        else if (enemy == _nearest && Crosshair.position.y > size[2].y && Crosshair.position.y < size[3].y && _currentSlider == _vertical)
            _currentSpeed = Mathf.Lerp(_currentSpeed, _slowSpeed, 10f * Time.deltaTime);
        else
            _currentSpeed = Mathf.Lerp(_currentSpeed, _fastSpeed, 10f * Time.deltaTime);
    }

    public Vector2 GetNearestOnScreen()
    {
        if (_enemies == null)
            return Vector2.zero;

        if (_enemies.Count < 1)
            return Vector2.zero;

        _nearest = GetNearest();

        //Vector3 pos = _nearest.transform.position + _nearest.transform.up * 5;
        Vector3 pos = _nearest.GetComponent<Collider>().bounds.center;
        return _camera.WorldToScreenPoint(pos);
    }

    public Enemy GetNearest()
    {
        Enemy nearest = _enemies[0];

        for (int i = 1; i < _enemies.Count; i++)
        {
            float distanceFirst = Vector3.Distance(_player.transform.position, nearest.transform.position);
            float distanceSecond = Vector3.Distance(_player.transform.position, _enemies[i].transform.position);

            if (distanceSecond < distanceFirst)
                nearest = _enemies[i];
        }

        return nearest;
    }


    public Vector2[] ProjectRectangle(Enemy sceneObject)
    {
        var bounds = sceneObject.GetComponent<Collider>();

        Vector3 minX = bounds.bounds.min;
        Vector3 maxX = new Vector3(bounds.bounds.max.x, bounds.bounds.min.y, bounds.bounds.max.z);

        Vector3 minY = bounds.bounds.min;
        Vector3 maxY = bounds.bounds.max;

        Vector2 minXScreen = _camera.WorldToScreenPoint(minX);
        Vector2 maxXScreen = _camera.WorldToScreenPoint(maxX);

        Vector2 minYScreen = _camera.WorldToScreenPoint(minY);
        Vector2 maxYScreen = _camera.WorldToScreenPoint(maxY);

        float width = maxXScreen.x - minXScreen.x;
        float height = maxYScreen.y - minYScreen.y;

        Vector2[] size = new Vector2[4] { minXScreen, maxXScreen, minYScreen, maxYScreen };

        return size;
    }
}
