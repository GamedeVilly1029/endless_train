using System.Collections.Generic;
using UnityEngine;

public interface IActor
{
    Transform Transform{get;set;}
    List<Action> ActionRow{get; set;}
}