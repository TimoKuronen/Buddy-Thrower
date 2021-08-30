using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tosser.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Wander")]
    public class WanderAction : Action
    {
        public override void Act(StateController controller)
        {
            Wander(controller);
        }
        private void Wander(StateController controller)
        {

        }
    }
}