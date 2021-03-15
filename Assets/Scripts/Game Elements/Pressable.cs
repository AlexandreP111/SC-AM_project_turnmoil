using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressable : MonoBehaviour
{
    public virtual void reset() { }
    public virtual void press() { }
    public virtual void depress() { }
}
