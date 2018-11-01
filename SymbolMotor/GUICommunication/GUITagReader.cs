using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using SymbolMotor.Helpers;
using NolekMoxa;
using NolekMoxa.Model;

namespace SymbolMotor.GUICommunication
{
    public class GUITagReader
    {
        public List<string[]> ReadTagsFromFile(string path)
        {
            return FileHandler.LoadFromTxt(path);
        }

        public List<ConnectedDevice> GetAllConnectedDevices(string ethernetDescriptionPart)
        {
            var dll = new NolekMoxa_CS();
            var ethifName = dll.GetNetworkInterfaces().FirstOrDefault(x => x[0] == ethernetDescriptionPart)?[1];
            if (ethifName != null)
            {
                ethifName = ethifName.Substring(0, ethifName.IndexOf(' '));
                var test = dll.GetConnectedDevices(ethifName);
                return test;
            }
            return null;
        }

        public List<Channel> GetAllInputChannels(ConnectedDevice device)
        {
            if (device == null) return null;
            var inputTypes = new[] {"ai", "relay", "di"};
            return device.Channels.Where(channel => inputTypes.Any(type => channel.Type.Equals(type))).ToList();
        }
        public List<Channel> GetAllOutputChannels(ConnectedDevice device)
        {
            if (device == null) return null;
            var outputTypes = new[] { "do", "ao"};
            return device.Channels.Where(channel => outputTypes.Any(type => channel.Type.Equals(type))).ToList();
        }

    }
}