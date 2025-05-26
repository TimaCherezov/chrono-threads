using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Объект, за которым следует камера (игрок)
    public SpriteRenderer mapBounds; // PNG карту

    private float cameraHalfWidth;
    private float cameraHalfHeight;

    void Start()
    {
        // Получаем размеры камеры
        Camera cam = GetComponent<Camera>();
        cameraHalfHeight = cam.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        if (target == null || mapBounds == null) return;

        // Границы карты
        float minX = mapBounds.bounds.min.x + cameraHalfWidth;
        float maxX = mapBounds.bounds.max.x - cameraHalfWidth;
        float minY = mapBounds.bounds.min.y + cameraHalfHeight;
        float maxY = mapBounds.bounds.max.y - cameraHalfHeight;

        // Позиция камеры с ограничениями
        float clampedX = Mathf.Clamp(target.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(target.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}