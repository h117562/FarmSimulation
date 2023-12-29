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

    public Vector2 GetDirection()
    {


        return Vector2.zero;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        do
        {
            if (angle < -360)
                angle += 360;
            if (angle > 360)
                angle -= 360;
        } while (angle < -360 || angle > 360);

        return Mathf.Clamp(angle, min, max);
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
            m_axis.x = ClampAngle(m_axis.x, -360, 360);
            m_axis.y = ClampAngle(m_axis.y, 0, 70);

            Quaternion newRot = Quaternion.Euler(m_axis.y, m_axis.x, 0);
            m_camera.transform.rotation = Quaternion.Slerp(m_camera.transform.rotation, newRot, m_cameraSmooth * Time.deltaTime);
        }
    }

}
