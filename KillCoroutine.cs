using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillCoroutine : MonoBehaviour
{
    private Scene scene; // for use current scene id
    private int timeout = 2; // seconds before restart

    public int sceneId; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // call functions with parameter's
        StartCoroutine( restartGame(timeout, sceneId) );
    }

    IEnumerator restartGame(int time, int id)
    {
        yield return new WaitForSeconds(time);  
        SceneManager.LoadScene(id);
    }
}
