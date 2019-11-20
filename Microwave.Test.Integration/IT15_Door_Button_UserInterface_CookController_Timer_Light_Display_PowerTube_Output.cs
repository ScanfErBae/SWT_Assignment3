using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT15_Door_Button_UserInterface_CookController_Timer_Light_Display_PowerTube_Output
    {
        public Output _output;
        public Display _display;
        public Light _light;
        public Button _buttonPower;
        public Button _buttonTime;
        public Button _buttonStartCancle;
        public Timer _timer;
        public PowerTube _powerTube;
        public CookController _cookController;
        public UserInterface _userInterface;

        public Door _sut;

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _display = new Display(_output);
            _light = new Light(_output);
            _buttonPower = new Button();
            _buttonTime = new Button();
            _buttonStartCancle = new Button();
            _sut = new Door();
            _timer = new Timer();
            _powerTube = new PowerTube(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
            _userInterface = new UserInterface(_buttonPower,_buttonTime, _buttonStartCancle, _sut, _display, _light, _cookController);
        }
    }
}