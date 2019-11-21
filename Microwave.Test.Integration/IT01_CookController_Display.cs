using System;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT01_CookController_Display
    {
        public IDisplay _display;
        public IOutput _output;
        public ITimer _timer;
        public IUserInterface _userInterface;
        public IPowerTube _powerTube;

        public CookController _sut;

        [SetUp]
        public void SetUp()
        {
            _userInterface = Substitute.For<IUserInterface>();
            _timer = Substitute.For<ITimer>();
            _powerTube = Substitute.For<IPowerTube>();
            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
            _sut = new CookController(_timer, _display, _powerTube, _userInterface);
        }

        [Test]

        public void Cooking_TimerTick_DisplayCalled()
        {
            _sut.StartCooking(50, 60);

            _timer.TimeRemaining.Returns(115);
            _timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("1:55")));
        }


    }
}
