﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Thinktecture.IdentityServer.Core.Protocols.Connect.Models;

namespace Thinktecture.IdentityServer.Core.Protocols.Connect.Results
{
    public class OidcAuthorizeErrorResult : IHttpActionResult
    {
        AuthorizeError Error { get; set; }

        public OidcAuthorizeErrorResult(AuthorizeError error)
        {
            Error = error;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult<HttpResponseMessage>(Execute());
        }

        private HttpResponseMessage Execute()
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.Redirect);

            if (Error.ErrorType == ErrorTypes.User)
            {
                //return BadRequest(error);

                // todo: return error page
            }
            else if (Error.ErrorType == ErrorTypes.Client)
            {
                string character;
                if (Error.ResponseMode == Constants.ResponseModes.Query ||
                    Error.ResponseMode == Constants.ResponseModes.FormPost)
                {
                    character = "?";
                }
                else
                {
                    character = "#";
                }

                var url = string.Format("{0}{1}error={2}", Error.ErrorUri.AbsoluteUri, character, Error.Error);

                if (Error.State.IsPresent())
                {
                    url = string.Format("{0}&state={1}", url, Error.State);
                }

                responseMessage.Headers.Location = new Uri(url);
                return responseMessage;
            }

            throw new ArgumentException("errorType");
        }
    }
}
