using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointEvents : MonoBehaviour
{
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}