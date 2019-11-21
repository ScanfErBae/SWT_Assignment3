using System.Diagnostics;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT10_11_12_Button_UserInterface_Door_CookController_Timer_Light_Display_PowerTube
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
        public void Press_once_show_displayEqual50()
        {
            _sutPowerButton.Press();
            _output.Received(1).OutputLine("Display shows: 50 W");
        }

        [Test]
        public void Press_four_times_show_displayEqual200()
        {
            _sutPowerButton.Press();
            _sutPowerButton.Press();
            _sutPowerButton.Press();
            _sutPowerButton.Press();
            _output.Received(1).OutputLine("Display shows: 50 W");
            _output.Received(1).OutputLine("Display shows: 100 W");
            _output.Received(1).OutputLine("Display shows: 150 W");
            _output.Received(1).OutputLine("Display shows: 200 W");
        }

        [Test]
        public void Press_twenty_times_show_displayEqual700()
        {
            for (int i = 0; i < 20; i++)
            {
                _sutPowerButton.Press();
            }
            _output.Received(1).OutputLine("Display shows: 700 W");
        }

        #endregion

        #region TimeButton
        [Test]
        public void Set_Time_Press_Power_Then_TimeButton_twice_show_displayEqual2()
        {
            _sutPowerButton.Press();
            _sutTimeButton.Press();
            _output.Received(1).OutputLine("Display shows: 50 W");
            _output.Received(1).OutputLine("Display shows: 01:00");
        }

        [Test]
        public void Increase_Time_Press_Power_Then_TimeButton_four_times_show_displayEqual2()
        {
            _sutPowerButton.Press();
            _sutTimeButton.Press();
            _sutTimeButton.Press();
            _sutTimeButton.Press();
            _sutTimeButton.Press();
            _output.Received(1).OutputLine("Display shows: 50 W");
            _output.Received(1).OutputLine("Display shows: 01:00");
            _output.Received(1).OutputLine("Display shows: 02:00");
            _output.Received(1).OutputLine("Display shows: 03:00");
            _output.Received(1).OutputLine("Display shows: 04:00");
            
        }

        [Test]
        public void Not_Pressing_Power_Then_TimeButton_once_Exception()
        {
            _sutTimeButton.Press();
            _output.DidNotReceive().OutputLine("Display shows: 01:00");
        }

        #endregion

        #region Start-CancelButton
        [Test]
        public void StartCancelButton_PowerButtonAndTimeButtonIsPressedOnce_LightTurnOnAndCookControllerStartsAndPowerTubeTurnedOn()
        {
            _sutPowerButton.Press();
            _output.Received(1).OutputLine("Display shows: 50 W");
            _sutTimeButton.Press();
            _output.Received(1).OutputLine("Display shows: 01:00");

            _sutStartCancelButton.Press();

            //Light works by pressing StartCancelButton
            Assert.That(_light.isOn, Is.EqualTo(true));
            _output.Received(1).OutputLine("Light is turned on");

            //CookController is cooking & calling to the timer class
            

            //Powertube works by pressing StartCancelButton
            Assert.That(_powerTube.IsOn, Is.EqualTo(true));
            _output.Received(1).OutputLine("PowerTube works with 7 %");
        }


        #endregion
    }
}