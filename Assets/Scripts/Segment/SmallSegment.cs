using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SegmentUpgrades
{
    None,

    // Basic
    Fire,
    Electricity,
    Laser,
    Shredder,
    Mirror,
    Explosive,
    Lifesteal,
    Shield,
    Slime,

    // Doubles
    DoubleFire,
    DoubleElectricity,
    DoubleLaser,
    DoubleShredder,
    DoubleMirror,
    DoubleExplosive,
    DoubleLifesteal,
    DoubleShield,
    DoubleSlime,

    // Combos
    FireShield,
    ElectricityShield,
    ExplosiveShield,
    ElectricityShredder,
    LaserShredder,
    FireMirror,
    ElectricityMirror,
    ExplosiveMirror,
    ShredderLifesteal,

    // Super combos
    TripleExplosive,
    TripleShredder,
    TripleLifesteal,
    ElectricityShredderFire,
    ElectricityShredderExplosive,
    ElectricityShieldSlime
}


public class SmallSegment : MonoBehaviour
{
    // Ball Joints
    public Transform object1;
    public Transform object2;

    // Type
    public SegmentUpgrades segmentType;
    public uint id;

    // Helpper
    private Vector3 initial_position;
    private float initial_distance;

    void Initialize()
    {
        initial_position = transform.position;
        initial_distance = Vector3.Distance(object1.position, object2.position);
    }

    void CorrectPlacement()
    {
        transform.position = (object1.position + object2.position) / 2f;
        transform.rotation = Quaternion.LookRotation((object2.position - object1.position).normalized);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Vector3.Distance(object1.position, object2.position));
    }

    private void Start()
    {
        Initialize();
    }

    void FixedUpdate()
    {
        CorrectPlacement();
    }
}
