using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeToScene1()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void ChangeToScene2()
    {
        SceneManager.LoadScene("Scene2");
    }

    public void ChangeToScene3()
    {
        SceneManager.LoadScene("Scene3");
    }

    // Add more methods for additional scenes as needed
}
