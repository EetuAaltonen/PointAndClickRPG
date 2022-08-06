using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_MainCameraHolder;

    [SerializeField]
    private Transform m_MainCameraTarget;

    [SerializeField]
    private float m_SmoothSpeed = 0.125f;

    [SerializeField]
    private float m_Height = 1.5f;

    [SerializeField]
    private Vector3 m_Offset = new Vector3(0, 6, -6);

    [SerializeField]
    float m_ZoomSensitivity = 0.5f;

    private Camera m_MainCamera;

    void Awake()
    {
        m_MainCamera = m_MainCameraHolder.transform.GetChild(0).gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraZoom();
    }

    void LateUpdate()
    {
        CameraFollow();
        CameraRotate();
    }

    public Camera GetMainCamera()
    {
        return m_MainCamera;
    }

    private void CameraFollow()
    {
        Vector3 actualPosition = m_MainCameraTarget.position + new Vector3(0, m_Height, 0);
        Vector3 smoothedPosition = Vector3.Lerp(m_MainCameraHolder.transform.position, actualPosition, m_SmoothSpeed);
        m_MainCameraHolder.transform.position = smoothedPosition;
        m_MainCamera.transform.localPosition = m_Offset;

        m_MainCamera.transform.LookAt(m_MainCameraHolder.transform.position);
    }

    private void CameraZoom()
    {
        float cameraInput = Input.GetAxis("Mouse_Scroll_Wheel");
        if (cameraInput != 0.0f)
        {
            float zoom = cameraInput > 0 ? -m_ZoomSensitivity : m_ZoomSensitivity;
            m_Offset.y += zoom;
            m_Offset.z -= zoom;
        }
    }

    private void CameraRotate()
    {
        if (Input.GetMouseButton(2))
        {
            float mouseInputX = Input.GetAxis("Mouse_X");
            float mouseInputY = Input.GetAxis("Mouse_Y");

            float rotationSensitivityX = Mathf.Abs(mouseInputX) > 0.0f ? 1.8f : mouseInputX;
            float rotationSensitivityY = Mathf.Abs(mouseInputY) > 0.0f ? 1.8f : mouseInputY;
            float baseRotationPerSecond = 360f * Time.deltaTime;

            Vector3 rotation = new Vector3(
                baseRotationPerSecond * mouseInputY * -rotationSensitivityY,
                baseRotationPerSecond * mouseInputX * rotationSensitivityX,
                0
            );

            m_MainCameraHolder.transform.eulerAngles += rotation;

            // TODO: Limit X-axis rotation between -45f and 45f degree
            /*Vector3 currentRotation = m_MainCameraHolder.transform.eulerAngles;
            currentRotation.x = Mathf.Clamp(currentRotation.x, -45f, 45f);
            m_MainCameraHolder.transform.localRotation = Quaternion.Euler(currentRotation);*/
        }
    }
}
