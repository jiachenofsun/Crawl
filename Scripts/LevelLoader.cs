using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    /* Loads the specified level */
    public void LoadLevel(int levelIndex)
    {
        StartCoroutine(Load(levelIndex));
    }

    /* Loads the next level according to the indexes in Build Settings */
    public void LoadNext()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }


    /* Co-Routine which plays the scene transition before changing levels. */
    IEnumerator Load(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
