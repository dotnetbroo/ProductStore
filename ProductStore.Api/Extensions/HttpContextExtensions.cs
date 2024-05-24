﻿using ProductStore.Service.Commons.Helpers;

namespace ProductStore.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static void InitAccessor(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            HttpContextHelper.Accessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
        }
    }
}
