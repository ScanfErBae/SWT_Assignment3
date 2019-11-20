using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT04_Timer_CookController
    {
        public IDisplay _display;
        public IPowerTube _powerTube;
        public CookController _cookController;

        public Timer _sut;

        [SetUp]
        public void SetUp()
        {
            _display = Substitute.For<IDisplay>();
            _powerTube = Substitute.For<IPowerTube>();
            _sut = new Timer();
            _cookController = new CookController(_sut, _display,_powerTube);
        }
    }
}