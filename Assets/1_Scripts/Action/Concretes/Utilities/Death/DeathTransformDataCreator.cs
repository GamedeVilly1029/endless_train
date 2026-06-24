using UnityEngine;
using System.Collections;

public static class DeathTransformDataCreator
{
    public static TransformData CreateTransform(BaseActor actor)
    {
        if (actor.IsFacingRight())
        {
            return RotationBasedBrancher(1, actor);
        }
        else
        {
            return RotationBasedBrancher(-1, actor);
        }
    }

    private static TransformData RotationBasedBrancher(float zRotInvert, BaseActor actor)
    {
        TransformData data = new TransformData(actor.GraphicTransform.localPosition, actor.GraphicTransform.localRotation);

        data.Rotation = Quaternion.Euler(new Vector3(data.Rotation.eulerAngles.x, data.Rotation.eulerAngles.y, 90));

        data.Position.x += 0.2f * zRotInvert;
        data.Position.y -= 0.2f;

        return data;
    }
}