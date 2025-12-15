using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenTK.Mathematics;

/// <summary>
/// Parses Gamebryo keyframe files (.kf) and exposes the sequences as animation tracks.
/// </summary>
public sealed class AnimationSequenceLoader
{
    private const uint Version_10_1_0_103 = (10u << 24) | (1u << 16) | (0u << 8) | 103u;
    private const uint Version_10_1_0_106 = (10u << 24) | (1u << 16) | (0u << 8) | 106u;
    private const uint Version_10_1_0_110 = (10u << 24) | (1u << 16) | (0u << 8) | 110u;
    private const uint Version_10_1_0_113 = (10u << 24) | (1u << 16) | (0u << 8) | 113u;
    private const uint Version_10_2_0_0 = (10u << 24) | (2u << 16) | (0u << 8) | 0u;
    private const uint Version_10_4_0_1 = (10u << 24) | (4u << 16) | (0u << 8) | 1u;
    private const uint Version_20_1_0_0 = (20u << 24) | (1u << 16) | (0u << 8) | 0u;
    private const uint Version_20_5_0_0 = (20u << 24) | (5u << 16) | (0u << 8) | 0u;

    public IReadOnlyList<AnimationSequence> LoadSequences(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException("Animation file not found.", path);

        var context = ParseFile(path);
        var sequences = new List<AnimationSequence>();
        for (int i = 0; i < context.Blocks.Length; i++)
        {
            if (context.Blocks[i] is NiControllerSequenceBlock sequence)
            {
                var animation = BuildAnimationSequence(sequence, context);
                if (animation.Tracks.Count > 0)
                    sequences.Add(animation);
            }
        }

        Console.WriteLine($"[INFO] Parsed {sequences.Count} sequence(s) from \"{Path.GetFileName(path)}\".");
        return sequences;
    }

    private KfContext ParseFile(string path)
    {
        using var stream = File.OpenRead(path);
        using var reader = new BinaryReader(stream);

        var context = new KfContext
        {
            HeaderString = ReadHeaderString(reader),
            Version = reader.ReadUInt32(),
            EndianType = reader.ReadByte(),
            UserVersion = reader.ReadUInt32()
        };

        if (context.EndianType != 1)
            throw new NotSupportedException("Only little endian KF files are supported.");

        context.NumBlocks = reader.ReadUInt32();
        ushort blockTypes = reader.ReadUInt16();
        context.BlockTypes = new string[blockTypes];
        for (int i = 0; i < blockTypes; i++)
            context.BlockTypes[i] = ReadSizedString(reader);

        int blockCount = checked((int)context.NumBlocks);
        context.BlockTypeIndex = new int[blockCount];
        for (int i = 0; i < blockCount; i++)
            context.BlockTypeIndex[i] = reader.ReadUInt16();

        reader.ReadUInt32(); // Unknown placeholder before block data

        context.Blocks = new object[blockCount];
        for (int blockIndex = 0; blockIndex < blockCount; blockIndex++)
        {
            int typeIndex = context.BlockTypeIndex[blockIndex];
            if (typeIndex < 0 || typeIndex >= context.BlockTypes.Length)
                throw new InvalidDataException($"Invalid block type index {typeIndex} for block {blockIndex}");

            string typeName = context.BlockTypes[typeIndex];
            context.Blocks[blockIndex] = ParseBlock(typeName, reader, context);
        }

        return context;
    }

    private object ParseBlock(string typeName, BinaryReader reader, KfContext context)
    {
        return typeName switch
        {
            "NiControllerSequence" => ParseNiControllerSequence(reader, context),
            "NiTransformInterpolator" => ParseNiTransformInterpolator(reader),
            "NiTransformData" => ParseNiTransformData(reader),
            "NiStringPalette" => ParseNiStringPalette(reader),
            "NiFloatInterpolator" => ParseNiFloatInterpolator(reader),
            "NiFloatData" => ParseNiFloatData(reader),
            "NiTextKeyExtraData" => ParseNiTextKeyExtraData(reader),
            _ => throw new NotSupportedException($"Unsupported KF block type: {typeName}")
        };
    }

