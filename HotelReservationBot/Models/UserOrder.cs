using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Bot.Builder.FormFlow;

namespace HotelReservationBot.Models
{

    public enum PizzaSizeOptions
    {
        Regular,
        Medium,
        Large
    }

    public enum AmmentitiesOptions
    {
        Banquet,
        Pool,
        Garden,
        KidsZone
    }
    [Serializable]
    public class UserOrder
    {
        public PizzaSizeOptions? PizzaSize;
        public int? Pizzas;
        public DateTime? DeliveryDate;
        public List<AmmentitiesOptions> Amenities;

        public static IForm<UserOrder> BuildForm()
        {
            return new FormBuilder<UserOrder>()
                .Message("Welcome to the restaurant").Build();
            
        }
    }
}