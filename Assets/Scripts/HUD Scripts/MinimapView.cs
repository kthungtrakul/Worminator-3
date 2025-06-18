using Core.Minimap;
using UnityEngine;

namespace Core.UI
{
    public class MinimapView : MonoBehaviour
    {
        // later inject view model using a DI framework
        private MinimapViewModel _minimapViewModel;

        // later define player position as part of the injected view model
        [SerializeField] private Transform _player;
        [SerializeField] private RectTransform _bearingMarker;

        [Header("Minimap internals")]
        [SerializeField] private RectTransform _minimapFrame;

        private void Awake()
        {
            _minimapViewModel = new MinimapViewModel();
        }

        private void LateUpdate()
        {
            UpdateMapBearingMarker();
        }

        private void UpdateMapBearingMarker()
        {
            var (position, rotation) = _minimapViewModel.CalculateBoundaryMarker(_player.eulerAngles.y);
            Vector2 halfSize = 0.5f * _minimapFrame.sizeDelta;
            Vector2 markerPosition = Vector2.Scale(position, halfSize);
            _bearingMarker.anchoredPosition = markerPosition;
            _bearingMarker.rotation = rotation;
        }
    }
}