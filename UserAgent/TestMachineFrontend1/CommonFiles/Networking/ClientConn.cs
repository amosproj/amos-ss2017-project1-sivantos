﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace CommonFiles.Networking
{

    /// <summary>
    /// Allows to send to a server and receive from a server
    /// </summary>
    /// <typeparam name="inType">Type of Objects received from the server</typeparam>
    /// <typeparam name="outType">Type of Objects send to the server</typeparam>
    public class ClientConn<inType, outType> : IDisposable
    {
        private ObjConn<inType, outType> objConn;

        /// <summary>
        /// Connects to a server
        /// </summary>
        /// <param name="endpoint">Endpoint of the server to connect to</param>
        /// <returns>Returns the created clientConn</returns>
        public static async Task<ClientConn<inType, outType>> connectAsync(IPEndPoint endpoint)
        {
            TcpClient socket = new TcpClient();
            await socket.ConnectAsync(endpoint.Address, endpoint.Port);
            NetworkStream stream = socket.GetStream();
            ObjConn<inType, outType> objConn = new ObjConn<inType, outType>(stream);
            return new ClientConn<inType, outType>(objConn);
        }

        /// <summary>
        /// Connects to a Server
        /// </summary>
        /// <param name="hostname">hostname of the server to connect to</param>
        /// <param name="port">port of the server to connect to</param>
        /// <returns>Returns the created ClientConn</returns>
        public static async Task<ClientConn<inType, outType>> connectAsync(string hostname, int port)
        {
            TcpClient socket = new TcpClient();
            await socket.ConnectAsync(hostname, port);
            NetworkStream stream = socket.GetStream();
            ObjConn<inType, outType> objConn = new ObjConn<inType, outType>(stream);
            return new ClientConn<inType, outType>(objConn);
        }

        // private constructor to avoid instantiation without calling connect
        private ClientConn(ObjConn<inType, outType> objConn)
        {
            this.objConn = objConn;
        }

        /// <summary>
        /// sends an Object of Type outType to the server
        /// </summary>
        /// <param name="obj"></param>
        public void sendObject(outType obj)
        {
            objConn.sendObject(obj);
        }

        /// <summary>
        /// receives an Object of Type outType from the server
        /// </summary>
        /// <returns></returns>
        public inType receiveObject()
        {
            return objConn.receiveObject();
        }

        /// <summary>
        /// closes the connection
        /// </summary>
        public void Dispose()
        {
            objConn.Dispose();
        }
    }
}


