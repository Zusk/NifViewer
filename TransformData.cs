using OpenTK.Mathematics;

/// <summary>
/// Represents a translation + rotation + scale transform for Ni nodes/geometry.
/// </summary>
public readonly struct TransformData
{
    public TransformData(Vector3 translation, Matrix3 rotation, float scale)
    {
        Translation = translation;
        Rotation = rotation;
        Scale = scale;
    }

    public Vector3 Translation { get; }
    public Matrix3 Rotation { get; }
    public float Scale { get; }

    public static TransformData Identity { get; } = new(Vector3.Zero, Matrix3.Identity, 1f);

    public static TransformData FromComponents(Vector3 translation, Matrix3 rotation, float scale) =>
        new(translation, rotation, scale);

    public static TransformData Combine(in TransformData parent, in TransformData local)
    {
        float combinedScale = parent.Scale * local.Scale;
        Matrix3 combinedRotation = Multiply(parent.Rotation, local.Rotation);
        Vector3 translatedChild = Multiply(parent.Rotation, local.Translation * parent.Scale);
        Vector3 combinedTranslation = translatedChild + parent.Translation;
        return new TransformData(combinedTranslation, combinedRotation, combinedScale);
    }

    public Vector3 TransformPosition(Vector3 localPosition)
    {
        Vector3 scaled = localPosition * Scale;
        Vector3 rotated = Multiply(Rotation, scaled);
        return rotated + Translation;
    }

    public Vector3 TransformNormal(Vector3 normal)
    {
        Vector3 rotated = Multiply(Rotation, normal);
        return rotated.LengthSquared > 0f ? Vector3.Normalize(rotated) : rotated;
    }

    private static Matrix3 Multiply(Matrix3 left, Matrix3 right)
    {
        return new Matrix3(
            left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31,
            left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32,
            left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33,
            left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31,
            left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32,
            left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33,
            left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31,
            left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32,
            left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33
        );
    }

    private static Vector3 Multiply(Matrix3 matrix, Vector3 vector)
    {
        return new Vector3(
            matrix.M11 * vector.X + matrix.M12 * vector.Y + matrix.M13 * vector.Z,
            matrix.M21 * vector.X + matrix.M22 * vector.Y + matrix.M23 * vector.Z,
            matrix.M31 * vector.X + matrix.M32 * vector.Y + matrix.M33 * vector.Z
        );
    }
}
