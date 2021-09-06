using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tosser.Core;
public interface IDragCharacter
{
    void DragCharacter(DragCharacter draggingCharacter);

    void DragStopped();
}