using UnityEngine;

namespace Core.UI
{
    public class MinimapCameraFollow : MonoBehaviour
    {
        [SerializeField] private GameObject _player;

        private void LateUpdate()
        {
            Vector3 newPosition = _player.transform.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;
            float playerRotation = -_player.transform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(90, 0, playerRotation);
        }
    }
}