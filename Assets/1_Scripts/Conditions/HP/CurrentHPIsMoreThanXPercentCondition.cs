using UnityEngine;

public class CurrentHPIsMoreThanXPercentCondition : BaseConditionCommand
{
    private float _maxHP;
    private float _currentHP;
    private float _percent;

    public CurrentHPIsMoreThanXPercentCondition(TurnProcessor turnProcessor, LevelMaster levelMaster, float maxHP, float currentHP, float percent)
        : base(turnProcessor, levelMaster)
    {
        _maxHP = maxHP;
        _currentHP = currentHP;
        _percent = percent;
    }

    public override bool Execute()
    {
        float maxHPPart = _maxHP / 100 * _percent;
        return _currentHP > maxHPPart;
    }
}