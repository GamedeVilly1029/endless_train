using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunFirstPlayerActionNextTurnConcrete : BaseConcrete
{
    public StunFirstPlayerActionNextTurnConcrete(
    TurnProcessor turnProcessor,
    LevelMaster levelMaster,
    IAction actionOfThisConcrete,
    List<IConditionCommand> extraConditions,
    ActionConcreteTag tag
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag)
    {
    }

    public override IEnumerator ChildExecute()
    {
        IStatusEffect stunFirstAction = new StunFirstActionEffect();
        stunFirstAction.ChildInitializeStatusEffect(LevelMasterInst.Player);
        LevelMasterInst.Player.StatusEffectsBeforeTurn.Add(stunFirstAction);

        yield return GlobalLowLevelConcrete.Pause;
    }
}