using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Project : MonoBehaviour
{
    void Elo()
    {
        print("ELO");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //private void OnDrawGizmos()
    //{
    //    Handles.BeginGUI();

    //    if (GUI.Button(new Rect(10, 10, 100, 50), "Button"))
    //    {
    //        print("Pressed");
    //        Debug.Log("Pressed");
    //    }

    //    Handles.EndGUI();

    //}

    protected void OnSceneGUI()
    {

    }

    private void OnGUI()
    {
        //if (GUI.Button(new Rect(10, 70, 50, 30), "Click"))
        //    Debug.Log("Clicked the button with text");
    }

}