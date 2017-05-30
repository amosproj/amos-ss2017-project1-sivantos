﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFiles.TransferObjects;
using System.Threading;

namespace RaspberryBackend
{
    /// <summary>
    /// This class represents a Command. It it sends date to a LCD through I2C. 
    /// </summary>
    class SendToLCD : Command
    {
        private const int charsMaxInLine = 16;
        private CancellationTokenSource _cancelSendToLCD;
        private int _scrollSpeed = 1;
        private const byte Command_sendMode = 0;
        private const byte Data_sendMode = 1;

        private LCD lcd = RaspberryPi.Instance.LcdDisplay;

        public SendToLCD(RaspberryPi raspberryPi) : base(raspberryPi)
        {
        }

        /// <summary>
        ///  executes the Command SendToLCD in dependency of the parsed parameter 
        /// </summary>
        /// <param name="parameter">either a text:string which is to be printed on lcd 
        /// or a #command:string e.g #cancel to clear the display 
        /// and terminate all tasks related to a previous call
        /// </param>
        public override void execute(object parameter)
        {

            Debug.WriteLine((string)parameter);
            string text = (string)parameter;
            if (text.Equals("#reset"))
            {
                lcd.initiateLCD();
                return;
            }

            if (text.Equals("#cancel"))
            {
                cancelPreviousTasks();
                Task.Delay(500);
                lcd.clrscr();
                return;
            }

            lcd.clrscr();

            if (text.Length <= charsMaxInLine)
            {
                prints(text);
            }

            else if (text.Length > charsMaxInLine && text.Length <= 2 * charsMaxInLine)
            {

                printInTwoLines(text, charsMaxInLine);
            }
            else
            {
                try
                {
                    _cancelSendToLCD = new CancellationTokenSource();
                    Task scrollTextTask = Task.Factory.StartNew(() => scrollText(text, _scrollSpeed), _cancelSendToLCD.Token);

                }
                catch (OperationCanceledException)
                {

                }

            }
        }

        private void cancelPreviousTasks()
        {

            //cancel previous Task (Scroll)
            if (_cancelSendToLCD != null)
            {
                _cancelSendToLCD.Cancel();
            }


        }

        private void printInTwoLines(string text, int charsMaxInLine)
        {
            string line1 = "", line2 = "";

            line1 = text.Substring(0, charsMaxInLine);
            line2 = text.Substring(charsMaxInLine);


            prints(line1);
            gotoSecondLine();
            prints(line2);
        }



        private void scrollText(string text, int countChars)
        {
            int maxChars = 16;

            lcd.clrscr();

            while (true)
            {
                for (int i = 0; i <= text.Length - maxChars; i = i + countChars < text.Length ? i + countChars : text.Length)
                {

                    if (_cancelSendToLCD.IsCancellationRequested)
                    {
                        return;
                    }

                    Task.Delay(200).Wait();
                    lcd.clrscr();
                    for (int j = i; j < maxChars + i && j < text.Length; j++)
                    {
                        printc(text.ElementAt(j));
                    }
                    Task.Delay(200).Wait();

                }

                if (_cancelSendToLCD.IsCancellationRequested)
                {
                    return;
                }

            }


        }



        /**
        * skip to second line
        **/
        private void gotoSecondLine()
        {
            lcd.write(0xc0, Command_sendMode);

        }

        /**
      * Can print string onto display
      **/
        private void prints(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                this.printc(text[i]);
            }
        }

        /**
      * Print single character onto display
      **/
        private void printc(char letter)
        {
            try
            {
                lcd.write(Convert.ToByte(letter), Data_sendMode);
            }
            catch (Exception e)
            {

            }
        }
    }
}