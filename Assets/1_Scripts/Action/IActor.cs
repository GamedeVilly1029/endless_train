using System.Collections.Generic;
using UnityEngine;

public interface IActor
{
    Transform Transform {get;set;}
    int PositionCellIndex{get;set;}
    List<Action> ActionRow {get; set;}
    RectTransform ActionRowPanel{get;set;}
    int HP {get;set;}
    bool IsFacingRight{get;set;}

    void TryToDie(int HP);
}