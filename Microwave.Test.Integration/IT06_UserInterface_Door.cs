using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT06_UserInterface_Door
    {
        public IDisplay _display;
        public ILight _light;
        public ICookController _cookController;
        public IButton _buttonPower;
        public IButton _buttonTime;
        public IButton _buttonStartCancle;
        public Door _door;
        public UserInterface _sut;

        [SetUp]
        public void SetUp()
        {
            _display = Substitute.For<IDisplay>();
            _light = Substitute.For<ILight>();
            _cookController = Substitute.For<ICookController>();
            _buttonPower = Substitute.For<IButton>();
            _buttonTime = Substitute.For<IButton>();
            _buttonStartCancle = Substitute.For<IButton>();
            _door = new Door();
            _sut = new UserInterface(_buttonPower,_buttonTime, _buttonStartCancle, _door, _display, _light, _cookController);
        }
    }
}