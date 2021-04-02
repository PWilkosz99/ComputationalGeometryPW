using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CricleCode : MonoBehaviour
{
    SpriteRenderer obj;
    public Rigidbody2D rigidbody2d;

    public LineRenderer line  ;


    // Start is called before the first frame update
    void Start()
    {

      //  line = GetComponent<LineRenderer>();
        obj = GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();

//        Debug.LogWarning(obj);


     //   var go = new GameObject("linia ma");
        //line = go.AddComponent<LineRenderer>();
        //line.
        //line.SetWidth(.2f, .2f);
        //line.SetPosition(0, Vector3.zero);
        //line.SetPosition(1, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("up"))
        {
          //  rigidbody2d.AddForce(Vector2.up * 500);
            gameObject.transform.Translate(0, 0.5f, 0, Space.World);
        }

        if (Input.GetKeyDown("down"))
        {
            //rigidbody2d.AddForce(Vector2.down * 500);
            gameObject.transform.Translate(0, -0.5f, 0, Space.World);

        }

        if (Input.GetKeyDown("left"))
        {
            //rigidbody2d.AddForce(Vector2.down * 500);
            gameObject.transform.Translate(-0.5f, 0, 0, Space.World);
        }

        if (Input.GetKeyDown("right"))
        {
            //rigidbody2d.AddForce(Vector2.down * 500);
            gameObject.transform.Translate(0.5f, 0, 0, Space.World);
        }

        if (Input.GetKeyDown("space"))
        {
            var a = gameObject.transform.position;
            a.y = 0;
            gameObject.transform.position = a;
        }



        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                hit.collider.attachedRigidbody.AddForce(Vector2.up);
            }
        }

        //if(line.GetPosition(1) != gameObject.transform.position)
        //{
        //    line.SetPosition(1, gameObject.transform.position);
        //}

    }

}

