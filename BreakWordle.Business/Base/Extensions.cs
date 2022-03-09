using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakWordle.Business.Base
{
    public static class Extensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddSingleton<IWordRetrieverService, FiveLetterWordRetriever>();
            services.AddSingleton<ISpellCheckerService, SpellCheckerService>();
            services.AddSingleton<ILetterWeightService, LetterWeightService>();
            services.AddSingleton<IWordWeightService, WordWeightService>();

            return services;
        }
    }
}
