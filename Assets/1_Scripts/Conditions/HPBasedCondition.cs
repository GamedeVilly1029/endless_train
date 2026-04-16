using Unity.VisualScripting;
using UnityEngine;

public static class HPBasedCondition
{
    public static bool CurrentHPIsMoreThanXPercent(float maxHP, float currentHP, float percent)
    {
        float maxHPPart = maxHP / 100 * percent;

        return currentHP > maxHPPart;
    }
}