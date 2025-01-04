using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    int wood = 0;

    [SerializeField]
    int stone = 0;

    Subscription<HarvestWoodEvent> wood_sub;
    Subscription<HarvestStoneEvent> stone_sub;

    // Start is called before the first frame update
    void Start()
    {
        wood_sub = EventBus.Subscribe<HarvestWoodEvent>(_OnWoodHarvest);
        stone_sub = EventBus.Subscribe<HarvestStoneEvent>(_OnStoneHarvest);
    }

    void _OnWoodHarvest(HarvestWoodEvent e) {
        wood += e.amount;

        EventBus.Publish(new WoodTotalEvent(wood));
    }

    void _OnStoneHarvest(HarvestStoneEvent e) {
        stone += e.amount;

        EventBus.Publish(new StoneTotalEvent(stone));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
