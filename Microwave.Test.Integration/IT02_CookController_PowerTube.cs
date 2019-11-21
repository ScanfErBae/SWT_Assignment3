using System;
using System.Threading;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute.ExceptionExtensions;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT02_CookController_PowerTube
    {
        public IDisplay _display;
        public IOutput _output;
        public ITimer _timer;
        public IUserInterface _userInterface;
        public PowerTube _powerTube;

        public CookController _sut;

        [SetUp]
        public void SetUp()
        {
            _userInterface = Substitute.For<IUserInterface>();
            _timer = Substitute.For<ITimer>();
            _output = Substitute.For<IOutput>();
            _display = Substitute.For<IDisplay>();
            _powerTube = new PowerTube(_output);
            _sut = new CookController(_timer, _display, _powerTube, _userInterface);
        }

        [Test]
        public void StartCooking_ValidParameters_PowerTubeStarted()
        {
            _sut.StartCooking(700, 60);

            Assert.That(_powerTube.IsOn, Is.True); 
        }

        [Test]
        public void StartCooking_Invalid_ArgumentOutOfRangeException()
        {
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(
                delegate { throw new ArgumentOutOfRangeException("Must be between 1 and 100 % (incl.)"); });
            Assert.That(ex.ParamName, Is.EqualTo("Must be between 1 and 100 % (incl.)"));
        }

        [Test]
        public void StartCooking_Invalid_ApplicationException()
        {
            ApplicationException ex = Assert.Throws<ApplicationException>(
                delegate { throw new ApplicationException("PowerTube.TurnOn: is already on"); });
            Assert.That(ex.Message, Is.EqualTo("PowerTube.TurnOn: is already on"));
        }

        [Test]
        public void StartCooking_ValidParameters_PowerTubeStopped()
        {
            _sut.StartCooking(50, 60);
            _sut.Stop();
            Assert.That(_powerTube.IsOn, Is.False);
        }

        [Test]
        public void StartCooking_WaitUntilDone_PowerTubeOff()
        {
            _sut.StartCooking(50, 1);

            _timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            Assert.That(_powerTube.IsOn, Is.False);
        }



     

    }
}