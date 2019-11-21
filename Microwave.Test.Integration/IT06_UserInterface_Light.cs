using System;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT06_UserInterface_Light
    {
        public IDisplay _display;
        public IDoor _door;
        public IButton _buttonPower;
        public IButton _buttonTime;
        public IButton _buttonStartCancel;
        public IOutput _output;
        public ICookController _cookController;

        public Light _light;
        public UserInterface _sut;


        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _display = Substitute.For<IDisplay>();
            _buttonPower = Substitute.For<IButton>();
            _buttonTime = Substitute.For<IButton>();
            _buttonStartCancel = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _cookController = Substitute.For<ICookController>();

            _light = new Light(_output);
            _sut = new UserInterface(_buttonPower, _buttonTime, _buttonStartCancel, _door, _display, _light,_cookController);
        }

        [Test]
        public void State_Ready_Open_Door()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);

            _output.Received(1).OutputLine("Light is turned on");
        }

        [Test]
        public void State_Door_Is_Open_Close_Door()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);

            _output.Received(1).OutputLine("Light is turned off");
        }


        [Test]
        public void State_Set_Time_Press_Start_Cancel_Button()
        {
            if (_buttonPower != null) _buttonPower.Pressed += Raise.EventWith(this, EventArgs.Empty);
            // Now in SetPower
            _buttonTime.Pressed += Raise.EventWith(this, EventArgs.Empty);
            // Now in SetTime
            _buttonStartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _output.Received(1).OutputLine("Light is turned on");
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
            _output.Received(1).OutputLine("Light is turned off");
        }

        [Test]
        public void ButtonStartCancel_Pressed_When_Cooking_()
        {
            _buttonPower.Pressed += Raise.EventWith(this, EventArgs.Empty);
            // Now in SetPower
            _buttonTime.Pressed += Raise.EventWith(this, EventArgs.Empty);
            // Now in SetTime
            _buttonStartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _buttonStartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);

            // Now in SetTime
            _output.Received(1).OutputLine("Light is turned off");
        }

    }



}


