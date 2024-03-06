using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActivator
{
    public Activable Activable { get; }
    public void Activate();
}
