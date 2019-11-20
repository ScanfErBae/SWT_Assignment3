using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT16_17_18_Button_UserInterface_Door_CookController_Timer_Light_Display_PowerTube_Output
    {
        public Output _output;
        public Display _display;
        public Light _light;
        public Door _door;
        public Timer _timer;
        public PowerTube _powerTube;
        public CookController _cookController;
        public UserInterface _userInterface;

        public Button _sutPowerButton;
        public Button _sutTimeButton;
        public Button _sutStartCancleButton;

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _display = new Display(_output);
            _light = new Light(_output);
            _sutPowerButton = new Button();
            _sutTimeButton = new Button();
            _sutStartCancleButton = new Button();
            _door = new Door();
            _timer = new Timer();
            _powerTube = new PowerTube(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
            _userInterface = new UserInterface(_sutPowerButton, _sutTimeButton, _sutStartCancleButton, _door, _display, _light, _cookController);
        }
    }
}