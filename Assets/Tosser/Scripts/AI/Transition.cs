using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tosser.AI
{
    [System.Serializable]
    public class Transition
    {
        public Decision decision;
        public State trueState;
        public State falseState;
    }
}