using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIResourceManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI wood_text;

    [SerializeField]
    TextMeshProUGUI stone_text;


    Subscription<WoodTotalEvent> wood_sub;
    Subscription<StoneTotalEvent> stone_sub;

    // Start is called before the first frame update
    void Start()
    {
        wood_sub = EventBus.Subscribe<WoodTotalEvent>(_OnWoodTotal);
        stone_sub = EventBus.Subscribe<StoneTotalEvent>(_OnStoneTotal);
        
    }

    void _OnWoodTotal(WoodTotalEvent e) {
        wood_text.text = "Wood: " + e.amount;
    }

    void _OnStoneTotal(StoneTotalEvent e) {
        stone_text.text = "Stone: " + e.amount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
