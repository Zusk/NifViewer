NiTransformData
├─ Num Rotation Keys: 1 (uint)
├─ Rotation Type: XYZ_ROTATION_KEY (KeyType)
├─ XYZ Rotations (KeyGroup<float>)
│  ├─ XYZ Rotations (KeyGroup<float>)
│  │  ├─ Num Keys: 5 (uint)
│  │  ├─ Interpolation: QUADRATIC_KEY (KeyType)
│  │  └─ Keys (Key<float>)
│  │     ├─ Key
│  │     │  ├─ Time: 0.000000 (float)
│  │     │  ├─ Value: 3.139730 (float)
│  │     │  ├─ Forward: 0.000004 (float)
│  │     │  └─ Backward: 0.000072 (float)
│  │     ├─ Key
│  │     │  ├─ Time: 0.666667 (float)
│  │     │  ├─ Value: 3.140051 (float)
│  │     │  ├─ Forward: 0.000137 (float)
│  │     │  └─ Backward: 0.000439 (float)
│  │     ├─ Key
│  │     │  ├─ Time: 2.800000 (float)
│  │     │  ├─ Value: 3.217782 (float)
│  │     │  ├─ Forward: 0.000000 (float)
│  │     │  └─ Backward: 0.000000 (float)
│  │     ├─ Key
│  │     │  ├─ Time: 4.966667 (float)
│  │     │  ├─ Value: 3.139501 (float)
│  │     │  ├─ Forward: -0.000000 (float)
│  │     │  └─ Backward: 0.000000 (float)
│  │     └─ Key
│  │        ├─ Time: 5.900000 (float)
│  │        ├─ Value: 3.139726 (float)
│  │        ├─ Forward: 0.000079 (float)
│  │        └─ Backward: 0.000006 (float)
│  ├─ XYZ Rotations (KeyGroup<float>)
│  │  ├─ Num Keys: 5 (uint)
│  │  ├─ Interpolation: QUADRATIC_KEY (KeyType)
│  │  └─ Keys (Key<float>)
│  │     ├─ Key
│  │     ├─ Key
│  │     ├─ Key
│  │     ├─ Key
│  │     └─ Key
│  └─ XYZ Rotations (KeyGroup<float>)
│     └─ (empty / collapsed)
├─ Translations (KeyGroup<Vector3>)
│  └─ (empty)
└─ Scales (KeyGroup<float>)
   ├─ Num Keys: 2 (uint)
   ├─ Interpolation: QUADRATIC_KEY (KeyType)
   └─ Keys (Key<float>)
      ├─ Key
      │  ├─ Time: 0.000000 (float)
      │  ├─ Value: 1.000000 (float)
      │  ├─ Forward: 0.000000 (float)
      │  └─ Backward: 0.000000 (float)
      └─ Key
         ├─ Time: 5.900000 (float)
         ├─ Value: 1.000000 (float)
         ├─ Forward: 0.000000 (float)
         └─ Backward: 0.000000 (float)

Notes (important for parsing / tooling)

    XYZ Rotations appears 3 times
    → corresponds to X, Y, Z rotation channels

    Each rotation axis is a KeyGroup<float>

    QUADRATIC_KEY means:

(time, value, forward_tangent, backward_tangent)

The second and third XYZ groups are present but visually collapsed

Translations exist structurally but contain no keys

Scale is constant over time (1.0 → 1.0)
