using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    void Update()
    {

        if (Input.GetKeyDown((KeyCode.T)))
        {
            SceneManager.LoadScene("Tutorial");
        }

        if (Input.GetKeyDown((KeyCode.N)))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
