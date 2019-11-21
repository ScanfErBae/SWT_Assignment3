using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT08_Door_UserInterface
    {
        public IDisplay _display;
        public ILight _light;
        public ICookController _cookController;
        public IButton _buttonPower;
        public IButton _buttonTime;
        public IButton _buttonStartCancle;
        public UserInterface _userInterface;

        public Door _sut;

        [SetUp]
        public void SetUp()
        {
            _display = Substitute.For<IDisplay>();
            _light = Substitute.For<ILight>();
            _cookController = Substitute.For<ICookController>();
            _buttonPower = Substitute.For<IButton>();
            _buttonTime = Substitute.For<IButton>();
            _buttonStartCancle = Substitute.For<IButton>();
            _sut = new Door();
            _userInterface = new UserInterface(_buttonPower,_buttonTime, _buttonStartCancle, _sut, _display, _light, _cookController);
        }
    }
}