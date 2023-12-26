using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager
{
    public Animation m_Animator;

    [Header("ControlKey")]
    public KeyCode m_Forward = KeyCode.W;
    public KeyCode m_Left = KeyCode.A;
    public KeyCode m_Right = KeyCode.D;
    public KeyCode m_Backward = KeyCode.S;
    

    float m_MovementSpeed = 1.0f;
    PlayerState m_State;
    Transform m_Zero, m_Rotated;

    public enum PlayerState
    {
        Idle,
        MoveForward,
        MoveLeft,
        MoveRight,
        MoveBackward
    }

    void MoveCharacter()
    {
        switch(m_State)//���� ����ϴ� �ִϸ��̼��� ������ �����̴� ����� �̻��ϰ� ���־ 90���� �����ִ� ó���� �ؾ���
        {
            case PlayerState.MoveForward:

            break;
            case PlayerState.MoveLeft:

            break;
            case PlayerState.MoveRight:

            break;
            case PlayerState.MoveBackward:

            break;
            default:
                m_State = PlayerState.Idle;
            break;
        }
    }


}
