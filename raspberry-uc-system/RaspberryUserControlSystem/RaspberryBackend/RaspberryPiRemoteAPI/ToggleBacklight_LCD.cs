﻿using System.Diagnostics;

namespace RaspberryBackend
{

    /// <summary>
    /// This class represents a Command. It it can be used to toggle the Backlight of a I2C connected LCD on the RaspberryPi.
    /// </summary>
    public partial class RaspberryPi
    {

        /// <summary>
        /// Toogles the Backlight of the LCD to onn or off
        /// </summary>
        /// <param name="parameter">1 for Backlight on or 0 for off</param>
        public void ToggleBacklight_LCD(string requestedParameter)
        {
            const byte ON = 0x01;
            const byte OFF = 0x00;

            if (requestedParameter.Equals("1"))
            {
                Debug.WriteLine("Received command ToggleBacklightLCD On!");
                switchToState(ON);

            }
            else if (requestedParameter.Equals("0"))
            {
                Debug.WriteLine("Received command ToggleBacklightLCD Off!");
                switchToState(OFF);
            }


        }

        private void switchToState(byte targetState)
        {
            setLCDBackgroundState(targetState);
            Debug.WriteLine("Backlight state changed!");
        }
    }
}