using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private ThirdCameraControl m_CameraControl;
    private CharacterControl m_CharacterControl;

    //Start is called before the first frame update
    void Start()
    {
        m_CameraControl = gameObject.GetComponent<ThirdCameraControl>();
        m_CameraControl.SetMainCamera();//�� ���� ���� ī�޶� �÷��̾�� ����
        m_CameraControl.CameraFollowTo(gameObject);

        m_CharacterControl = gameObject.GetComponent<CharacterControl>();
    }

    //Update is called once per frame
    void Update()
    {
        m_CameraControl.UpdateCamera();//���콺 �����ӿ� ���� ī�޶� 3��Ī ȸ��
        m_CharacterControl.UpdateCharacterMove(m_CameraControl.GetDirection());//����Ű�� ���缭 ĳ���� �̵�
    }
}
