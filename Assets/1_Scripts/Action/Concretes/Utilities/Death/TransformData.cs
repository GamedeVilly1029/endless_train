using UnityEngine;

public struct TransformData
{
    public Vector3 Position;
    public Quaternion Rotation;

    public TransformData(Vector3 position, Quaternion rotation)
    {
        Position = position;
        Rotation = rotation;
    }

    public static TransformData Lerp(TransformData start, TransformData target, float t)
    {
        return new TransformData
        (
            Vector3.Lerp(start.Position, target.Position, t),
            Quaternion.Lerp(start.Rotation, target.Rotation, t)
        );
    }
}