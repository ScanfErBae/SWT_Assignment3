using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT15_Door_Button_UserInterface_CookController_Timer_Light_Display_PowerTube
    {
        public IOutput _output;
        public Display _display;
        public Light _light;
        public Button _buttonPower;
        public Button _buttonTime;
        public Button _buttonStartCancel;
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
            _buttonStartCancel = new Button();
            _timer = new Timer();
            _powerTube = new PowerTube(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
            _sut = new Door();
            _userInterface = new UserInterface(_buttonPower,_buttonTime, _buttonStartCancel, _sut, _display, _light, _cookController);
        }

        [Test]
        public void OpenDoor_1SubscriberAndReady_LightTurnOnIsCalled_OutputLine_Tested()
        {
            _sut.Open();
            _output.Received(1).OutputLine("Light is turned on");
        }

        [Test]
        public void CloseDoor_1SubscriberAndDoorWasOpen_LightTurnOffIsCalled_OutputLine_Tested()
        {
            _sut.Open();
            _sut.Close();
            _output.Received(1).OutputLine("Light is turned off");
        }

        [Test]
        public void OpenDoor_1SubscriberAndCooking_CookControllerIsStoppedAndDisplayIsCleared_OutputLine_Tested()
        {
            _buttonPower.Press();
            // Now in SetPower
            _buttonTime.Press();
            // Now in SetTime
            _buttonStartCancel.Press();
            // Now in SetPower
            _sut.Open();

            _output.Received(1).OutputLine("PowerTube turned off");
            _output.Received(1).OutputLine("Display cleared");
        }

        [Test]
        public void OpenDoor_When_PowerSet_OutputLine_Tested()
        {
            _buttonPower.Press();
            // Now in SetPower
            _sut.Open();

            _output.Received(1).OutputLine("Display cleared");
            _output.Received(1).OutputLine("Light is turned on");
        }

        [Test]
        public void OpenDoor_When_SetTime_OutputLine_Tested()
        {
            _buttonPower.Press();
            // Now in SetPower
            _buttonTime.Press();
            // Now in SetTime
            _sut.Open();

            _output.Received(1).OutputLine("Display cleared");
            _output.Received(1).OutputLine("Light is turned on");
        }
    }

}
