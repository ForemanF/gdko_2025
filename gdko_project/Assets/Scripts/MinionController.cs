using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MinionState
{ 
    Idle,
    Wander,
    MovingTowardsPosition,
    ReachedTargetPosition,
    Undefined
}

public class MinionController : MonoBehaviour
{
    [SerializeField]
    float movement_speed = 1f;

    [SerializeField]
    float wander_range = 1f;

    [SerializeField]
    float rand_wait_time_min = 0.5f;

    [SerializeField]
    float rand_wait_time_max = 2f;

    protected MinionState my_state = MinionState.Idle;
    protected TaskState my_task_state = TaskState.Undefined;
    Vector2 target_location;

    protected enum TaskState { 
        Harvesting,
        Returning,
        Undefined
    }

    // Start is called before the first frame update
    protected void Start()
    {
        StartCoroutine(MinionWander());
    }

    // Update is called once per frame
    IEnumerator MinionWander()
    {
        while (true)
        {
            if (my_state == MinionState.Idle)
            {
                yield return ProcessIdle();
            }
            else if (my_state == MinionState.Wander)
            {
                ProcessMovement();
            }

            yield return null;
        }
    }


    IEnumerator ProcessIdle() {
        float rand = Random.Range(0f, 1f);
        if(rand < 0.4f) {
            target_location = transform.position + new Vector3(Random.Range(-wander_range, wander_range), Random.Range(-wander_range, wander_range));
            my_state = MinionState.Wander;
        }
        else {
            yield return new WaitForSeconds(Random.Range(rand_wait_time_min, rand_wait_time_max));
        }
    }

    protected void SetTargetPosition(Vector3 new_target, MinionState new_minion_state = MinionState.MovingTowardsPosition) {
        target_location = new_target;
        my_state = new_minion_state;
    }

    protected void ProcessMovement() {
        // Figure out the direction that needs to be made
        Vector2 diff = target_location - (Vector2)transform.position;
        if(diff.magnitude < 0.1) {
            if(my_state == MinionState.Wander) { 
                my_state = MinionState.Idle;
            }
            else { 
                my_state = MinionState.ReachedTargetPosition;
            }

            return;
        }

        diff.Normalize();
        Vector2 movement = diff * movement_speed * Time.deltaTime;
        transform.position += (Vector3)movement;
    }
}
