using Unity.VisualScripting;
using UnityEngine;

public static class HPBasedCondition
{
    public static bool CurrentHPIsMoreThanXPercent(int maxHP, int currentHP, int percent)
    {
        float maxHPPart = maxHP / 100 * percent;
        return currentHP > maxHPPart;
    }
}