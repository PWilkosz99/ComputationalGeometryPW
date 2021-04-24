using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBackAction : MonoBehaviour
{
    public void Back()
    {
                SceneManager.LoadScene("ProjectGUI");
    }
}
