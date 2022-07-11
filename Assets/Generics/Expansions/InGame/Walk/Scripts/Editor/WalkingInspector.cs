using Generics.Editor.Inspector;
using Generics.Packages.Walk;
using UnityEditor;

namespace Generics.Packages.Runner
{

    [CustomEditor(typeof(WalkingBehaviour))]
    public class WalkingInspector : ComponentInspectorBase<WalkingBehaviour>
    {

        protected override void DrawOptions()
        {
            base.DrawOptions();

            DrawHeader("Input");
            DrawComponentOption<WalkingExtension_JoystickInput>("Joystick");
            DrawComponentOption<WalkingExtension_FollowTargetInput>("Follow Cursor(single runner game only)");

            DrawHeader("Movement");
            DrawComponentOption<WalkingMove_Teleport>("Teleport");
            DrawComponentOption<WalkingMove_Lerp>("Lerp");
            DrawComponentOption<WalkingMove_Speed>("Speed");
            DrawComponentOption<WalkingMove_CharacterController>("Character Controller");
            DrawComponentOption<WalkingMove_NavMeshAgent>("Nav Mesh Agent");

            //Speed extensions
            if (Target.TryGetComponent<WalkingMove_Speed>(out var _speedRef))
            {
                DrawHeader("Speed Extensions");
                DrawComponentOption<WalkingExtension_AdjustSpeedByDistanceToDestination>("Adjust Speed By Destination Distance");
                DrawComponentOption<WalkingExtension_AdjustAnimSpeedBySpeed>("Adjust Animation Speed By Run Speed");
            }

            DrawHeader("Rotation");
            DrawComponentOption<WalkingRotation_Destination>("Direction");
            DrawComponentOption<WalkingRotation_LookAt>("Look At");

            DrawHeader("Misc");
            DrawComponentOption<WalkingExtension_WalkAnimation>("Animate Walking");

        }

    }

}