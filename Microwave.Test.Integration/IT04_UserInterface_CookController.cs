using System;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT04_UserInterface_CookController
    {
        public IDisplay _display;
        public ILight _light;
        public IDoor _door;
        public IButton _buttonPower;
        public IButton _buttonTime;
        public IButton _buttonStartCancel;
        public ITimer _timer;
        public IPowerTube _powerTube;
        
        public CookController _cookController;
        public UserInterface _sut;

        [SetUp]
        public void SetUp()
        {
            _display = Substitute.For<IDisplay>();
            _light = Substitute.For<ILight>();
            _buttonPower = Substitute.For<IButton>();
            _buttonTime = Substitute.For<IButton>();
            _buttonStartCancel = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _timer = Substitute.For<ITimer>();
            _powerTube = Substitute.For<IPowerTube>();

            _cookController = new CookController(_timer, _display, _powerTube);
            _sut = new UserInterface(_buttonPower,_buttonTime, _buttonStartCancel, _door, _display, _light, _cookController);
        }

        [Test]
        public void OnStartCancelPressed_TimeSet60_TimerReceivedStart60()
        {
            _buttonPower.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _buttonTime.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _buttonStartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _timer.Received(1).Start(60);
        }

        [Test]
        public void OnStartCancelPressed_PowerSet50_PowerTubeReceivedTurnOn50()
        {
            _buttonPower.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _buttonTime.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _buttonStartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _powerTube.Received(1).TurnOn(7);
        }

     



        [Test]
        public void OnDoorOpened_MicrowaveIsCooking_TimerStopped()
        {
            _buttonPower.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _buttonTime.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _buttonStartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _door.Opened += Raise.EventWith(this, EventArgs.Empty);

            _timer.Received().Stop();
        }

        [Test]
        public void OnDoorOpened_MicrowaveIsCooking_PowerTubeTurnedOff()
        {
            _buttonPower.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _buttonTime.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _buttonStartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _door.Opened += Raise.EventWith(this, EventArgs.Empty);

            _powerTube.Received().TurnOff();
        }

        [Test]
        public void OnStartCancelPressed_MicrowaveIsCooking_TimerStopped()
        {
            _buttonPower.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _buttonTime.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _buttonStartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _buttonStartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _timer.Received().Stop();
        }

        [Test]
        public void OnStartCancelPressed_MicrowaveIsCooking_PowerTubeTurnedOff()
        {
            _buttonPower.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _buttonTime.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _buttonStartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _buttonStartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _powerTube.Received().TurnOff();
        }
    }
}