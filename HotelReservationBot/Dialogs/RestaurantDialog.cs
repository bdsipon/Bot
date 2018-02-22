using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using HotelReservationBot.Models;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HotelReservationBot.Dialogs
{
    public class RestaurantDialog
    {
        public static readonly IDialog<string> dialog = Chain.PostToChain()
        .Select(msg => msg.Text)
        .Switch(
        new RegexCase<IDialog<string>>(new Regex("^hi", RegexOptions.IgnoreCase), (context, txt) =>
        {
            return Chain.ContinueWith(new RootDialog(), AfterGreetingContinuation);
        }),
        new DefaultCase<string, IDialog<string>>((context, txt) =>
        {
            return Chain.ContinueWith(FormDialog.FromForm(UserOrder.BuildForm, FormOptions.PromptInStart), AfterGreetingContinuation);
        }))
        .Unwrap()
        .PostToUser();
        private async static Task<IDialog<string>> AfterGreetingContinuation(IBotContext context, IAwaitable<object> res)
        {
            var token = await res;
            var name = "User";
            context.UserData.TryGetValue<string>("CName", out name);
            return Chain.Return("Thank you for approaching restaurant bot :" + name);
        }
    }
}