using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotConcrete : ValueConcrete
{
    private BaseActor _caster;
    private BaseActor _target;
    private float _throwDuration;
    private float _fallDuration;
    private float _fadeDuration;
    public SlingshotConcrete
    (
        TurnProcessor turnProcessor,
        LevelMaster levelMaster,
        BaseAction actionOfThisConcrete,
        List<IConditionCommand> extraConditions,
        ActionConcreteTag tag,
        int value,
        BaseActor caster,
        float throwDuration,
        float fallDuration,
        float fadeDuration
    ) : base(turnProcessor, levelMaster, actionOfThisConcrete, extraConditions, tag, value)
    {
        _caster = caster;
        _throwDuration = throwDuration;
        _fallDuration = fallDuration;
        _fadeDuration = fadeDuration;
    }

    public override IEnumerator ChildExecute()
    {
        GameObject rock = Object.Instantiate(Resources.Load<GameObject>("Prefab/Rock"), _caster.TransformReference.position, Quaternion.identity);

        _target = GlobalLowLevelConcrete.TryReturnFirstActorOnCellsAhead(TurnProcessorInst, LevelMasterInst, _caster);
        if (new ActorIsNotNullCondition(TurnProcessorInst, LevelMasterInst, _target).Execute())
        {
            yield return new BeforeHitSwing(_caster, 0.5f).Execute();
            yield return new ThrowTransformObjectConcrete(rock.transform, _target.TransformReference.position, _throwDuration).Execute();

            yield return _target.TakeBluntDamage(Value);

            Object.FindAnyObjectByType<AudioMaster>().PlaySound("hit");
            _caster.StartCoroutine(new TransformFallConcrete(rock.transform, _fallDuration).Execute());
            SpriteRenderer rend = rock.GetComponent<SpriteRenderer>();
            _caster.StartCoroutine(new TransparencyLerpGraphicConcrete(0, rend, _fadeDuration).Execute());
        }
        else
        {
            if (_caster.IsFacingRight())
            {
                Vector2 targetPos = new(LevelMasterInst.Cells[^1].CellPosition.x + 3, LevelMasterInst.Cells[^1].CellPosition.y);

                yield return new BeforeHitSwing(_caster, 0.5f).Execute();
                yield return new ThrowTransformObjectConcrete(rock.transform, targetPos, _throwDuration).Execute();

                _caster.StartCoroutine(new TransformFallConcrete(rock.transform, _fallDuration).Execute());
                SpriteRenderer rend = rock.GetComponent<SpriteRenderer>();
                _caster.StartCoroutine(new TransparencyLerpGraphicConcrete(0, rend, _fadeDuration).Execute());
            }
            else
            {
                Vector2 targetPos = new(LevelMasterInst.Cells[0].CellPosition.x - 3, LevelMasterInst.Cells[0].CellPosition.y);
                yield return new BeforeHitSwing(_caster, 0.5f).Execute();
                yield return new ThrowTransformObjectConcrete(rock.transform, targetPos, _throwDuration).Execute();

                _caster.StartCoroutine(new TransformFallConcrete(rock.transform, _fallDuration).Execute());
                SpriteRenderer rend = rock.GetComponent<SpriteRenderer>();
                _caster.StartCoroutine(new TransparencyLerpGraphicConcrete(0, rend, _fadeDuration).Execute());
            }
        }
    }

    public override IConcrete Clone(BaseAction clonedAction)
    {
        return new SlingshotConcrete(TurnProcessorInst, LevelMasterInst, clonedAction, ActionPassedConditions, Tag, Value, _caster, _throwDuration, _fallDuration, _fadeDuration);
    }
}