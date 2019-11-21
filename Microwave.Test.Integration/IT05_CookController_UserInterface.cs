using System;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT05_CookController_UserInterface
    {
        public IDisplay _display;
        public ILight _light;
        public IDoor _door;
        public IButton _buttonPower;
        public IButton _buttonTime;
        public IButton _buttonStartCancel;
        public ITimer _timer;
        public IPowerTube _powerTube;
        
        public UserInterface _userInterface;
        public CookController _sut;

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
            
            _sut = new CookController(_timer, _display, _powerTube);
            _userInterface = new UserInterface(_buttonPower,_buttonTime, _buttonStartCancel, _door, _display, _light, _sut);
            _sut.UI = _userInterface;


        }

        [Test]
        public void Cooking_TimerExpired_UICalled()
        {
            _buttonPower.Pressed += Raise.EventWith(this, EventArgs.Empty);
            // Now in SetPower
            _buttonTime.Pressed += Raise.EventWith(this, EventArgs.Empty);
            // Now in SetTime
            _buttonStartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            _display.Received().Clear();
            _light.Received(1).TurnOff();

        }
    }
}