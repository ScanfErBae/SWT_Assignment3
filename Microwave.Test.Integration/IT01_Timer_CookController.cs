using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT01_Timer_CookController
    {
        public IDisplay _display;
        public IPowerTube _powerTube;
        public IUserInterface _userInterface;
        public CookController _cookController;

        public Timer _sut;

        [SetUp]
        public void SetUp()
        {
            _display = Substitute.For<IDisplay>();
            _powerTube = Substitute.For<IPowerTube>();
            _userInterface = Substitute.For<IUserInterface>();
            _sut = new Timer();
            _cookController = new CookController(_sut, _display,_powerTube, _userInterface);
        }

        [Test]
        public void OnTimeExpired_PowerTube_TurnOff()
        {
            _cookController.StartCooking(50, 1);

            Thread.Sleep(1500);

            _powerTube.Received(1).TurnOff();
        }

        [Test]
        public void OnTimeExpired_CookingIsDone_Called()
        {
            _cookController.StartCooking(50, 1);

            Thread.Sleep(1500);

            _userInterface.Received(1).CookingIsDone();
        }

        [Test]
        public void OnTimeExpired_NotReached_CookingIsDone_NotCalled_Called()
        {
            _cookController.StartCooking(50, 1);

            Thread.Sleep(950);

            _userInterface.DidNotReceive().CookingIsDone();
        }

        [Test]
        public void OnTimeTick()
        {
            _cookController.StartCooking(50, 60);

            Thread.Sleep(1500);

            _display.Received(1).ShowTime(0, 59);

        }
    }
}