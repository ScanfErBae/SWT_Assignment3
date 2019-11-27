using System;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT11_Door_UserInterface
    {
        public IDisplay _display;
        public ILight _light;
        public ICookController _cookController;
        public IButton _buttonPower;
        public IButton _buttonTime;
        public IButton _buttonStartCancel;
        public UserInterface _userInterface;

        public Door _sut;

        [SetUp]
        public void SetUp()
        {
            _display = Substitute.For<IDisplay>();
            _light = Substitute.For<ILight>();
            _cookController = Substitute.For<ICookController>();
            _buttonPower = Substitute.For<IButton>();
            _buttonTime = Substitute.For<IButton>();
            _buttonStartCancel = Substitute.For<IButton>();
            _sut = new Door();
            _userInterface = new UserInterface(_buttonPower,_buttonTime, _buttonStartCancel, _sut, _display, _light, _cookController);
        }

        [Test]
        public void OpenDoor_1SubscriberAndReady_LightTurnOnIsCalled()
        {
            _sut.Open();
            _light.Received(1).TurnOn();
        }

        [Test]
        public void CloseDoor_1SubscriberAndDoorWasOpen_LightTurnOffIsCalled()
        {
            _sut.Open();
            _sut.Close();
            _light.Received(1).TurnOff();
        }

        [Test]
        public void OpenDoor_1SubscriberAndCooking_CookControllerIsStoppedAndDisplayIsCleared()
        {
            _buttonPower.Pressed += Raise.EventWith(this, EventArgs.Empty);
            // Now in SetPower
            _buttonTime.Pressed += Raise.EventWith(this, EventArgs.Empty);
            // Now in SetTime
            _buttonStartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);
            // Now in SetPower
            _sut.Open();

            _cookController.Received(1).Stop();
            _display.Received(1).Clear();
        }
    }
}