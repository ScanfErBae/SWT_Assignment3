using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT11_12_13_Button_UserInterface_Door_CookController_Timer_Light_Display_PowerTube
    {
        public IOutput _output;
        public Display _display;
        public Light _light;
        public Timer _timer;
        public PowerTube _powerTube;
        public CookController _cookController;
        public UserInterface _userInterface;
        public Door _door;

        public Button _sutPowerButton;
        public Button _sutTimeButton;
        public Button _sutStartCancelButton;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
            _light = new Light(_output);
            _sutPowerButton = new Button();
            _sutTimeButton = new Button();
            _sutStartCancelButton = new Button();
            _timer = new Timer();
            _powerTube = new PowerTube(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
            _door = new Door();
            _userInterface = new UserInterface(_sutPowerButton, _sutTimeButton, _sutStartCancelButton, _door, _display, _light, _cookController);
        }

        #region PowerButton

        [Test]
        public void Press_once_show_displayEqual100()
        {
            _sutPowerButton.Press();
            _display.ShowPower(50);
        }

        [Test]
        public void Press_four_times_show_displayEqual200()
        {
            _sutPowerButton.Press();
            _sutPowerButton.Press();
            _sutPowerButton.Press();
            _sutPowerButton.Press();
            _display.ShowPower(200);
        }

        [Test]
        public void Press_twenty_times_show_displayEqual700()
        {
            for (int i = 0; i < 20; i++)
            {
                _sutPowerButton.Press();
            }
            _display.ShowPower(700);
        }

        #endregion
    }
}