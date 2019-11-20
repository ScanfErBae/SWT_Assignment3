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
        public IPowerTube _powerTube;

        public CookController _sut;

        [SetUp]
        public void SetUp()
        {
            _timer = Substitute.For<ITimer>();
            _powerTube = Substitute.For<IPowerTube>();
            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
            _sut = new CookController(_timer, _display, _powerTube);
        }
    }
}
