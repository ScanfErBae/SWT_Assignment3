using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT10_UserInterface_Button_Door_CookController_Timer_Light_Display_PowerTube_Output
    {
        public Output _output;
        public Display _display;
        public Light _light;
        public Door _door;
        public Button _buttonPower;
        public Button _buttonTime;
        public Button _buttonStartCancle;
        public Timer _timer;
        public PowerTube _powerTube;
        public CookController _sut;
        public UserInterface _userInterface;

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _display = new Display(_output);
            _light = new Light(_output);
            _buttonPower = new Button();
            _buttonTime = new Button();
            _buttonStartCancle = new Button();
            _door = new Door();
            _timer = new Timer();
            _powerTube = new PowerTube(_output);
            _sut = new CookController(_timer, _display, _powerTube);
            _userInterface = new UserInterface(_buttonPower,_buttonTime, _buttonStartCancle, _door, _display, _light, _sut);
        }
    }
}