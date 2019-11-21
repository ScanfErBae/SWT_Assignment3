using System;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT03_CookController_Display_Time
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
            _timer = new Timer();
            _powerTube = Substitute.For<IPowerTube>();
            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
            _sut = new CookController(_timer, _display, _powerTube, _userInterface);
        }

        [Test]
        public void Cooking_TimerTick_DisplayCalled()
        {
            _sut.StartCooking(50, 60);
      
            System.Threading.Thread.Sleep(1000);

            _output.Received().OutputLine("Display shows: 00:59");
        }
    }
}
