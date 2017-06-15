﻿
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaspberryBackend;
using CommonFiles.TransferObjects;
using CommonFiles.Networking;

namespace RaspberryBackendTests
{
    [TestClass]
    public class SerializerTests
    {

        private Request _request;
        private Result _result;

        private const string _TESTCOMMAND_1 = "TestCommand_1";
        private const int _TESTPARAM_1 = 1;

        private const string _TESTCOMMAND_2 = "TestCommand_2";
        private const int _TESTPARAM_2 = 2;

        [TestInitialize]
        public void setUp()
        {
            _request = new Request(_TESTCOMMAND_1, _TESTPARAM_1);
            _result = new Result("testexception");
        }

        [TestMethod]
        public void TestSerializeDeserialize()
        {
            //string request = Serializer.Serialize(_request);
            //string result = Serializer.Serialize(_result);

            //Request deserializedRequest = (Request) Serializer.Deserialize(request, typeof(Request));
            //Result deserializedStatus = (Result) Serializer.Deserialize(result, typeof(Result));
            
            
            //Assert.AreEqual(deserializedRequest.command, _request.command);
            //Assert.AreEqual(deserializedRequest.parameter, _request.parameter);


        }
    }
}
