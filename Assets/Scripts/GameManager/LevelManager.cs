using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int currentLevel;
    public int nextLevel;
    Animator anim;
    public Text text;
    public EnemyManager lvl;
    public int i;

    void Start ()
    {
        anim = GetComponent<Animator>();

        text.text = "Level " + (i);
        anim.Play("Level");
        Debug.Log("Level " + currentLevel + " Begin");
    }



    public void loadNextLevel()
    {
        if (lvl.LevelNumber == 1)
        {
            SceneManager.LoadScene("Level2");
        }
        if (lvl.LevelNumber == 2)
        {
            SceneManager.LoadScene("Level3");
        }
        if (lvl.LevelNumber == 3)
        {
            SceneManager.LoadScene("YouWin");
        }
    }
}


