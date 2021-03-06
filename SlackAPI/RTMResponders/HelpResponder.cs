﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using Pook.SlackAPI;
using Pook.SlackAPI.RTMMessages;

namespace SlackAPI.RTMResponders
{
	[Description("Displays some help")]
	public class HelpResponder : IMessageResponder
	{
		public bool CanRespond(Message message, SlackUser user)
		{
			return
				message.text.StartsWith("?", StringComparison.OrdinalIgnoreCase) ||
				message.text.StartsWith("help", StringComparison.OrdinalIgnoreCase) ||
				message.text.StartsWith("what?", StringComparison.OrdinalIgnoreCase) ||
				message.text.StartsWith("what can I", StringComparison.OrdinalIgnoreCase);
		}

		public Task Respond(ISlackSocket socket, Message message, SlackUser user)
		{
			var response = new StringBuilder();
			foreach (var r in socket.Responders)
			{
				var attr = (DescriptionAttribute)r.GetType().GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault();
				if (attr?.Description == "!")
					continue;
				string name = CapitalToSpace(r.GetType().Name.Replace("Responder", string.Empty));
				response.AppendLine($"*{name}* - {attr?.Description}");
			}

			return socket.Send(message.Reply("Here's is what I know \n>>>" + response.ToString()));
		}

		private string CapitalToSpace(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
				return string.Empty;
			var newText = new StringBuilder(text.Length * 2);
			newText.Append(text[0]);
			for (int i = 1; i < text.Length; i++)
			{
				if (char.IsUpper(text[i]))
					newText.Append(' ');
				newText.Append(text[i]);
			}
			return newText.ToString();
		}
	}
}
