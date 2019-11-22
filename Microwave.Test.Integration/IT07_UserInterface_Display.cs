using System;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    class IT07_UserInterface_Display
    {
        public IOutput _output;
        public IPowerTube _powerTube;
        public ICookController _cookController;
        public IButton _buttonPower;
        public IButton _buttonTime;
        public IButton _buttonStartCancel;
        public IDoor _door;
        public ILight _light;


        public Display _display;
        public Timer _timer;
        public UserInterface _sut;


        [SetUp]
        public void SetUp()
        {
            _cookController = Substitute.For<ICookController>();
            _powerTube = Substitute.For<IPowerTube>();
            _output = Substitute.For<IOutput>();
            _buttonPower = Substitute.For<IButton>();
            _buttonTime = Substitute.For<IButton>();
            _buttonStartCancel = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _light = Substitute.For<ILight>();

            
            _display = new Display(_output);
            _sut = new UserInterface(_buttonPower, _buttonTime, _buttonStartCancel, _door, _display, _light, _cookController);
        }

        [Test]
        public void PowerButton_Pressed_ShowPower_Called()
        {
            _buttonPower.Pressed += Raise.EventWith(this, EventArgs.Empty);
            // Now in SetPower
            _output.Received(1).OutputLine("Display shows: 50 W");
        }

        [Test]
        public void TimeButton_Pressed_ShowTime_Called()
        {
            _buttonPower.Pressed += Raise.EventWith(this, EventArgs.Empty);
            // Now in SetPower
            _buttonTime.Pressed += Raise.EventWith(this, EventArgs.Empty);
            // Now in SetTime
            _output.Received(1).OutputLine("Display shows: 01:00");
        }

        [Test]
        public void CookingIsDone_Clear_Called()
        {
            _buttonPower.Pressed += Raise.EventWith(this, EventArgs.Empty);
            // Now in SetPower
            _buttonTime.Pressed += Raise.EventWith(this, EventArgs.Empty);
            // Now in SetTime
            _buttonStartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _sut.CookingIsDone();

            // Now in SetTime
            _output.Received(1).OutputLine("Display cleared");
        }

        [Test]
        public void Door_Open_When_Cooking_Clear_Called()
        {
            _buttonPower.Pressed += Raise.EventWith(this, EventArgs.Empty);
            // Now in SetPower
            _buttonTime.Pressed += Raise.EventWith(this, EventArgs.Empty);
            // Now in SetTime
            _buttonStartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);
            
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);

            // Now in SetTime
            _output.Received(1).OutputLine("Display cleared");
        }

        [Test]
        public void ButtonStartCancle_Pressed_When_Cooking_Clear_Called()
        {
            _buttonPower.Pressed += Raise.EventWith(this, EventArgs.Empty);
            // Now in SetPower
            _buttonTime.Pressed += Raise.EventWith(this, EventArgs.Empty);
            // Now in SetTime
            _buttonStartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _buttonStartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);

            // Now in SetTime
            _output.Received(1).OutputLine("Display cleared");
        }

    }
}
