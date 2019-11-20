using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT05_UserInterface_Button
    {
        public IDoor _door;
        public IDisplay _display;
        public ILight _light;
        public ICookController _cookController;
        public Button _buttonPower;
        public Button _buttonTime;
        public Button _buttonStartCancle;
        public UserInterface _sut;

        [SetUp]
        public void SetUp()
        {
            _display = Substitute.For<IDisplay>();
            _door = Substitute.For<IDoor>();
            _light = Substitute.For<ILight>();
            _cookController = Substitute.For<ICookController>();
            _buttonPower = new Button();
            _buttonTime = new Button();
            _buttonStartCancle = new Button();
            _sut = new UserInterface(_buttonPower,_buttonTime, _buttonStartCancle, _door, _display, _light, _cookController);
        }
    }
}