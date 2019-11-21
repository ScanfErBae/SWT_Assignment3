using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT14_Door_Button_UserInterface_CookController_Timer_Light_Display_PowerTube
    {
        public IOutput _output;
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
            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
            _light = new Light(_output);
            _buttonPower = new Button();
            _buttonTime = new Button();
            _buttonStartCancle = new Button();
            _timer = new Timer();
            _powerTube = new PowerTube(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
            _sut = new Door();
            _userInterface = new UserInterface(_buttonPower,_buttonTime, _buttonStartCancle, _sut, _display, _light, _cookController);
        }


    }
}