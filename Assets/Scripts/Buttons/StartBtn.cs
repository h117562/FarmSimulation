using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtn : MonoBehaviour
{
    public void Onclick()
    {
        SceneManager.LoadScene("GameScene");
        //Debug.Log("StartBtn Pressed");
    }
}
