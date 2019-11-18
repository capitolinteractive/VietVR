using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeController : MonoBehaviour
{
    public GameObject target;
    //public GameObject parentTarget;
    public static GlobeController Current { get; private set; }
    bool IsMoveLeft;
    public bool IsMoving;
    public bool moveCommand;

    public float pubPoint;
    public Vector3 pubVec;


    public float timeTakenDuringLerp = 1f;
    public float distanceToMove = 10;
    private bool _isLerping;
    private Quaternion _startPosition;
    private Quaternion _endPosition;
    private float _timeStartedLerping;

    public void StartLerping()
    {
        _isLerping = true;
        _timeStartedLerping = Time.time;

        //We set the start position to the current position, and the finish to 10 spaces in the 'forward' direction
        _startPosition = transform.rotation;
        _endPosition = Quaternion.LookRotation(new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z) - new Vector3(transform.position.x, target.transform.position.y, transform.position.z)); 
    }


    // Use this for initialization
    void Start()
    {
        Current = this;
    }

    
    /*
    // Update is called once per frame
    void Update()
    {

        // transform.Rotate(Vector3.up * Time.deltaTime *10);
        if (target != null)
        {

            


        }

    }//update
    */

    void FixedUpdate()
    {
        if (_isLerping)
        {
            float timeSinceStarted = Time.time - _timeStartedLerping;
            float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

            transform.rotation = Quaternion.Slerp(_startPosition, _endPosition, percentageComplete);

            if (percentageComplete >= 1.0f)
            {
                _isLerping = false;
            }
        }
    }
}

