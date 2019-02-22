using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

using System.Linq;


public class AI : MonoBehaviour
{

    private enum AIState { WANDERING, CHASING, DEAD };
    private AICharacterControl characterController;
    private GameObject[] allWaypoints;
    private int currentWaypoint = 0;
    private ThirdPersonCharacter tpCharacter;
    private AIState state = AIState.WANDERING;


    // Use this for initialization
    void Start()
    {
        //store the controllers in variables for easy access later
        characterController = GetComponent<AICharacterControl>();
        tpCharacter = GetComponent<ThirdPersonCharacter>();
        //store all the waypoints too
        allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        //shuffle array to make unique wandering path
        System.Random rnd = new System.Random(System.DateTime.Now.Millisecond);
        allWaypoints = allWaypoints.OrderBy(x => rnd.Next()).ToArray();

    }

    // Update is called once per frame
    void Update()
    {
        if (state == AIState.WANDERING)
        {
            characterController.SetTarget(allWaypoints[currentWaypoint].transform);
            //if i'm wandering...
            Debug.Log(currentWaypoint);
            if ((Vector3.Distance(characterController.target.transform.position, transform.position) < 2.0f))
            {
                //...make me target the next one
                currentWaypoint++;
                //make sure that we don't fall off the end of teh array but lop back round
                currentWaypoint %= allWaypoints.Length;
            }

        }
    }
}