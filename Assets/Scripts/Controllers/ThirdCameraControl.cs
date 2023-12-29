using UnityEngine;

public class ThirdCameraControl : MonoBehaviour
{

    [Header("MainCamera")]
    public GameObject m_camera;
    public float m_cameraSmooth = 16.0f;

    [Header("MouseSensitivity")]
    public float m_sensitivityX = 5f;
    public float m_sensitivityY = 5f;


    [HideInInspector]
    Vector2 m_axis = Vector2.zero;
    GameObject m_followTarget;
    string InputX = "Mouse X";
    string InputY = "Mouse Y";

    bool m_lock = false;

    public void SetMainCamera()
    {
        m_camera = Camera.main.transform.parent.gameObject;
    }

    public void UpdateCamera()
    {
        if (!m_lock)
        {
            RotateCamera();
            MoveCamera();
        }
    }

    public void CameraFollowTo(GameObject obj)
    {
        m_followTarget = obj;
    }

    public void ToggleCameraLock()
    {
        m_lock = !m_lock;
    }

    public float GetDirection()
    {
        return m_camera.transform.eulerAngles.y;
    }

    private void MoveCamera()
    {
        if (m_followTarget != null)
            m_camera.transform.position = m_followTarget.transform.position;
    }

    private void RotateCamera()
    {
        m_axis.x += Input.GetAxis(InputX) * m_sensitivityX;
        m_axis.y -= Input.GetAxis(InputY) * m_sensitivityY;

        if (m_axis.magnitude != 0)//마우스 움직임이 있을때만
        {
            m_axis.x = Mathf.Clamp(m_axis.x, -360, 360);
            m_axis.y = Mathf.Clamp(m_axis.y, 0, 70);

            Quaternion newRot = Quaternion.Euler(m_axis.y, m_axis.x, 0);
            m_camera.transform.rotation = Quaternion.Slerp(m_camera.transform.rotation, newRot, m_cameraSmooth * Time.deltaTime);
        }
    }

}
