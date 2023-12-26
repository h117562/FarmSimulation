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
        switch(m_State)//현재 사용하는 애니메이션중 옆으로 움직이는 모션이 이상하게 돼있어서 90도로 꺾어주는 처리를 해야함
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
