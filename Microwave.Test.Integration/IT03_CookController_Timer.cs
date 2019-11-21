using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT03_CookController_Timer
    {
        public IDisplay _display;
        public IPowerTube _powerTube;
        public Timer _timer;

        public CookController _sut;

        [SetUp]
        public void SetUp()
        {
            _display = Substitute.For<IDisplay>();
            _powerTube = Substitute.For<IPowerTube>();
            _timer = new Timer();
            _sut = new CookController(_timer, _display, _powerTube);
        }

        public void Test1()
        {
            _sut.StartCooking(50,60);
            Assert.That(_timer);
        }
    }
}