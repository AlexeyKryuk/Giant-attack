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
    [SerializeField] private Color _base;
    [SerializeField] private Color _target;

    private Transform _handleAreaVer;
    private Transform _handleAreaHor;

    private void Awake()
    {
        _handleAreaVer = _gradientVer.transform.GetChild(0);
        _handleAreaHor = _gradientHor.transform.GetChild(0);
    }

    private void Update()
    {
        _handleAreaVer.gameObject.SetActive(_twisting.enabled);
        _handleAreaHor.gameObject.SetActive(_twisting.enabled);

        if (_handleAreaHor.gameObject.activeInHierarchy)
        {
            _nearestEnemyView.position = _twisting.GetNearest();
            Vector3 nearestPos = _nearestEnemyView.anchoredPosition;
            
            if (transform.TryGetComponent(out RectTransform rectTransform))
            {
                float XPos = nearestPos.x / rectTransform.rect.width;
                float YPos = nearestPos.y / rectTransform.rect.height;

                _gradientHor.value = XPos;
                _gradientVer.value = YPos;
            }
        }
    }
}
