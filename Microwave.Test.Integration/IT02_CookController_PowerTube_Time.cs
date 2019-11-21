using System;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute.ExceptionExtensions;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT02_CookController_PowerTube_Time
    {
        public IDisplay _display;
        public IOutput _output;
        public IUserInterface _userInterface;

        public PowerTube _powerTube;
        public Timer _timer;
        public CookController _sut;

        [SetUp]
        public void SetUp()
        {
            _userInterface = Substitute.For<IUserInterface>();
            _timer = new Timer();
            _output = Substitute.For<IOutput>();
            _display = Substitute.For<IDisplay>();
            _powerTube = new PowerTube(_output);
            _sut = new CookController(_timer, _display, _powerTube, _userInterface);
        }

        [Test]
        public void StartCooking_ValidParameters_PowerTubeStarted()
        {
            _sut.StartCooking(700, 60);

            _output.Received(1).OutputLine("PowerTube works with 100 %");

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

            _output.Received(1).OutputLine("PowerTube turned off");

        }

        [Test]
        public void StartCooking_WaitUntilDone_PowerTubeOff()
        {
            _sut.StartCooking(50, 1);

            System.Threading.Thread.Sleep(1500);

            _output.Received(1).OutputLine("PowerTube turned off");
        }
    }
}