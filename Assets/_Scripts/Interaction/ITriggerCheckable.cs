using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCheckable
{
    bool IsAggroad { get; set; }
    bool IsWithinStrikingDistance { get; set; }
    void SetAggroStatus(bool isAggroad);
    void SetStrikingDistanceBool(bool isWithinStrikingDistance);
}