    private NiControllerSequenceBlock ParseNiControllerSequence(BinaryReader reader, KfContext context)
    {
        var sequence = new NiControllerSequenceBlock { Name = ReadSizedString(reader) };

        uint controlledCount = reader.ReadUInt32();
        if (context.Version >= Version_10_1_0_106)
            reader.ReadUInt32(); // Array grow by

        for (uint i = 0; i < controlledCount; i++)
            sequence.ControlledBlocks.Add(ParseControlledBlock(reader, context));

        if (context.Version <= Version_10_1_0_103)
            return sequence;

        sequence.Weight = reader.ReadSingle();
        sequence.TextKeysRef = reader.ReadInt32();
        sequence.CycleType = reader.ReadUInt32();
        sequence.Frequency = reader.ReadSingle();

        if (context.Version <= Version_10_4_0_1)
            sequence.Phase = reader.ReadSingle();

        sequence.StartTime = reader.ReadSingle();
        sequence.StopTime = reader.ReadSingle();

        if (context.Version == Version_10_1_0_106)
            sequence.PlayBackwards = reader.ReadBoolean();

        sequence.ManagerRef = reader.ReadInt32();
        sequence.AccumRootName = ReadSizedString(reader);

        if (context.Version >= Version_10_1_0_113 && context.Version <= Version_20_1_0_0)
            sequence.StringPaletteRef = reader.ReadInt32();

        return sequence;
    }

    private ControlledBlockDefinition ParseControlledBlock(BinaryReader reader, KfContext context)
    {
        var block = new ControlledBlockDefinition();
        if (context.Version <= Version_10_1_0_103)
            block.NodeName = ReadSizedString(reader);

        if (context.Version >= Version_10_1_0_106)
            block.InterpolatorRef = reader.ReadInt32();

        if (context.Version <= Version_20_5_0_0)
            block.ControllerRef = reader.ReadInt32();

        if (context.Version <= Version_10_1_0_110)
        {
            block.BlendInterpolatorRef = reader.ReadInt32();
            block.BlendIndex = reader.ReadUInt16();
        }

        if (context.Version >= Version_10_2_0_0 && context.Version <= Version_20_1_0_0)
        {
            block.StringPaletteRef = reader.ReadInt32();
            block.NodeNameOffset = reader.ReadUInt32();
            block.PropertyTypeOffset = reader.ReadUInt32();
            block.ControllerTypeOffset = reader.ReadUInt32();
            block.ControllerIdOffset = reader.ReadUInt32();
            block.InterpolatorIdOffset = reader.ReadUInt32();
        }
        else
        {
            block.NodeName = ReadSizedString(reader);
            block.PropertyType = ReadSizedString(reader);
            block.ControllerType = ReadSizedString(reader);
            block.ControllerId = ReadSizedString(reader);
            block.InterpolatorId = ReadSizedString(reader);
        }

        return block;
    }

    private NiTransformInterpolatorBlock ParseNiTransformInterpolator(BinaryReader reader)
    {
        var transform = new NiQuatTransform
        {
            Translation = ReadVector3(reader),
            Rotation = ReadQuaternion(reader),
            Scale = reader.ReadSingle()
        };
        int dataRef = reader.ReadInt32();
        return new NiTransformInterpolatorBlock(transform, dataRef);
    }

    private NiTransformDataBlock ParseNiTransformData(BinaryReader reader)
    {
        var data = new NiTransformDataBlock();
        data.NumRotationKeys = reader.ReadUInt32();
        if (data.NumRotationKeys > 0)
        {
            data.RotationType = ParseKeyType(reader.ReadUInt32());
            if (data.RotationType != KeyType.XyzRotation)
            {
                for (uint i = 0; i < data.NumRotationKeys; i++)
                    data.QuaternionKeys.Add(ReadQuatKey(reader, data.RotationType.Value));
            }
            else
            {
                for (int axis = 0; axis < 3; axis++)
                    data.XyzRotations.Add(ReadFloatKeyGroup(reader));
            }
        }

        data.Translations = ReadVector3KeyGroup(reader);
        data.Scales = ReadFloatKeyGroup(reader);

        return data;
    }

