﻿using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace HotelReservationBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hi, I am Restaurant bot");
            
            context.Wait(MessageReceivedAsync);

           
        }

        private static async Task Respond(IDialogContext context)
        {
            var userName = String.Empty;
            context.UserData.TryGetValue<string>("CName", out userName);
            if(string.IsNullOrEmpty(userName))
            {
                await context.PostAsync("What is your name");
                context.UserData.SetValue<bool>("GetName", true);
            }
            else
            {
                await context.PostAsync(String.Format("Hi{0}. How can I help you today?", userName));
            }
        }

        public async Task MessageReceivedAsync(IDialogContext context,IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            var userName = String.Empty;
            var getName = false;
            context.UserData.TryGetValue<string>("CName", out userName);
            context.UserData.TryGetValue<bool>("GetName", out getName);
            if (getName)
            {
                userName = message.Text;
                context.UserData.SetValue<string>("CName", userName);
                context.UserData.SetValue<bool>("GetName", false);
            }
            await Respond(context);
            context.Done(message);
        }
    }
}