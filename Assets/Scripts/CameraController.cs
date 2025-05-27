using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Объект, за которым следует камера (игрок)
    public SpriteRenderer mapBounds; // PNG карту

    private float cameraHalfWidth;
    private float cameraHalfHeight;

    private void Start()
    {
        // Получаем размеры камеры
        Camera cam = GetComponent<Camera>();
        cameraHalfHeight = cam.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * cam.aspect;
    }

    private void LateUpdate()
    {
        if (target == null || mapBounds == null) return;

        // Границы карты
        var minX = mapBounds.bounds.min.x + cameraHalfWidth;
        var maxX = mapBounds.bounds.max.x - cameraHalfWidth;
        var minY = mapBounds.bounds.min.y + cameraHalfHeight;
        var maxY = mapBounds.bounds.max.y - cameraHalfHeight;

        // Позиция камеры с ограничениями
        var clampedX = Mathf.Clamp(target.position.x, minX, maxX);
        var clampedY = Mathf.Clamp(target.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}