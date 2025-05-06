using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // ������, �� ������� ������� ������ (�����)
    public SpriteRenderer mapBounds; // PNG �����

    private float cameraHalfWidth;
    private float cameraHalfHeight;

    void Start()
    {
        // �������� ������� ������
        Camera cam = GetComponent<Camera>();
        cameraHalfHeight = cam.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        if (target == null || mapBounds == null) return;

        // ������� �����
        float minX = mapBounds.bounds.min.x + cameraHalfWidth;
        float maxX = mapBounds.bounds.max.x - cameraHalfWidth;
        float minY = mapBounds.bounds.min.y + cameraHalfHeight;
        float maxY = mapBounds.bounds.max.y - cameraHalfHeight;

        // ������� ������ � �������������
        float clampedX = Mathf.Clamp(target.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(target.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}