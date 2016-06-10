﻿using System;
using System.Threading.Tasks;

using Pook.SlackAPI.RTMMessages;
using System.ComponentModel;

namespace Pook.SlackAPI.RTMResponders
{
    [Description("!")]
    public class EchoResponder : IMessageResponder
    {
        public bool CanRespond(Message message, SlackUser user)
        {
            return message.text.StartsWith("echo", StringComparison.OrdinalIgnoreCase);
        }

        public Task Respond(ISlackSocket socket, Message message, SlackUser user)
        {
            socket.Send(message.Reply("ECHO: " + message.text.Replace("echo", string.Empty)));

            return Task.FromResult(0);
        }
    }
}