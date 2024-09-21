using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillInvoke : MonoBehaviour
{
    private Scene scene; // for use current scene id
    private int timeout = 2; // seconds before restart

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke("restartGame", timeout); // cull function after timeout
    }

    private void restartGame()
    {
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }
}
