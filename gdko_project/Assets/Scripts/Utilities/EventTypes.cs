using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestWoodEvent
{
    public int amount = 0;

    public HarvestWoodEvent(int _amount)
    {
        amount = _amount;
    }
}

public class WoodTotalEvent
{
    public int amount = 0;

    public WoodTotalEvent(int _amount)
    {
        amount = _amount;
    }
}

public class HarvestStoneEvent
{
    public int amount = 0;

    public HarvestStoneEvent(int _amount)
    {
        amount = _amount;
    }
}

public class StoneTotalEvent
{
    public int amount = 0;

    public StoneTotalEvent(int _amount)
    {
        amount = _amount;
    }
}