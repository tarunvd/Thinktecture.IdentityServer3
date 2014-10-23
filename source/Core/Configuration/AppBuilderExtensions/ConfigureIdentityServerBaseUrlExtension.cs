﻿/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Thinktecture.IdentityServer.Core.Extensions;

namespace Owin
{
    static class ConfigureIdentityServerBaseUrlExtension
    {
        public static IAppBuilder ConfigureIdentityServerBaseUrl(this IAppBuilder app, string publicHostName)
        {
            app.Use(async (ctx, next) =>
            {
                var baseUrl = ctx.Environment.GetBaseUrl(publicHostName);
                ctx.Environment.SetIdentityServerBaseUrl(baseUrl);
                
                var basePath = ctx.Request.PathBase.Value;
                if (basePath == String.Empty) basePath = "/";
                ctx.Environment.SetIdentityServerBasePath(basePath);

                await next();
            });

            return app;
        }
    }
}
