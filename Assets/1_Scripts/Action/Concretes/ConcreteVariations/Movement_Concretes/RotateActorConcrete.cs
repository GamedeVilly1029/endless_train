using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateActorConcrete : BaseConcrete
{
    private IActor _actorToRotate;

    public RotateActorConcrete(
    TurnProcessor turnProcessor,
    LevelMaster levelMaster, 
    IAction actionOfThisConcrete, 
    List<IConditionCommand> extraConditions, 
    ActionConcreteTag tag,
    IActor actorToRotate
    ): base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag)
    {
        _actorToRotate = actorToRotate;
    }

    public override IEnumerator ChildExecute()
    {
        Quaternion start = _actorToRotate.TransformReference.rotation;
        Quaternion target = _actorToRotate.IsFacingRight() ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
        float timePast = 0;
        while (timePast < 0.25)
        {
            float normalizedTime = timePast / 0.25f;
            _actorToRotate.TransformReference.rotation = Quaternion.Slerp(start, target, normalizedTime);
            timePast += Time.deltaTime;
            yield return null;
        }
        _actorToRotate.TransformReference.rotation = target;

        yield return GlobalLowLevelConcrete.Pause;
    }
}