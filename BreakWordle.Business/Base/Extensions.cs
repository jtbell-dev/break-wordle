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
            // get words
            var letterService = new FiveLetterWordRetrieverService();
            var words = letterService.GetWords();
            services.AddSingleton(words);
            services.AddSingleton<IWordRetrieverService, FiveLetterWordRetrieverService>();
            services.AddSingleton<ISpellCheckerService, SpellCheckerService>();
            services.AddSingleton<ILetterWeightService, LetterWeightService>();
            services.AddSingleton<IWordWeightService, WordWeightService>();

            services.AddScoped<BreakWordleBL>();

            return services;
        }
    }
}
