using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvesterMinion : MinionController
{
    enum HarvesterType { 
        Lumberjack,
        Miner
    }

    [SerializeField]
    Transform dropoff_location = null;

    [SerializeField]
    Transform harvest_object = null;

    [SerializeField]
    float harvest_time = 5;

    [SerializeField]
    float deposit_time = 1;

    [SerializeField]
    HarvesterType harvester_type = HarvesterType.Lumberjack;

    // Start is called before the first frame update
    new void Start()
    {
        my_task_state = TaskState.Returning;

        SetTargetPosition(dropoff_location.position);

        StartCoroutine(WorkLogic());
    }

    protected IEnumerator WorkLogic() { 
        while(true) {
            if(my_state == MinionState.ReachedTargetPosition) {
                if(my_task_state == TaskState.Harvesting) {
                    yield return HarvestResource();

                }
                else if(my_task_state == TaskState.Returning) {
                    yield return ReturnResource();
                }
            }

            if(my_state == MinionState.MovingTowardsPosition) {
                ProcessMovement();
            }
            
            yield return null;
        }
    }

    IEnumerator HarvestResource() {
        yield return new WaitForSeconds(harvest_time);

        SetTargetPosition(dropoff_location.position);
        my_task_state = TaskState.Returning;
    }

    IEnumerator ReturnResource() { 
        yield return new WaitForSeconds(deposit_time);

        if(harvester_type == HarvesterType.Lumberjack) { 
            EventBus.Publish(new HarvestWoodEvent(1));
        }
        else if(harvester_type == HarvesterType.Miner) { 
            EventBus.Publish(new HarvestStoneEvent(1));
        }

        SetTargetPosition(harvest_object.position);
        my_task_state = TaskState.Harvesting;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
