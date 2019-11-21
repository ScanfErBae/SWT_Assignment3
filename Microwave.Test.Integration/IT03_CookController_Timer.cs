using System;
using System.Threading;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT03_CookController_Timer
    {
        public IDisplay _display;
        public IPowerTube _powerTube;
        public Timer _timer;
        public IUserInterface _userInterface;

        public CookController _sut;

        [SetUp]
        public void SetUp()
        {
            _display = Substitute.For<IDisplay>();
            _powerTube = Substitute.For<IPowerTube>();
            _timer = new Timer();
            _userInterface = Substitute.For<IUserInterface>();
            _sut = new CookController(_timer, _display, _powerTube, _userInterface);
        }

        [Test]
        public void StartCooking_TimeSet60_TimerStartedWith60()
        {
            _sut.StartCooking(50, 2);
            Assert.That(_timer.TimeRemaining, Is.EqualTo(2));
            System.Threading.Thread.Sleep(1000);
            Assert.That(_timer.TimeRemaining, Is.EqualTo(1));
        }

        [Test]
        public void StartCooking_TimeSet60_TimerS1tartedWith60()
        {
            _sut.StartCooking(50, 2);
            System.Threading.Thread.Sleep(1000);
            _display.Received(1).ShowTime(0,1);
        }
    }
}