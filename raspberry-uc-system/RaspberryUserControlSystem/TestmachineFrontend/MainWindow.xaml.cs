﻿using CommonFiles.Networking;
using CommonFiles.TransferObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Xml;

namespace TestmachineFrontend
{
    /// <summary>
    /// Simple UI for establishing connection
    /// to Raspberry-Pi
    /// </summary>
    public partial class MainWindow : Window
    {
        //private string hostname = "minwinpc";
        private List<ClientConn<Request>> connections = new List<ClientConn<Request>>();
        private ClientConn<Request> clientConnection;

        public MainWindow()
        {
            InitializeComponent();

            //test for the class RequestConnClient
            //try
            //{
            //    clientConnection = new ClientConn<Request>(hostname, 13370);
            //    Debug.WriteLine("Connection to " + hostname + " established.");
            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine(e);
            //    Debug.WriteLine("Connection to " + hostname + " failed.");
            //}


            this.DataContext = this;
        }

        public UInt16 PinID { get; set; }
        public string IPaddress { get; set; }
        public string Port { get; set; }
        public string DeviceName { get; set; }


        public List<ClientConn<Request>> Connections {
             get { return connections;}
             set { connections = value; }
        }

        private void connectIP_button_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    Connections.Add(new ClientConn<Request>(IPaddress, 13370));
            //}
            //catch (Exception)
            //{
            //    this.debug.Items.Add(new DebugContent { origin = "TCP Connection", text = "Couldn't establish connection" });
            //}
            connectToBackend();

        }

        public void connectToBackend()
        {
            try
            {
                clientConnection = new ClientConn<Request>(IPaddress, 13370);
                Debug.WriteLine("Connection to " + IPaddress + " established.");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.WriteLine("Connection to " + IPaddress + " failed.");
            }
        }

        private void vcSlider_DragStarted(object sender, RoutedEventArgs e)
        {

        }

        private void vcSlider_DragCompleted(object sender, RoutedEventArgs e)
        {

        }

        private void vcSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void soundSlider_DragStarted(object sender, RoutedEventArgs e)
        {

        }

        private void soundSlider_DragCompleted(object sender, RoutedEventArgs e)
        {

        }

        private void soundSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void readPin_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Connections[0].sendObject(new Request("ReadPin", PinID));
                clientConnection.sendObject(new Request("ReadPin", PinID));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Request could not be sent: " + ex.Message);
            }
        }

        private void writePin_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Connections[0].sendObject(new Request("WritePin", PinID));
                clientConnection.sendObject(new Request("WritePin", PinID));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Request could not be sent: " + ex.Message);
            }
        }

        private void reset_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Connections[0].sendObject(new Request("ResetPin", PinID));
                clientConnection.sendObject(new Request("ResetPin", PinID));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Request could not be sent: " + ex.Message);
            }
        }

        private void ledOFF_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clientConnection.sendObject(new Request("LightLED", 0));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Request could not be sent: " + ex.Message);
            }
        }

        private void ledON_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clientConnection.sendObject(new Request("LightLED", 1));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Request could not be sent: " + ex.Message);
            }

        }

    }
}