using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT05_6_7_Button_UserInterface
    {
        public IDoor _door;
        public IDisplay _display;
        public ILight _light;
        public ICookController _cookController;
        public UserInterface _userInterface;

        public Button _sutPowerButton;
        public Button _sutTimeButton;
        public Button _sutStartCancleButton;

        [SetUp]
        public void SetUp()
        {
            _display = Substitute.For<IDisplay>();
            _door = Substitute.For<IDoor>();
            _light = Substitute.For<ILight>();
            _cookController = Substitute.For<ICookController>();
            _sutPowerButton = new Button();
            _sutTimeButton = new Button();
            _sutStartCancleButton = new Button();
            _userInterface = new UserInterface(_sutPowerButton, _sutTimeButton, _sutStartCancleButton, _door, _display, _light, _cookController);
        }
    }
}