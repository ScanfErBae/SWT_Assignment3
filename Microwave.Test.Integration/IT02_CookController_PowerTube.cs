using System;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT02_CookController_PowerTube
    {
        public IDisplay _display;
        public IOutput _output;
        public ITimer _timer;
        public PowerTube _powerTube;

        public CookController _sut;

        [SetUp]
        public void SetUp()
        {
            _timer = Substitute.For<ITimer>();
            _output = Substitute.For<IOutput>();
            _display = Substitute.For<IDisplay>();
            _powerTube = new PowerTube(_output);
            _sut = new CookController(_timer, _display, _powerTube);
        }

        [Test]
        public void StartCooking_ValidParameters_PowerTubeStarted()
        {
            _sut.StartCooking(50, 60);

            Assert.That(_powerTube.IsOn, Is.EqualTo(true)); 
        }

        [Test]
        public void StartCooking_ValidParameters_PowerTubeStopped()
        {
            _sut.StartCooking(50, 60);
            _sut.Stop();
            Assert.That(_powerTube.IsOn, Is.EqualTo(false));
        }


    }
}