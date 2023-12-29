using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [Header("Animator")]
    public Animator m_animator;//플레이어 캐릭터 애니메이션

    [Header("Rigidbody")]
    public Rigidbody m_rigidbody;//플레이어 캐릭터 Rigidbody

    [Header("ControlKey")]
    public KeyCode m_forward = KeyCode.W;
    public KeyCode m_left = KeyCode.A;
    public KeyCode m_right = KeyCode.D;
    public KeyCode m_backward = KeyCode.S;

    Vector3 m_direction = new Vector3(0, 0, 1);//현재 이동방향
    float m_movementSpeed = 4.0f;//이동속도
    float m_rotateSpeed = 10.0f;//회전속도
    PlayerState m_state = PlayerState.Idle;//현재 플레이어 상태
    PlayerState m_before = PlayerState.Idle;//이전 플레이어 상태
    bool m_freeze = false;//방향키 잠금

    string m_walkTrigger = "Walk";
    string m_IdleTrigger = "Idle";



    public void UpdateCharacterMove(float cameraAngle)//캐릭터 이동 업데이트
    {
        if (!m_freeze)
        {
            MoveCharacter(cameraAngle);
            UpdateTrigger();
        }
    }

    //방향키를 고정시키는 함수
    public void ToggleFreeze()
    {
        m_freeze = !m_freeze;
    }

    void MoveCharacter(float cameraAngle)//방향키에 맞춰서 해당 방향으로 이동
    {
        bool keyA, keyD, keyW, keyS;

        m_direction = Vector3.zero;//방향 초기화

        keyA = Input.GetKey(m_left);
        keyD = Input.GetKey(m_right);
        keyW = Input.GetKey(m_forward);
        keyS = Input.GetKey(m_backward);

        if (keyA && keyD)//좌우 동시 입력 무효 처리
        {
            keyA = false;
            keyD = false;
        }

        if (keyW && keyS)//앞뒤 동시 입력 무효 처리
        {
            keyW = false;
            keyS = false;
        }

        if (keyA)
        {
            m_direction.x = -1;
        }

        if (keyD)
        {
            m_direction.x = 1;
        }

        if (keyW)
        {
            m_direction.z = 1;
        }

        if (keyS)
        {
            m_direction.z = -1;
        }

        m_direction = Quaternion.Euler(new Vector3(0.0f, cameraAngle, 0.0f)) * m_direction;

        if (keyA || keyD || keyS || keyW)//이동방향키를 눌렀을 경우
        {
            this.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(m_direction.x, 0, m_direction.z)), Time.deltaTime * m_rotateSpeed);
            m_state = PlayerState.Walk;
        }
        else//아무 행동 없을 때
        {
            m_state = PlayerState.Idle;
        }

        
        Vector3 targetVelocity = Vector3.Lerp(m_direction, m_direction + this.transform.position, (m_state == PlayerState.Walk ? 0 : m_movementSpeed * Time.deltaTime));
        targetVelocity.y = m_rigidbody.velocity.y;//y축 운동량은 그대로
        m_rigidbody.velocity = targetVelocity;
    }

    //애니메이션의 트리거를 처리하는 함수
    void UpdateTrigger()
    {
        if (m_state != m_before)//이전 상태에서 변화가 있었다면 Trigger 변경
        {
            switch (m_state)
            {
                case PlayerState.Walk:
                    m_animator.SetTrigger(m_walkTrigger);
                    break;
                default:
                    m_animator.SetTrigger(m_IdleTrigger);
                    break;
            }
        }

        m_before = m_state;//이전 상태 업데이트
    }

    public enum PlayerState
    {
        Idle,
        Walk,
    }
}
