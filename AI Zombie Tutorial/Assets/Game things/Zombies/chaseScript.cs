using UnityEngine;
using System.Collections;

public class chaseScript : MonoBehaviour
{

    public Transform player;
    static Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

        void Update()
        {
            Vector3 direction = player.position - this.transform.position;
            float angle = Vector3.Angle(direction, this.transform.forward);
            if (Vector3.Distance(player.position, this.transform.position) < 10 && angle < 30)
            {

                direction.y = 0;

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                            Quaternion.LookRotation(direction), 0.1f);

                if (direction.magnitude > 5)
            {
                this.transform.Translate(0, 0, 0.5f);
            }
        }

    }
}
