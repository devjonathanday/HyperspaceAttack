using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    enum Type { point, damageBoost }
    uint ID { get; set; }
    Type type { get; set; }
}
