using Essentials.Utilities;

namespace Generics.Packages.InputSystem
{


    public class InputController : Singleton<InputController>
    {
        public GameInput Input;

        public InputController()
        {
            Input = new GameInput();
            Input.Enable();
        }


    }

}