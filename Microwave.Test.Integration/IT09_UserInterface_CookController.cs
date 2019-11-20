using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT09_UserInterface_CookController
    {
        public IDisplay _display;
        public ILight _light;
        public IDoor _door;
        public IButton _buttonPower;
        public IButton _buttonTime;
        public IButton _buttonStartCancle;
        public ITimer _timer;
        public IPowerTube _powerTube;
        public CookController _cookController;

        public UserInterface _sut;

        [SetUp]
        public void SetUp()
        {
            _display = Substitute.For<IDisplay>();
            _light = Substitute.For<ILight>();
            _buttonPower = Substitute.For<IButton>();
            _buttonTime = Substitute.For<IButton>();
            _buttonStartCancle = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _timer = Substitute.For<ITimer>();
            _powerTube = Substitute.For<IPowerTube>();
            _cookController = new CookController(_timer, _display, _powerTube);
            _sut = new UserInterface(_buttonPower,_buttonTime, _buttonStartCancle, _door, _display, _light, _cookController);
        }
    }
}