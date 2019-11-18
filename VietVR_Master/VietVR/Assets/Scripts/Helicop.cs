using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicop : MonoBehaviour {
    Rigidbody rb;
    public bool up;
    public float t;
    public float moveDur;
    Vector3 startp, endp, initStart,initEnd;



    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        up = true;
        initStart = transform.position;
        initEnd = transform.position + new Vector3(0, 3.5f, 0);
        startp = initStart;
        endp = initEnd;
        moveDur = 3f;
    }
	
	// Update is called once per frame
	void Update () {

        /*
        if (t < moveDur)
        {
            t = Mathf.Min(moveDur, t + Time.deltaTime);
            float t01 = t / moveDur;
            float lerpVal = 1 - (1 - t01) * (1 - t01);
            transform.position = Vector3.Lerp(startp, endp, lerpVal);

        }
        */
        if (transform.position.y < endp.y && up)
        {
            rb.AddForce(Vector3.up * Time.deltaTime * 180f);
        }
        else if (transform.position.y > endp.y && !up)
        {
            rb.AddForce(Vector3.down * Time.deltaTime * 180f);
        }

        else
        {
            up = !up;
        }

        /*
        {
            if (up)
            {
                up = false;
                t = 0;
                startp = transform.position;
                endp = transform.position + new Vector3(0, -3f, 0);
            }
            else if (!up)
            {
                up = true;
                t = 0;
                startp = transform.position;
                endp = transform.position + new Vector3(0, 3f, 0);
            }

        }
        */

    }
}
