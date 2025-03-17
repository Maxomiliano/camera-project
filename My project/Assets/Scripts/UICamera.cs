using UnityEngine;

public class UICamera : MonoBehaviour
{
    public float Sensitivity = 2f;
    private float _rotationX = 0f;
    private float _rotationY = 0f;


    void Update()
    {
        if (UIManager.Instance.AnyPanelOpen) return;

        float mouseX = Input.GetAxis("Mouse X") * Sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Sensitivity;

        _rotationY -= mouseY;
        _rotationY = Mathf.Clamp(_rotationY, -90f, 90f);

        _rotationY += mouseX;

        transform.localRotation = Quaternion.Euler(_rotationX, _rotationY, 0);
    }
}
