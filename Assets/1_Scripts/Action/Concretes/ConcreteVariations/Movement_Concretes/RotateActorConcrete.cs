using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateActorConcrete : BaseConcrete
{
    private BaseActor _actorToRotate;

    public RotateActorConcrete(
    TurnProcessor turnProcessor,
    LevelMaster levelMaster, 
    BaseAction actionOfThisConcrete, 
    List<IConditionCommand> extraConditions, 
    ActionConcreteTag tag,
    BaseActor actorToRotate
    ): base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag)
    {
        _actorToRotate = actorToRotate;
    }

    public override IEnumerator ChildExecute()
    {
        Quaternion start = _actorToRotate.GraphicTransform.rotation;
        Quaternion target = _actorToRotate.IsFacingRight() ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
        float timePast = 0;
        while (timePast < 0.25)
        {
            float normalizedTime = timePast / 0.25f;
            _actorToRotate.GraphicTransform.rotation = Quaternion.Slerp(start, target, normalizedTime);
            timePast += Time.deltaTime;
            yield return null;
        }
        _actorToRotate.GraphicTransform.rotation = target;

        yield return GlobalLowLevelConcrete.Pause;
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new RotateActorConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ExtraConditions, Tag, _actorToRotate);
    }
}