    private NiStringPaletteBlock ParseNiStringPalette(BinaryReader reader)
    {
        string palette = ReadSizedString(reader);
        uint length = reader.ReadUInt32();
        return new NiStringPaletteBlock(palette, length);
    }

    private NiFloatInterpolatorBlock ParseNiFloatInterpolator(BinaryReader reader)
    {
        float value = reader.ReadSingle();
        int dataRef = reader.ReadInt32();
        return new NiFloatInterpolatorBlock(value, dataRef);
    }

    private NiFloatDataBlock ParseNiFloatData(BinaryReader reader)
    {
        var block = new NiFloatDataBlock();
        block.Data = ReadFloatKeyGroup(reader);
        return block;
    }

    private NiTextKeyExtraDataBlock ParseNiTextKeyExtraData(BinaryReader reader)
    {
        var extra = new NiTextKeyExtraDataBlock
        {
            Name = ReadSizedString(reader)
        };

        uint count = reader.ReadUInt32();
        for (uint i = 0; i < count; i++)
        {
            float time = reader.ReadSingle();
            string value = ReadSizedString(reader);
            extra.TextKeys.Add(new TextKey(time, value));
        }

        return extra;
    }

    private AnimationSequence BuildAnimationSequence(NiControllerSequenceBlock sequence, KfContext context)
    {
        var tracks = new List<AnimationTrack>();
        foreach (var block in sequence.ControlledBlocks)
        {
            if (!TryResolveNodeName(block, sequence, context, out string? nodeName) || string.IsNullOrEmpty(nodeName))
                continue;

            var interpolator = context.GetBlock<NiTransformInterpolatorBlock>(block.InterpolatorRef);
            if (interpolator == null)
                continue;

            var data = context.GetBlock<NiTransformDataBlock>(interpolator.DataRef);
            var sampler = new AnimationSampler(interpolator.Transform, data);
            var track = new AnimationTrack(nodeName, sequence.StartTime, sequence.StopTime, sampler.SampleNormalized);
            tracks.Add(track);
        }

        return new AnimationSequence(
            sequence.Name,
            sequence.StartTime,
            sequence.StopTime,
            sequence.AccumRootName ?? string.Empty,
            sequence.Frequency,
            tracks);
    }

    private bool TryResolveNodeName(ControlledBlockDefinition block, NiControllerSequenceBlock sequence, KfContext context, out string? nodeName)
    {
        nodeName = block.NodeName;
        if (!string.IsNullOrEmpty(nodeName))
            return true;

        NiStringPaletteBlock? palette = null;
        if (block.StringPaletteRef >= 0)
            palette = context.GetBlock<NiStringPaletteBlock>(block.StringPaletteRef);

        if (palette == null && sequence.StringPaletteRef >= 0)
            palette = context.GetBlock<NiStringPaletteBlock>(sequence.StringPaletteRef);

        if (palette != null)
        {
            nodeName = palette.GetString(block.NodeNameOffset);
            return !string.IsNullOrEmpty(nodeName);
        }

        nodeName = null;
        return false;
    }

    private static KeyType ParseKeyType(uint value) => value switch
    {
        1 => KeyType.Linear,
        2 => KeyType.Quadratic,
        3 => KeyType.Tbc,
        4 => KeyType.XyzRotation,
        5 => KeyType.Const,
        _ => KeyType.Invalid
    };

