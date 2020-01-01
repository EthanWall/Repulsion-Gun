using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTeleporter : MonoBehaviour
{

    public string levelToLoad;

    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadSceneAsync(levelToLoad);
    }
}
