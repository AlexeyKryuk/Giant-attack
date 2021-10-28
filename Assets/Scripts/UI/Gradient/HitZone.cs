using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitZone : MonoBehaviour
{
    [SerializeField] private LinearTwisting _twisting;
    [SerializeField] private Slider _gradientVer;
    [SerializeField] private Slider _gradientHor;
    [SerializeField] private RectTransform _nearestEnemyView;

    private RectTransform _handleVer;
    private RectTransform _handleHor;

    private void Awake()
    {
        _handleVer = _gradientVer.handleRect;
        _handleHor = _gradientHor.handleRect;
    }

    private void Update()
    {
        MoveHandle();
    }

    private void MoveHandle()
    {
        _handleVer.gameObject.SetActive(_twisting.enabled);
        _handleHor.gameObject.SetActive(_twisting.enabled);

        if (_handleHor.gameObject.activeInHierarchy)
        {
            _nearestEnemyView.position = _twisting.GetNearestOnScreen();
            Vector3 nearestPos = _nearestEnemyView.anchoredPosition;

            if (transform.TryGetComponent(out RectTransform rectTransform))
            {
                float XPos = nearestPos.x / rectTransform.rect.width;
                float YPos = nearestPos.y / rectTransform.rect.height;

                _gradientHor.value = XPos;
                _gradientVer.value = YPos;
            }

            FitToSize();
        }
    }

    private void FitToSize()
    {
        Enemy nearestOnScene = _twisting.GetNearest();
        Vector2[] size = _twisting.ProjectRectangle(nearestOnScene);
        float width = size[0].x - size[1].x;
        float height = size[3].y - size[2].y;

        _handleHor.sizeDelta = new Vector2(Mathf.Abs(width), 0);
        _handleVer.sizeDelta = new Vector2(0, Mathf.Abs(height));
    }
}
