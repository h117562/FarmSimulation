using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [Header("Animator")]
    public Animator m_animator;//�÷��̾� ĳ���� �ִϸ��̼�

    [Header("Rigidbody")]
    public Rigidbody m_rigidbody;//�÷��̾� ĳ���� Rigidbody

    [Header("ControlKey")]
    public KeyCode m_forward = KeyCode.W;
    public KeyCode m_left = KeyCode.A;
    public KeyCode m_right = KeyCode.D;
    public KeyCode m_backward = KeyCode.S;

    [HideInInspector]
    Vector3 m_direction = Vector3.zero;//�̵�����
    float m_movementSpeed = 2.0f;//�̵� �ӵ�(��)
    float m_speed = 0.0f;//���� �ӵ�
    float m_rotateSpeed = 10.0f;//ĳ���� ȸ���ӵ�
    PlayerState m_state = PlayerState.Idle;//���� �÷��̾� ����
    PlayerState m_before = PlayerState.Idle;//���� �÷��̾� ����
    bool m_freeze = false;//����Ű ���

    string m_walkTrigger = "Walk";
    string m_IdleTrigger = "Idle";



    public void UpdateCharacterMove(float cameraAngle)//ĳ���� �̵� ������Ʈ
    {
        if (!m_freeze)
        {
            MoveCharacter(cameraAngle);
            UpdateTrigger();
        }
    }

    //����Ű�� ������Ű�� �Լ�
    public void ToggleFreeze()
    {
        m_freeze = !m_freeze;
    }

    void MoveCharacter(float cameraAngle)//����Ű�� ���缭 �ش� �������� �̵�
    {
        bool keyA, keyD, keyW, keyS;
        m_direction = Vector3.zero;//���� �ʱ�ȭ

        keyA = Input.GetKey(m_left);
        keyD = Input.GetKey(m_right);
        keyW = Input.GetKey(m_forward);
        keyS = Input.GetKey(m_backward);

        if (keyA && keyD)//�¿� ���� �Է� ��ȿ ó��
        {
            keyA = false;
            keyD = false;
        }

        if (keyW && keyS)//�յ� ���� �Է� ��ȿ ó��
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

        m_direction = m_direction.normalized;
        m_direction = Quaternion.Euler(new Vector3(0.0f, cameraAngle, 0.0f)) * m_direction;

        if (keyA || keyD || keyS || keyW)//�̵�����Ű�� ������ ���
        {
            m_state = PlayerState.Walk;
        }
        else//�ƹ� �ൿ ���� ��
        {
            m_state = PlayerState.Idle;
        }

        Quaternion look = Quaternion.identity;

        if (m_direction.magnitude != 0)
        {
            look = Quaternion.LookRotation(new Vector3(m_direction.x, 0, m_direction.z));
        }else
        {
            m_direction = transform.forward;
        }
        
        m_speed = Mathf.Lerp(m_speed, (m_state != PlayerState.Walk ? 0.0f : 1.0f), 10 * Time.deltaTime);
        this.transform.rotation = Quaternion.Lerp(transform.rotation, look, (m_state != PlayerState.Walk ? 0 : m_rotateSpeed * Time.deltaTime));
        m_direction *= m_speed;
        m_direction.y = m_rigidbody.velocity.y;//y�� ����� �״��
        
        m_rigidbody.velocity = m_direction * m_movementSpeed;
    }

    //�ִϸ��̼��� Ʈ���Ÿ� ó���ϴ� �Լ�
    void UpdateTrigger()
    {
        if (m_state != m_before)//���� ���¿��� ��ȭ�� �־��ٸ� Trigger ����
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
       
        m_before = m_state;//���� ���� ������Ʈ
    }


    public enum PlayerState
    {
        Idle,
        Walk,
    }
}
