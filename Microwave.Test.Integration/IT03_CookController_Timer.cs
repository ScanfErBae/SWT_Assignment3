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
        public ITimer _timer;
        public IPowerTube _powerTube;
        public CookController _sut;

        [SetUp]
        public void SetUp()
        {
            _timer = new Timer();
            _display = Substitute.For<IDisplay>();
            _powerTube = Substitute.For<IPowerTube>();
            _sut = new CookController(_timer, _display, _powerTube);
        }
    }
}