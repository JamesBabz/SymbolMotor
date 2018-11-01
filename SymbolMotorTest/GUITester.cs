using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NolekMoxa.Model;
using SymbolMotor.GUICommunication;
using SymbolMotor.Helpers;

namespace SymbolMotorTest
{
    [TestClass]
    public class GUITester
    {

        [TestMethod]
        public void TestReadTagsFromFile()
        {
            var reader = new GUITagReader();
            var filePath = "C:\\Users\\James\\source\\repos\\Tagliste\\Tagliste\\bin\\Debug\\TestTags";
            var tester = reader.ReadTagsFromFile(filePath);

            Assert.AreEqual("asd, 456, Bool, 30", string.Join(", ", tester[1]));
        }

        [TestMethod]
        public void TestReadTagsFromWrongFile()
        {
            var reader = new GUITagReader();
            var filePath = "C:\\Users\\James\\source\\repos\\Tagliste\\Tagliste\\bin\\Debug\\wewq";
            var tester = reader.ReadTagsFromFile(filePath);

            Assert.IsNull(tester);
        }

        [TestMethod]
        public void TestReadTagsFromEmptyFile()
        {
            var reader = new GUITagReader();
            var filePath = "C:\\Users\\James\\source\\repos\\Tagliste\\Tagliste\\bin\\Debug\\TestTagsEmpty";
            var tester = reader.ReadTagsFromFile(filePath);

            Assert.AreEqual(0, tester.Count);
        }


        [TestMethod]
        public void TestGetConnectedDevices()
        {
            var reader = new GUITagReader();
            var devices = reader.GetAllConnectedDevices("Ethernet");
            Assert.AreEqual(2, devices.Count);
        }

        [TestMethod]
        public void TestGetConnectedDevicesOnWrongEthernetPort()
        {
            var reader = new GUITagReader();
            Assert.IsNull(reader.GetAllConnectedDevices("qwjlejlkr"));
        }

        [TestMethod]
        public void TestGetAllInputChannels()
        {
            var device = new ConnectedDevice
            {
                Channels = new List<Channel>
                {
                    new Channel("ai"),
                    new Channel("di"),
                    new Channel("do"),
                    new Channel("relay"),
                    new Channel("ao"),
                    new Channel("dwqdqw"),
                    new Channel("2212112")
                }
            };

            var reader = new GUITagReader();
            var result = reader.GetAllInputChannels(device);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void TestGetAllInputChannelsWithoutDevice()
        {
            var reader = new GUITagReader();
            var result = reader.GetAllInputChannels(null);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestGetAllInputChannelsWhereNoneExists()
        {
            var reader = new GUITagReader();
            var result = reader.GetAllInputChannels(new ConnectedDevice());
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestGetAllOutputChannels()
        {
            var device = new ConnectedDevice
            {
                Channels = new List<Channel>
                {
                    new Channel("ai"),
                    new Channel("di"),
                    new Channel("do"),
                    new Channel("relay"),
                    new Channel("ao"),
                    new Channel("dwqdqw"),
                    new Channel("2212112")
                }
            };

            var reader = new GUITagReader();
            var result = reader.GetAllOutputChannels(device);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void TestGetAllOutputChannelsWithoutDevice()
        {
            var reader = new GUITagReader();
            var result = reader.GetAllOutputChannels(null);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestGetAllOutputChannelsWhereNoneExists()
        {
            var reader = new GUITagReader();
            var result = reader.GetAllOutputChannels(new ConnectedDevice());
            Assert.AreEqual(0, result.Count);
        }
    }
}