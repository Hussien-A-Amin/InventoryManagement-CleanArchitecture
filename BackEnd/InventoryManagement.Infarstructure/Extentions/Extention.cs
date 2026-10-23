using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Infarstructure.Extentions;

public static class Extention
{
    public static string Normalize(this TimeSpan time)
    {
        var days = time.Days;
        
        var hours = time.Hours;
        time=time.Subtract(TimeSpan.FromHours(hours));

        var minutes = time.Minutes;
        time=time.Subtract(TimeSpan.FromMinutes(minutes));
        var seconds = time.Seconds;

        StringBuilder builder = new();
        if (days > 1)
            builder.Append($"{days} Days,");
        if (hours > 1)
            builder.Append($"{hours} Hours,");
        if (minutes > 1)
            builder.Append($"{minutes} Minutes,");
            
        builder.Append($"{seconds} Seconds");

        return builder.ToString();
    }
}
