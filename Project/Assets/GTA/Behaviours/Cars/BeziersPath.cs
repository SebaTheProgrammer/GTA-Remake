using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using PathCreation;

public class BeziersPath : MonoBehaviour
{
    [SerializeField]
   // private PathCreator pathCreator;

   // [SerializeField]
    private float speed = 5;
    float distanceTravelled;

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
       // transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);

       // transform.LookAt(pathCreator.path.GetPointAtDistance(distanceTravelled+1));
    }
}
