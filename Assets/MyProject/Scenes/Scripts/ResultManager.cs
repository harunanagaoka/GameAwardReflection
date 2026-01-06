using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.N))
        {
            //Title‚ğŒÄ‚Ño‚·ˆ—
            SceneManager.LoadScene("Title");
        }
    }
}
