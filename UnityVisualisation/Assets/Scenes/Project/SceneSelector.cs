using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    public void selectScene()
    {
        switch (this.gameObject.name)
        {
            case "Scene01Button":
                SceneManager.LoadScene("lab3.1");
                break;
            case "Scene02Button":
                SceneManager.LoadScene("lab3.2");
                break;
        }
    }
}