using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickableObject : InteractableObject
{
    protected override void Interact()
    {
        print("pick up the object");
    }
}
