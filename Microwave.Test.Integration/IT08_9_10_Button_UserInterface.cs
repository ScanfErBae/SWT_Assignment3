using System;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT08_9_10_Button_UserInterface
    {
        public IDoor _door;
        public IDisplay _display;
        public ILight _light;
        public ICookController _cookController;
        public UserInterface _userInterface;
       

        public Button _sutPowerButton;
        public Button _sutTimeButton;
        public Button _sutStartCancelButton;

        [SetUp]
        public void SetUp()
        {
            _display = Substitute.For<IDisplay>();
            _door = Substitute.For<IDoor>();
            _light = Substitute.For<ILight>();
            _cookController = Substitute.For<ICookController>();
           
            _sutPowerButton = new Button();
            _sutTimeButton = new Button();
            _sutStartCancelButton = new Button();
            _userInterface = new UserInterface(_sutPowerButton, _sutTimeButton, _sutStartCancelButton, _door, _display,
                _light, _cookController);
        }

        #region PowerButton
        [Test]
        public void Press_once_show_displayEqual100()
        {
            _sutPowerButton.Press();
            _display.Received(1).ShowPower(Arg.Is<int>(50));
        }

        [Test]
        public void Press_twice_show_displayEqual100()
        {
            _sutPowerButton.Press();
            _sutPowerButton.Press();
            _display.Received(1).ShowPower(Arg.Is<int>(100));
        }

        [Test]
        public void Press_four_times_show_displayEqual200()
        {
            _sutPowerButton.Press();
            _sutPowerButton.Press();
            _sutPowerButton.Press();
            _sutPowerButton.Press();
            _display.Received(1).ShowPower(Arg.Is<int>(200));
        }

        [Test]
        public void Press_twenty_times_show_displayEqual700()
        {
            for (int i = 0; i < 20; i++)
            {
                _sutPowerButton.Press();
            }
            _display.Received(1).ShowPower(Arg.Is<int>(700));
        }

        #endregion

        #region TimeButton
        [Test]
        public void Set_Time_Press_Power_Then_TimeButton_twice_show_displayEqual2()
        {
            _sutPowerButton.Press();
            _sutTimeButton.Press();
            _display.Received(1).ShowTime(Arg.Is<int>(1), Arg.Is<int>(0));
        }


        [Test]
        public void Increase_Time_Press_Power_Then_TimeButton_four_times_show_displayEqual2()
        {
            _sutPowerButton.Press();
            _sutTimeButton.Press();
            _sutTimeButton.Press();
            _sutTimeButton.Press();
            _sutTimeButton.Press();
            _display.Received(1).ShowTime(Arg.Is<int>(4), Arg.Is<int>(0));
        }

        [Test]
        public void Not_Pressing_Power_Then_TimeButton_once_Exception()
        {
            _sutTimeButton.Press();
            _display.Received(0).ShowTime(Arg.Is<int>(0), Arg.Is<int>(0));
        }

        #endregion

        #region StartCancelButton

        [Test]
       public void Press_StartCancel_at_start_Nothing_happens()
        {
            _sutStartCancelButton.Press();
            _display.DidNotReceiveWithAnyArgs();
            _light.DidNotReceiveWithAnyArgs();
            _cookController .DidNotReceiveWithAnyArgs();
        }

       [Test]
       public void Press_StartCancel_at_Set_Power_State_Nothing_happens()
       {
           _sutPowerButton.Press();
           _sutStartCancelButton.Press();
           _display.Received(0).ShowPower(Arg.Is<int>(0));
        }

       [Test]
       public void Press_StartCancel_at_Set_Time_State_Starts_Cooking()
       {
           _sutPowerButton.Press();
           _sutTimeButton.Press();
           _sutStartCancelButton.Press();
           _cookController.Received(1).StartCooking(50,60);
           
           
       }

       [Test]
       public void Press_StartCancel_at_Set_Time_State_Lights_Turned_On()
       {
           _sutPowerButton.Press();
           _sutTimeButton.Press();
           _sutStartCancelButton.Press();
           _light.Received(1).TurnOn();
       }

       [Test]
       public void Press_StartCancel_When_Cooking_Lights_Off()
       {
           _sutPowerButton.Press();
           _sutTimeButton.Press();
           _sutStartCancelButton.Press();
           _sutStartCancelButton.Press();
           _light.Received(1).TurnOff();
        }

       [Test]
       public void Press_StartCancel_When_Cooking_Cooking_stop()
       {
           _sutPowerButton.Press();
           _sutTimeButton.Press();
           _sutStartCancelButton.Press();
           _sutStartCancelButton.Press();
           _cookController.Received(1).Stop();
       }

       [Test]
       public void Press_StartCancel_When_Cooking_Clear_display()
       {
           _sutPowerButton.Press();
           _sutTimeButton.Press();
           _sutStartCancelButton.Press();
           _sutStartCancelButton.Press();
           _display.Received().Clear();
       }


        #endregion
    }
}

