using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabable<T>
{
    void Grab(T objectToGrab);
}
