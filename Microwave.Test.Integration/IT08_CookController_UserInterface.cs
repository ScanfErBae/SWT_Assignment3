using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT08_CookController_UserInterface
    {
        public IDisplay _display;
        public ILight _light;
        public IDoor _door;
        public IButton _buttonPower;
        public IButton _buttonTime;
        public IButton _buttonStartCancle;
        public ITimer _timer;
        public IPowerTube _powerTube;
        public CookController _sut;
        public UserInterface _userInterface;

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
            _sut = new CookController(_timer, _display, _powerTube);
            _userInterface = new UserInterface(_buttonPower,_buttonTime, _buttonStartCancle, _door, _display, _light, _sut);
        }
    }
}