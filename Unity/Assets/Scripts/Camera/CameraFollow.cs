using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform m_Target;

    [SerializeField]
    private float m_SmoothSpeed = 0.125f;

    [SerializeField]
    private float m_Height;

    [SerializeField]
    private Vector3 m_Offset;

    [SerializeField]
    float m_ZoomSensitivity = 0.5f;
    
    // Update is called once per frame
    void Update () {
        CameraZoom();
    }

    void LateUpdate()
    {
        Vector3 actualPosition = m_Target.position + m_Offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, actualPosition, m_SmoothSpeed);
        transform.position = m_Target.position + m_Offset + new Vector3(0, m_Height, 0);


        transform.LookAt(m_Target);
    }

    void CameraZoom()
    {
        float cameraInput = Input.GetAxis("Mouse ScrollWheel");
        if (cameraInput != 0.0f)
        {
            float zoom = cameraInput > 0 ? -m_ZoomSensitivity : m_ZoomSensitivity;
            m_Offset.y += zoom;
            m_Offset.z -= zoom;
        }
    }
}