    private static Vector3 ReadVector3(BinaryReader reader) =>
        new(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

    private static Quaternion ReadQuaternion(BinaryReader reader) =>
        new(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

    private static float ReadFloat(BinaryReader reader) => reader.ReadSingle();

    private static string ReadHeaderString(BinaryReader reader)
    {
        var bytes = new List<byte>();
        byte value;
        while ((value = reader.ReadByte()) != (byte)'\n')
            bytes.Add(value);
        return Encoding.ASCII.GetString(bytes.ToArray());
    }

    private static string ReadSizedString(BinaryReader reader)
    {
        uint first = reader.ReadUInt32();
        if (first == 0)
            return string.Empty;

        long start = reader.BaseStream.Position;
        if (reader.BaseStream.Position + sizeof(uint) <= reader.BaseStream.Length)
        {
            uint sentinel = reader.ReadUInt32();
            if (sentinel == 0xFFFFFFFF)
            {
                uint actualLength = reader.ReadUInt32();
                if (actualLength == 0)
                    return string.Empty;
                var inline = reader.ReadBytes((int)actualLength);
                return Encoding.Latin1.GetString(inline);
            }
            reader.BaseStream.Position = start;
        }

        if (first > reader.BaseStream.Length - reader.BaseStream.Position && reader.BaseStream.Position + sizeof(uint) * 3 <= reader.BaseStream.Length)
        {
            uint unknown = reader.ReadUInt32();
            uint sentinel = reader.ReadUInt32();
            if (sentinel != 0xFFFFFFFF)
                throw new InvalidDataException("Unexpected NiString encoding.");
            uint actualLength = reader.ReadUInt32();
            var inline = reader.ReadBytes((int)actualLength);
            return Encoding.Latin1.GetString(inline);
        }

        var data = reader.ReadBytes((int)first);
        return Encoding.Latin1.GetString(data);
    }

    private static KeyGroup<float> ReadFloatKeyGroup(BinaryReader reader)
    {
        var group = new KeyGroup<float>();
        group.NumKeys = reader.ReadUInt32();
        if (group.NumKeys == 0)
            return group;

        group.Interpolation = ParseKeyType(reader.ReadUInt32());
        for (uint i = 0; i < group.NumKeys; i++)
        {
            float time = reader.ReadSingle();
            float value = reader.ReadSingle();
            SkipKeyExtras(reader, group.Interpolation, KeyValueKind.Float);
            group.Keys.Add(new KeyFrame<float>(time, value));
        }

        return group;
    }

    private static KeyGroup<Vector3> ReadVector3KeyGroup(BinaryReader reader)
    {
        var group = new KeyGroup<Vector3>();
        group.NumKeys = reader.ReadUInt32();
        if (group.NumKeys == 0)
            return group;

        group.Interpolation = ParseKeyType(reader.ReadUInt32());
        for (uint i = 0; i < group.NumKeys; i++)
        {
            float time = reader.ReadSingle();
            var value = ReadVector3(reader);
            SkipKeyExtras(reader, group.Interpolation, KeyValueKind.Vector3);
            group.Keys.Add(new KeyFrame<Vector3>(time, value));
        }

        return group;
    }

    private static QuatKey ReadQuatKey(BinaryReader reader, KeyType keyType)
    {
        float time = reader.ReadSingle();
        var rotation = ReadQuaternion(reader);
        if (keyType == KeyType.Tbc)
            SkipTbc(reader);
        return new QuatKey(time, rotation);
    }

    private static void SkipKeyExtras(BinaryReader reader, KeyType interpolation, KeyValueKind kind)
    {
        if (interpolation == KeyType.Quadratic)
        {
            SkipValue(reader, kind);
            SkipValue(reader, kind);
        }
        if (interpolation == KeyType.Tbc)
            SkipTbc(reader);
    }

    private static void SkipValue(BinaryReader reader, KeyValueKind kind)
    {
        switch (kind)
        {
            case KeyValueKind.Vector3:
                reader.ReadSingle();
                reader.ReadSingle();
                reader.ReadSingle();
                break;
            case KeyValueKind.Float:
                reader.ReadSingle();
                break;
        }
    }

    private static void SkipTbc(BinaryReader reader)
    {
        reader.ReadSingle();
        reader.ReadSingle();
        reader.ReadSingle();
    }

    private static Matrix3 MatrixFromQuaternion(Quaternion quaternion)
    {
        quaternion = Quaternion.Normalize(quaternion);
        float xx = quaternion.X * quaternion.X;
        float yy = quaternion.Y * quaternion.Y;
        float zz = quaternion.Z * quaternion.Z;
        float xy = quaternion.X * quaternion.Y;
        float xz = quaternion.X * quaternion.Z;
        float yz = quaternion.Y * quaternion.Z;
        float wx = quaternion.W * quaternion.X;
        float wy = quaternion.W * quaternion.Y;
        float wz = quaternion.W * quaternion.Z;

        return new Matrix3(
            1f - 2f * (yy + zz), 2f * (xy - wz), 2f * (xz + wy),
            2f * (xy + wz), 1f - 2f * (xx + zz), 2f * (yz - wx),
            2f * (xz - wy), 2f * (yz + wx), 1f - 2f * (xx + yy)
        );
    }

    private sealed class KfContext
    {
        public string HeaderString { get; set; } = string.Empty;
        public uint Version { get; set; }
        public byte EndianType { get; set; }
        public uint UserVersion { get; set; }
        public uint NumBlocks { get; set; }
        public string[] BlockTypes { get; set; } = Array.Empty<string>();
        public int[] BlockTypeIndex { get; set; } = Array.Empty<int>();
        public object[] Blocks { get; set; } = Array.Empty<object>();

        public T? GetBlock<T>(int index) where T : class
        {
            if (index < 0 || index >= Blocks.Length)
                return null;
            return Blocks[index] as T;
        }
    }

    private sealed class NiControllerSequenceBlock
    {
        public string Name { get; set; } = string.Empty;
        public float Weight { get; set; }
        public float Frequency { get; set; }
        public float Phase { get; set; }
        public float StartTime { get; set; }
        public float StopTime { get; set; }
        public uint CycleType { get; set; }
        public int ManagerRef { get; set; } = -1;
        public int TextKeysRef { get; set; } = -1;
        public int StringPaletteRef { get; set; } = -1;
        public string? AccumRootName { get; set; }
        public bool PlayBackwards { get; set; }
        public List<ControlledBlockDefinition> ControlledBlocks { get; } = new();
    }

    private sealed class ControlledBlockDefinition
    {
        public int InterpolatorRef { get; set; } = -1;
        public int ControllerRef { get; set; } = -1;
        public int BlendInterpolatorRef { get; set; } = -1;
        public ushort BlendIndex { get; set; }
        public int StringPaletteRef { get; set; } = -1;
        public uint NodeNameOffset { get; set; }
        public uint PropertyTypeOffset { get; set; }
        public uint ControllerTypeOffset { get; set; }
        public uint ControllerIdOffset { get; set; }
        public uint InterpolatorIdOffset { get; set; }
        public string? NodeName { get; set; }
        public string? PropertyType { get; set; }
        public string? ControllerType { get; set; }
        public string? ControllerId { get; set; }
        public string? InterpolatorId { get; set; }
    }

    private sealed class NiTransformInterpolatorBlock
    {
        public NiTransformInterpolatorBlock(NiQuatTransform transform, int dataRef)
        {
            Transform = transform;
            DataRef = dataRef;
        }

        public NiQuatTransform Transform { get; }
        public int DataRef { get; }
    }

    private sealed class NiTransformDataBlock
    {
        public uint NumRotationKeys { get; set; }
        public KeyType? RotationType { get; set; }
        public List<QuatKey> QuaternionKeys { get; } = new();
        public List<KeyGroup<float>> XyzRotations { get; } = new();
        public KeyGroup<Vector3>? Translations { get; set; }
        public KeyGroup<float>? Scales { get; set; }
    }

    private sealed class NiStringPaletteBlock
    {
        private readonly string _palette;
        private readonly Dictionary<uint, string> _cache = new();

        public NiStringPaletteBlock(string palette, uint length)
        {
            _palette = palette;
            Length = length;
        }

        public uint Length { get; }

        public string? GetString(uint offset)
        {
            if (_palette.Length == 0 || offset >= _palette.Length)
                return null;

            if (_cache.TryGetValue(offset, out var cached))
                return cached;

            int start = (int)Math.Min(offset, (uint)_palette.Length - 1);
            int end = _palette.IndexOf('\0', start);
            string value = end >= 0 ? _palette[start..end] : _palette[start..];
            _cache[offset] = value;
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }

    private sealed class NiFloatInterpolatorBlock
    {
        public NiFloatInterpolatorBlock(float value, int dataRef)
        {
            DefaultValue = value;
            DataRef = dataRef;
        }

        public float DefaultValue { get; }
        public int DataRef { get; }
    }

    private sealed class NiFloatDataBlock
    {
        public KeyGroup<float>? Data { get; set; }
    }

    private sealed class NiTextKeyExtraDataBlock
    {
        public string Name { get; set; } = string.Empty;
        public List<TextKey> TextKeys { get; } = new();
    }

    private sealed class TextKey
    {
        public TextKey(float time, string value)
        {
            Time = time;
            Value = value;
        }

        public float Time { get; }
        public string Value { get; }
    }

    private sealed class KeyGroup<T>
    {
        public uint NumKeys { get; set; }
        public KeyType Interpolation { get; set; }
        public List<KeyFrame<T>> Keys { get; } = new();
    }

    private sealed class KeyFrame<T>
    {
        public KeyFrame(float time, T value)
        {
            Time = time;
            Value = value;
        }

        public float Time { get; }
        public T Value { get; }
    }

    private enum KeyType
    {
        Invalid = 0,
        Linear = 1,
        Quadratic = 2,
        Tbc = 3,
        XyzRotation = 4,
        Const = 5
    }

    private enum KeyValueKind
    {
        Float,
        Vector3
    }

    private sealed class QuatKey
    {
        public QuatKey(float time, Quaternion rotation)
        {
            Time = time;
            Rotation = rotation;
        }

        public float Time { get; }
        public Quaternion Rotation { get; }
    }

    private sealed class NiQuatTransform
    {
        public Vector3 Translation { get; set; }
        public Quaternion Rotation { get; set; }
        public float Scale { get; set; }
    }

    private sealed class AnimationSampler
    {
        private readonly NiQuatTransform _baseTransform;
        private readonly IReadOnlyList<QuatKey> _rotationKeys;
        private readonly KeyGroup<Vector3>? _translationKeys;
        private readonly KeyGroup<float>? _scaleKeys;
        private readonly float _minKeyTime;
        private readonly float _maxKeyTime;

        public AnimationSampler(NiQuatTransform baseTransform, NiTransformDataBlock? data)
        {
            _baseTransform = CleanBaseTransform(baseTransform);
            _rotationKeys = (IReadOnlyList<QuatKey>?)(data?.QuaternionKeys) ?? Array.Empty<QuatKey>();
            _translationKeys = data?.Translations;
            _scaleKeys = data?.Scales;
            (_minKeyTime, _maxKeyTime) = GetKeyRange(_rotationKeys, _translationKeys, _scaleKeys);
        }

        public TransformData SampleNormalized(float normalizedTime)
        {
            float clamped = Clamp01(normalizedTime);
            float time = _maxKeyTime > _minKeyTime
                ? _minKeyTime + clamped * (_maxKeyTime - _minKeyTime)
                : _minKeyTime;

            var translation = SampleTranslation(time);
            var rotation = SampleRotation(time);
            var scale = SampleScale(time);
            var matrix = MatrixFromQuaternion(rotation);
            return TransformData.FromComponents(translation, matrix, scale);
        }

        private Vector3 SampleTranslation(float time)
        {
            return SampleKeyGroup(_translationKeys, time, _baseTransform.Translation, Vector3.Lerp);
        }

        private Quaternion SampleRotation(float time)
        {
            if (_rotationKeys.Count == 0)
                return _baseTransform.Rotation;

            if (time <= _rotationKeys[0].Time)
                return _rotationKeys[0].Rotation;

            if (time >= _rotationKeys[^1].Time)
                return _rotationKeys[^1].Rotation;

            int index = 0;
            while (index < _rotationKeys.Count && _rotationKeys[index].Time < time)
                index++;

            if (index == 0)
                return _rotationKeys[0].Rotation;
            if (index >= _rotationKeys.Count)
                return _rotationKeys[^1].Rotation;

            var before = _rotationKeys[index - 1];
            var after = _rotationKeys[index];
            float span = after.Time - before.Time;
            float t = span <= float.Epsilon ? 0f : (time - before.Time) / span;
            return Quaternion.Slerp(before.Rotation, after.Rotation, Clamp01(t));
        }

        private float SampleScale(float time)
        {
            float scale = SampleKeyGroup(_scaleKeys, time, _baseTransform.Scale, Lerp);
            return scale <= 0f ? 1f : scale;
        }

        private static T SampleKeyGroup<T>(KeyGroup<T>? group, float time, T fallback, Func<T, T, float, T> lerp)
        {
            if (group == null || group.Keys.Count == 0)
                return fallback;

            if (time <= group.Keys[0].Time)
                return group.Keys[0].Value;
            if (time >= group.Keys[^1].Time)
                return group.Keys[^1].Value;

            int index = 1;
            while (index < group.Keys.Count && group.Keys[index].Time < time)
                index++;

            var before = group.Keys[index - 1];
            var after = group.Keys[index];
            float span = after.Time - before.Time;
            float t = span <= float.Epsilon ? 0f : (time - before.Time) / span;
            return lerp(before.Value, after.Value, Clamp01(t));
        }

        private static (float min, float max) GetKeyRange(
            IReadOnlyList<QuatKey> rotationKeys,
            KeyGroup<Vector3>? translationKeys,
            KeyGroup<float>? scaleKeys)
        {
            float min = float.PositiveInfinity;
            float max = float.NegativeInfinity;

            UpdateRange(ref min, ref max, rotationKeys);
            UpdateRange(ref min, ref max, translationKeys);
            UpdateRange(ref min, ref max, scaleKeys);

            if (float.IsPositiveInfinity(min))
                min = 0f;
            if (float.IsNegativeInfinity(max))
                max = 0f;

            return (min, max);
        }

        private static void UpdateRange(ref float min, ref float max, IReadOnlyList<QuatKey>? keys)
        {
            if (keys == null || keys.Count == 0)
                return;

            float first = keys[0].Time;
            float last = keys[^1].Time;
            if (first < min)
                min = first;
            if (last > max)
                max = last;
        }

        private static void UpdateRange(ref float min, ref float max, KeyGroup<Vector3>? group)
        {
            if (group == null || group.Keys.Count == 0)
                return;

            UpdateRange(ref min, ref max, group.Keys);
        }

        private static void UpdateRange(ref float min, ref float max, KeyGroup<float>? group)
        {
            if (group == null || group.Keys.Count == 0)
                return;

            UpdateRange(ref min, ref max, group.Keys);
        }

        private static void UpdateRange<T>(ref float min, ref float max, IReadOnlyList<KeyFrame<T>>? keys)
        {
            if (keys == null || keys.Count == 0)
                return;

            float first = keys[0].Time;
            float last = keys[^1].Time;
            if (first < min)
                min = first;
            if (last > max)
                max = last;
        }

        private static float Clamp01(float value)
        {
            if (value <= 0f)
                return 0f;
            if (value >= 1f)
                return 1f;
            return value;
        }

        private static float Lerp(float start, float end, float t) => start + (end - start) * t;

        private static NiQuatTransform CleanBaseTransform(NiQuatTransform transform)
        {
            if (transform.Scale <= 0f)
                transform.Scale = 1f;
            return transform;
        }
    }
}

public sealed class AnimationSequence
{
    public AnimationSequence(string name, float startTime, float stopTime, string accumRootName, float frequency, IReadOnlyList<AnimationTrack> tracks)
    {
        Name = name;
        StartTime = startTime;
        StopTime = stopTime;
        AccumRootName = accumRootName;
        Frequency = frequency;
        Tracks = tracks;
    }

    public string Name { get; }
    public float StartTime { get; }
    public float StopTime { get; }
    public string AccumRootName { get; }
    public float Frequency { get; }
    public IReadOnlyList<AnimationTrack> Tracks { get; }
}

public sealed class AnimationTrack
{
    private readonly Func<float, TransformData> _sampler;

    public AnimationTrack(string nodeName, float startTime, float stopTime, Func<float, TransformData> sampler)
    {
        NodeName = nodeName;
        _sampler = sampler;
    }

    public string NodeName { get; }

    public TransformData Sample(float normalizedTime) => _sampler(normalizedTime);
}
