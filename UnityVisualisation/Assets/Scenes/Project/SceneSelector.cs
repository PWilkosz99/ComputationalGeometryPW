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
                SceneManager.LoadScene("E1RotationOfTheRay");
                break;
            case "Scene02Button":
                SceneManager.LoadScene("E2PointOfLinesCrossing");
                break;
            case "Scene03Button":
                SceneManager.LoadScene("E3EquationFor2Points");
                break;
            case "Scene04Button":
                SceneManager.LoadScene("E4WhichSideofLine");
                break;
            case "Scene05Button":
                SceneManager.LoadScene("E5PointRelativeToTriangle");
                break;
            case "Scene06Button":
                SceneManager.LoadScene("E6PointRelativeToPolygon");
                break;
            case "Scene07Button":
                SceneManager.LoadScene("E7CircleWithLineCrossingPoints");
                break;
            case "Scene08Button":
                SceneManager.LoadScene("E8TriangleArea");
                break;
            case "Scene09Button":
                SceneManager.LoadScene("E9ConvexHull");
                break;
            case "Scene10Button":
                SceneManager.LoadScene("E10KDTree");
                break;
        }
    }
}