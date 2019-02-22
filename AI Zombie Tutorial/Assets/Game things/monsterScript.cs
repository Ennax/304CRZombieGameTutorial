using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class monsterScript : MonoBehaviour
{

    public GameObject player;
    public AudioClip[] footsounds;
    public Transform eyes;
    public AudioSource growl;

    private NavMeshAgent nav;
    private AudioSource sound;
    private Animator anim;
    private string state = "idle";
    private bool alive = true;
    private float wait = 0f;
    private bool highAlert = false;
    private float alertness = 20f;

    // Use this for initialization
    void Start()
    {

        nav = GetComponent<NavMeshAgent>();
        sound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        nav.speed = 0.6f;
        anim.speed = 1.6f;

    }

    public void footstep(int _num)

    { //2 sounds
        sound.clip = footsounds[_num];
        sound.Play();
    }

    //checking to see if the monster sees the player

    public void checkSight()
    {
        if(alive)
        {
            RaycastHit rayHit;
            if(Physics.Linecast(eyes.position, player.transform.position, out rayHit))
            {
                //print("hit" + rayHit.collider.gameObject.name);
                if(rayHit.collider.gameObject.name == "player")
                {
                    if(state != "kill")
                        {
                        state = "chase";
                        nav.speed = 1.8f;
                        anim.speed = 3f;
                        growl.pitch = 1.2f;
                        growl.Play();
                    }
                }
     
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(eyes.position, player.transform.position, Color.blue);
        if (alive)
        {
            anim.SetFloat("velocity", nav.velocity.magnitude);
            //Idle state

            if (state == "idle")
            {
                //goes to a random place to walk to
                Vector3 randomPos = Random.insideUnitSphere * alertness;
                NavMeshHit navHit;
                NavMesh.SamplePosition(transform.position + randomPos, out navHit, 20f, NavMesh.AllAreas);

                if(highAlert)
                {
                    NavMesh.SamplePosition(player.transform.position + randomPos, out navHit, 20f, NavMesh.AllAreas);
                    //Each time, lose awarness of player's existance

                    alertness += 5f;

                    if(alertness > 20f)
                    {
                        highAlert = false;
                        nav.speed = 0.6f;
                        anim.speed = 1.6f;
                    }
                }


                nav.SetDestination(navHit.position);
                state = "walk";
            }

            //Walking state
            if (state == "walk")

            {

                if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
                {
                    state = "search";
                    wait = 4f;
                }

            }

            //Searching State

            if(state == "search")
            {
                if(wait > 0f)
                {
                    wait -= Time.deltaTime;
                    transform.Rotate(0f, 120f * Time.deltaTime, 0f);
                }
                else
                {
                    state = "idle";
                }
            }
            //Chasing state

        if(state == "chase")
            {
                nav.destination = player.transform.position;

                //Losing sight of player

                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance > 10f)
                {
                    state = "hunt";

                }

            }
            //Hunt state

            if (state == "hunt")
            {
                if(nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
                {
                    state = "search";
                    wait = 3f;
                    highAlert = true;
                    alertness = 5f;
                    checkSight();

                }
            }
            //nav.SetDestination(player.transform.position);
        }
    }
}