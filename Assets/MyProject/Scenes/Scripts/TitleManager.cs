using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    void Update()
    {

        if (Input.GetKeyDown((KeyCode.N))
)
        {
            //Main_Proto‚ğŒÄ‚Ño‚·ˆ—
            SceneManager.LoadScene("Main_Proto");
        }
    }
}
