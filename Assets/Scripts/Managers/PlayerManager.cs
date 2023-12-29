using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private ThirdCameraControl m_CameraControl;
    private CharacterControl m_CharacterControl;

    //Start is called before the first frame update
    void Start()
    {
        m_CameraControl = gameObject.GetComponent<ThirdCameraControl>();
        m_CameraControl.SetMainCamera();//씬 안의 메인 카메라를 플레이어에게 붙임
        m_CameraControl.CameraFollowTo(gameObject);

        m_CharacterControl = gameObject.GetComponent<CharacterControl>();
    }

    //Update is called once per frame
    void Update()
    {
        m_CameraControl.UpdateCamera();//마우스 움직임에 따라 카메라 3인칭 회전
        m_CharacterControl.UpdateCharacterMove(m_CameraControl.GetDirection());//방향키에 맞춰서 캐릭터 이동
    }
}
